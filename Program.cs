using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DeveloperPracticalTest
{
    class Program
    {
        public List<Customer> customers = new List<Customer>();

        public void searchCustomers()
        {
            Console.WriteLine("Search customer by first name/last name:");
            string searchText = Console.ReadLine();

            List<Customer> searchResults = customers.FindAll(x => x.FirstName.Contains(searchText) || x.LastName.Contains(searchText));

            foreach (Customer c in searchResults)
            {
                Console.WriteLine($"\n{c.CustomerId}\t{c.FirstName} {c.LastName}");
            }
        }

        public void createCustomer()
        {

            string userName;
            string firstName;
            string lastName;
            string phoneNumber;
            DateTime dateOfBirth = new DateTime();

            bool invalidFlag = true;
            do
            {
                Console.WriteLine("\nEnter username:");
                userName = Console.ReadLine().Trim();

                Regex r = new Regex("^[a-zA-Z0-9]*$");
                if (userName == "" || userName.Length < 5 || userName.Length > 20 || !r.IsMatch(userName))
                {
                    Console.WriteLine("Invalid username: Should only contain letter or number, must between 5 and 20 characters");
                }
                else if (customers.Exists(x => x.Username == userName))
                {
                    Console.WriteLine("Username already taken");
                }
                else
                {
                    invalidFlag = false;
                }
            } while (invalidFlag);


            invalidFlag = true;
            do
            {
                Console.WriteLine("\nEnter first name:");
                firstName = Console.ReadLine().Trim();
                Regex r = new Regex("^[0-9]*$");
                if (firstName == "" || r.IsMatch(firstName))
                {
                    Console.WriteLine("Invalid firstName: Cannot be blank and no numbers");
                }
                else
                {
                    invalidFlag = false;
                }
            } while (invalidFlag);

            invalidFlag = true;
            do
            {
                Console.WriteLine("\nEnter last name:");
                lastName = Console.ReadLine().Trim();
                Regex r = new Regex("^[0-9]*$");
                if (lastName == "" || r.IsMatch(lastName))
                {
                    Console.WriteLine("Invalid lastName: Cannot be blank and no numbers");
                }
                else
                {
                    invalidFlag = false;
                }
            } while (invalidFlag);

            invalidFlag = true;
            do
            {
                Console.WriteLine("\nEnter phone number:");
                phoneNumber = Console.ReadLine().Trim();
                Regex r = new Regex("^[0-9]*$");
                if (phoneNumber == "" || !r.IsMatch(phoneNumber))
                {
                    Console.WriteLine("Invalid phoneNumber: Cannot be blank and only valid Australian landline or mobile numbers");
                }
                else
                {
                    invalidFlag = false;
                }
            } while (invalidFlag);

            invalidFlag = true;
            do
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
                    if (dateOfBirth.Subtract(new DateTime()).TotalDays > 40150 || dateOfBirth.Subtract(new DateTime()).TotalDays <= 0)
                    {
                        Console.WriteLine("Age cannot be more than 110 years");
                    }
                    else
                    {
                        invalidFlag = false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Date Format");
                }
            } while (invalidFlag);

            Customer customer = new Customer(
                userName,
                firstName,
                lastName,
                phoneNumber,
                dateOfBirth
                );

            Console.WriteLine($"A customer with ID({customer.CustomerId}) and username({userName}) created");
            customers.Add(customer);

        }
        static void Main(string[] args)
        {

            Program run = new Program();

            while (true)
            {

                Console.WriteLine("\nCustomer Management System");
                Console.WriteLine("1. Add new customer");
                Console.WriteLine("2. Search Customer");
                Console.WriteLine("3. Edit Customer");
                Console.WriteLine("4. Add customer from a Json file");
                Console.WriteLine("5. Exit");

                Console.WriteLine("\nEnter choice:");
                int choice;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            run.createCustomer();
                            break;

                        case 2:
                            run.searchCustomers();
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
    }
}
