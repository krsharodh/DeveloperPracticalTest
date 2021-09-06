using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DeveloperPracticalTest
{
    /// <summary>
    /// Handles various functionalities of customer management
    /// </summary>
    public class CustomerController : ICustomerController
    {
        List<CustomerModel> customers = new List<CustomerModel>();

        public CustomerController()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        /// <summary>
        /// Get customer object data using ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer Object or Null if Customer ID not present</returns>
        public CustomerModel getCustomerByID(int id)
        {
            return customers.Find(x => x.CustomerId == id);
        }

        /// <summary>
        /// Get customer ID as input from the user and return a Customer object
        /// </summary>
        /// <returns>Customer object or Null if customer ID not found</returns>
        public CustomerModel getCustomerData()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the customer ID:");
                    int customerID = Convert.ToInt32(Console.ReadLine());

                    CustomerModel customer = getCustomerByID(customerID);
                    if (customer == null)
                    {
                        throw new Exception("Customer not found !");
                    }
                    return customer;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets valid username as input from the user
        /// </summary>
        /// <param name="editMode">If true, Changes the input text and avaoids matching username with same user</param>
        /// <param name="prevUsername">Previous User Name (Only applicable on edit mode)</param>
        /// <returns>Valid Username</returns>
        string getUsername(bool editMode = false, string prevUsername = "")
        {
            string userName;
            while (true)
            {
                try
                {
                    Console.WriteLine(editMode ? "\nEnter new username" : "\nEnter username:");
                    userName = Console.ReadLine().Trim();

                    Regex r = new Regex("^[a-zA-Z0-9]*$");

                    // Username Length: 5 to 20, Allowed Chars: AlphaNumeric
                    if (userName == "" || userName.Length < 5 || userName.Length > 20 || !r.IsMatch(userName))
                    {
                        throw new Exception("Invalid username: Should only contain letter or number, must between 5 and 20 characters");
                    }
                    else if (customers.Exists(x => (x.Username == userName) && (x.Username != prevUsername)))
                    {
                        throw new Exception("Username already taken");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message ?? "Invalid username");
                }
            }
            return userName;
        }

        /// <summary>
        /// Gets valid name as input from the user
        /// </summary>
        /// <param name="promptString">prompt string presented to user (Ex: First Name, Last Name)</param>
        /// <param name="editMode">If true, Changes the input text</param>
        /// <returns>Valid Name String</returns>
        string getName(string promptString, bool editMode = false)
        {
            string name;
            while (true)
            {
                try
                {
                    Console.WriteLine(editMode ? $"\nEnter new {promptString}" : $"\nEnter {promptString}:");
                    name = Console.ReadLine().Trim();
                    Regex r = new Regex("^[0-9]*$");

                    // Username Length: atleast 1, Allowed Chars: No Numbers
                    if (name == "" || r.IsMatch(name))
                    {
                        throw new Exception($"Invalid {promptString}: Cannot be blank and no numbers");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message ?? $"Invalid {promptString}");
                }
            }
            return name;
        }

        /// <summary>
        /// Gets valid phone number as input from the user
        /// </summary>
        /// <param name="editMode">If true, Changes the input text</param>
        /// <returns>Valid phone number string</returns>
        string getPhoneNumber(bool editMode = false)
        {
            string phoneNumber;
            while (true)
            {
                try
                {
                    Console.WriteLine(editMode ? "\nEnter new phone number" : "\nEnter phone number:");
                    phoneNumber = Console.ReadLine().Trim();
                    Regex r = new Regex("^[0-9]*$");
                    if (phoneNumber == "" || !r.IsMatch(phoneNumber))
                    {
                        throw new Exception("Invalid phoneNumber: Cannot be blank and only valid Australian landline or mobile numbers");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message ?? "Invalid phone number");
                }
            }
            return phoneNumber;
        }

        /// <summary>
        /// Converts datestring(dd/mm/yyy) to DateTime Object
        /// </summary>
        /// <param name="dateString">A string in the form (dd/mm/yyy)</param>
        /// <returns>Valid DateTime object or Default Date</returns>
        public DateTime getDateUtil(string dateString)
        {
            DateTime date = new DateTime();
            try
            {

                string[] dateParts = dateString.Split('/');
                date = new DateTime(
                            Int32.Parse(dateParts[2]),
                            Int32.Parse(dateParts[1]),
                            Int32.Parse(dateParts[0])
                            );
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message ?? "Invalid Date");
            }
            return date;
        }

        /// <summary>
        /// Gets date of birth in the form (dd/mm/yyy) as a input from the user and returns a valid DateTime object 
        /// </summary>
        /// <param name="editMode">If true, Changes the input text</param>
        /// <returns>valid DateTime object</returns>
        DateTime getDateOfBirth(bool editMode = false)
        {
            DateTime dateOfBirth;
            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter Year (dd/mm/yyyy):");
                    string dob = Console.ReadLine();

                    dateOfBirth = getDateUtil(dob);
                    if (DateTime.Now.Subtract(dateOfBirth).TotalDays > 40150 || DateTime.Now.Subtract(dateOfBirth).TotalDays <= 0)
                    {
                        throw new Exception("Age cannot be more than 110 years");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message ?? "Invalid Date Format");
                }
            }
            return dateOfBirth;
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        public void createCustomer()
        {
            // Inputs required to create a customer
            string userName = getUsername();
            string firstName = getName("first name");
            string lastName = getName("last name");
            string phoneNumber = getPhoneNumber();
            DateTime dateOfBirth = getDateOfBirth();

            CustomerModel customer = new CustomerModel(
                userName,
                firstName,
                lastName,
                phoneNumber,
                dateOfBirth
                );

            customers.Add(customer);

            Console.WriteLine("");
            Log.Information("Customer with following details created successfully\n");
            Console.WriteLine(customer);
        }

        /// <summary>
        /// Displays searched customers in the form of a table. Matches search with first name/last name. Search Text is case sensitive 
        /// </summary>
        public void searchCustomers()
        {
            Console.WriteLine("Search customer by first name/last name:");
            string searchText = Console.ReadLine();

            List<CustomerModel> searchResults = customers.FindAll(x => x.FirstName.Contains(searchText) || x.LastName.Contains(searchText));

            TableUtil tableUtil = new TableUtil();

            tableUtil.PrintLine();
            tableUtil.PrintRow("ID", "First Name", "Last Name", "Phone Number");
            tableUtil.PrintLine();
            foreach (CustomerModel c in searchResults)
            {
                tableUtil.PrintRow(c.CustomerId.ToString(), c.FirstName, c.LastName, c.PhoneNumber);
            }
            tableUtil.PrintLine();
        }

        /// <summary>
        /// Edits Customer Data
        /// </summary>
        public void editCustomer()
        {
            while (true)
            {

                try
                {
                    Console.WriteLine("\nPlease enter the customer ID:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var customerIndex = customers.FindIndex(x => x.CustomerId == id);
                    if (customerIndex < 0)
                    {
                        throw new Exception("Customer not found !");
                    }

                    // Takes input of new customer data
                    //TODO Allow optional editing of customers
                    customers[customerIndex].Username = getUsername(true, customers[customerIndex].Username);
                    customers[customerIndex].FirstName = getName("first name", true);
                    customers[customerIndex].LastName = getName("last name", true);
                    customers[customerIndex].PhoneNumber = getPhoneNumber(true);
                    customers[customerIndex].DateOfBirth = getDateOfBirth(true);

                    Console.WriteLine();
                    Console.WriteLine(customers[customerIndex]);
                    Log.Information($"Customer details updated successfully");
                    return;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message ?? "Invalid ID");
                }

            }
        }

        /// <summary>
        /// Add customers from a json file. Gets json file path as input from the user
        /// </summary>
        public void addCustomersFromJson()
        {
            /*
             * Json File Structure
             * [
             *     {
             *       "username": "jamesbond007",
	         *       "firstName": "James",
	         *       "lastName": "Bond",
	         *       "dataOfBirth": "01/01/1990",
	         *       "phoneNumber": "0123456789"
             *     }...,
             * ]
             */

            //TODO Validate Json data
            while (true)
            {

                try
                {
                    Console.WriteLine("Enter location of json file:");
                    string fileName = Console.ReadLine();
                    using (StreamReader r = new StreamReader(fileName))
                    {
                        string json = r.ReadToEnd();
                        dynamic array = JsonConvert.DeserializeObject(json);
                        int customerCount = 0;
                        foreach (var item in array)
                        {
                            CustomerModel c = new CustomerModel(
                                    $"{item.username}",
                                    $"{item.firstName}",
                                    $"{item.lastName}",
                                    $"{item.phoneNumber}",
                                    getDateUtil($"{item.dataOfBirth}")
                                    );
                            customers.Add(c);
                            Console.WriteLine();
                            Console.Write(c);
                            customerCount++;
                        }

                        Console.WriteLine();
                        Log.Information($"{customerCount} customers added successfully");
                    }
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
                }
                return;
            }
        }
    }
}
