using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DeveloperPracticalTest
{
    class CustomerController
    {
        List<Customer> customers = new List<Customer>();

        public Customer getCustomerByID(int id)
        {
            return customers.Find(x => x.CustomerId == id);
        }

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
                    Console.WriteLine(ex.Message ?? "Invalid username");
                }
            }
            return userName;
        }

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
                    if (name == "" || r.IsMatch(name))
                    {
                        throw new Exception($"Invalid {promptString}: Cannot be blank and no numbers");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message ?? $"Invalid {promptString}");
                }
            }
            return name;
        }

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
                    Console.WriteLine(ex.Message ?? "Invalid phone number");
                }
            }
            return phoneNumber;
        }

        DateTime getDateUtil(string dateString)
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
                Console.WriteLine("Invalid Date");
            }
            return date;
        }

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
                    Console.WriteLine(ex.Message ?? "Invalid Date Format");
                }
            }
            return dateOfBirth;
        }

        public void createCustomer()
        {
            // Inputs required to create a customer
            string userName = getUsername();
            string firstName = getName("first name");
            string lastName = getName("last name");
            string phoneNumber = getPhoneNumber();
            DateTime dateOfBirth = getDateOfBirth();

            Customer customer = new Customer(
                userName,
                firstName,
                lastName,
                phoneNumber,
                dateOfBirth
                );

            customers.Add(customer);

            Console.WriteLine(customer);
            Console.WriteLine($"Customer created successfully");
        }

        public void searchCustomers()
        {
            Console.WriteLine("Search customer by first name/last name:");
            string searchText = Console.ReadLine();

            List<Customer> searchResults = customers.FindAll(x => x.FirstName.Contains(searchText) || x.LastName.Contains(searchText));

            TableUtil.PrintLine();
            TableUtil.PrintRow("ID", "First Name", "Last Name", "Phone Number");
            TableUtil.PrintLine();
            foreach (Customer c in searchResults)
            {
                TableUtil.PrintRow(c.CustomerId.ToString(), c.FirstName, c.LastName, c.PhoneNumber);
            }
            TableUtil.PrintLine();
        }

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

                    customers[customerIndex].Username = getUsername(true, customers[customerIndex].Username);
                    customers[customerIndex].FirstName = getName("first name", true);
                    customers[customerIndex].LastName = getName("last name", true);
                    customers[customerIndex].PhoneNumber = getPhoneNumber(true);
                    customers[customerIndex].DateOfBirth = getDateOfBirth(true);

                    Console.WriteLine(customers[customerIndex]);
                    Console.WriteLine($"Customer details updated successfully");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message ?? "Invalid ID");
                }

            }
        }

        public void addCustomersFromJson()
        {
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
                            Customer c = new Customer(
                                    $"{item.username}",
                                    $"{item.firstName}",
                                    $"{item.lastName}",
                                    $"{item.phoneNumber}",
                                    getDateUtil($"{item.dataOfBirth}")
                                    );
                            customers.Add(c);
                            Console.Write(c);
                            customerCount++;
                        }

                        Console.WriteLine($"{customerCount} customers added successfully");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return;
            }
        }
    }
}
