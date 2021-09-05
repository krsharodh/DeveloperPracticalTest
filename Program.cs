using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DeveloperPracticalTest
{
    class Program
    {
        List<Customer> customers = new List<Customer>();
        List<CallNote> callNotes = new List<CallNote>();
        static int tableWidth = 75;

        void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
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

        void createCustomer()
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

        void searchCustomers()
        {
            Console.WriteLine("Search customer by first name/last name:");
            string searchText = Console.ReadLine();

            List<Customer> searchResults = customers.FindAll(x => x.FirstName.Contains(searchText) || x.LastName.Contains(searchText));

            PrintLine();
            PrintRow("ID", "First Name", "Last Name", "Phone Number");
            PrintLine();
            foreach (Customer c in searchResults)
            {
                PrintRow(c.CustomerId.ToString(), c.FirstName, c.LastName, c.PhoneNumber);
            }
            PrintLine();
        }

        void editCustomer()
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

        void addCustomersFromJson()
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

        void addCallNote()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the customer ID:");
                    int customerID = Convert.ToInt32(Console.ReadLine());
                    var customerIndex = customers.FindIndex(x => x.CustomerId == customerID);
                    if (customerIndex < 0)
                    {
                        throw new Exception("Customer not found !");
                    }

                    Console.WriteLine("\nPlease select one of the following");
                    Console.WriteLine("1. Existing Issue");
                    Console.WriteLine("2. New Issue");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"Existing Call Notes for {customers[customerIndex].FirstName} {customers[customerIndex].LastName}");
                            List<CallNote> callNotesTemp = callNotes.FindAll(x => x.CustomerId == customerID);

                            PrintLine();
                            PrintRow("Call Note ID", "Parent Call Note ID", "Text");
                            PrintLine();
                            foreach (CallNote c in callNotesTemp)
                            {
                                PrintRow(c.CustomerId.ToString(), c.ParentCallNoteId.ToString(), c.Text);
                            }
                            PrintLine();

                            Console.WriteLine("Please Enter Issue ID");
                            int issueID = Convert.ToInt32(Console.ReadLine());
                            var callNoteIndex = callNotes.FindIndex(x => x.CallNoteId == customerID);

                            if (callNoteIndex < 0)
                            {
                                throw new Exception("Call Note not found");
                            }

                            Console.WriteLine("Please Enter Call Note Text");
                            string t1 = Console.ReadLine();

                            CallNote callNote1 = new CallNote(issueID, customerID, t1);
                            callNotes.Add(callNote1);

                            Console.WriteLine("Call Note Added Successfully");
                            break;

                        case 2:
                            Console.WriteLine("Please Enter Call Note Text");
                            string t2 = Console.ReadLine();

                            CallNote callNote2 = new CallNote(customerID, t2);
                            callNotes.Add(callNote2);

                            Console.WriteLine("Call Note Added Successfully");
                            break;

                        default:
                            Console.WriteLine("Please enter a valid choice");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return;
            }

        }

        void printMenu()
        {
            Console.WriteLine("\nCustomer Management System");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Search Customer");
            Console.WriteLine("3. Edit Customer");
            Console.WriteLine("4. Add customer from a Json file");
            Console.WriteLine("5. Add call note to a customer");
            Console.WriteLine("6. Exit");
        }

        public void run()
        {

            while (true)
            {
                printMenu();
                Console.WriteLine("\nEnter choice:");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            createCustomer();
                            break;

                        case 2:
                            searchCustomers();
                            break;

                        case 3:
                            editCustomer();
                            break;

                        case 4:
                            addCustomersFromJson();
                            break;

                        case 5:
                            addCallNote();
                            break;

                        case 6:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Please enter a valid input (1-5)");
                            break;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid input (1-5)");
                }
            }

        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.run();
        }
    }
}
