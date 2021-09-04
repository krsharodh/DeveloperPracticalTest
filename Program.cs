using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DeveloperPracticalTest
{
    class Program
    {
        List<Customer> customers = new List<Customer>();
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

        void createCustomer()
        {
            // Inputs required to create a customer
            string userName;
            string firstName;
            string lastName;
            string phoneNumber;
            DateTime dateOfBirth = new DateTime();


            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter username:");
                    userName = Console.ReadLine().Trim();

                    Regex r = new Regex("^[a-zA-Z0-9]*$");

                    if (userName == "" || userName.Length < 5 || userName.Length > 20 || !r.IsMatch(userName))
                    {
                        throw new Exception("Invalid username: Should only contain letter or number, must between 5 and 20 characters");
                    }
                    else if (customers.Exists(x => x.Username == userName))
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



            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter first name:");
                    firstName = Console.ReadLine().Trim();
                    Regex r = new Regex("^[0-9]*$");
                    if (firstName == "" || r.IsMatch(firstName))
                    {
                        throw new Exception("Invalid firstName: Cannot be blank and no numbers");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message ?? "Invalid first name");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter last name:");
                    lastName = Console.ReadLine().Trim();
                    Regex r = new Regex("^[0-9]*$");
                    if (lastName == "" || r.IsMatch(lastName))
                    {
                        throw new Exception("Invalid lastName: Cannot be blank and no numbers");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message ?? "Invalid last name");
                }
            }


            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter phone number:");
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


            while (true)
            {
                try
                {

                    Console.WriteLine("\nEnter Year (dd/mm/yyyy):");
                    string[] dob = Console.ReadLine().Split('/');
                    Regex r = new Regex("^[0-9]*$");
                    dateOfBirth = new DateTime(
                        Int32.Parse(dob[2]),
                        Int32.Parse(dob[1]),
                        Int32.Parse(dob[0])
                        );
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

            Customer customer = new Customer(
                userName,
                firstName,
                lastName,
                phoneNumber,
                dateOfBirth
                );

            Console.WriteLine($"A customer with ID {customer.CustomerId} and username {userName} created");
            customers.Add(customer);

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

        void printMenu()
        {
            Console.WriteLine("\nCustomer Management System");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Search Customer");
            Console.WriteLine("3. Edit Customer");
            Console.WriteLine("4. Add customer from a Json file");
            Console.WriteLine("5. Exit");
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

                        case 5:
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
