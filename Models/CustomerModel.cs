using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    /// <summary>
    /// Customer Model
    /// </summary>
    public class CustomerModel : ICustomerModel
    {
        public static int CustomerIdSeed { get; set; } = 1;
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Creates customer with default values
        /// </summary>
        public CustomerModel()
        {
            CustomerId = 0;
            Username = "";
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            DateOfBirth = new DateTime();
        }

        /// <summary>
        /// Creates new Customer
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="firstName">Firstname</param>
        /// <param name="lastName">Lastname</param>
        /// <param name="phoneNumber">Phonenumber</param>
        /// <param name="dateOfBirth">DateTime Object</param>
        public CustomerModel(string username, string firstName, string lastName, string phoneNumber, DateTime dateOfBirth)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            CustomerId = CustomerIdSeed;
            CustomerIdSeed++;
        }

        /// <summary>
        /// Edits Customer Data
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="firstName">Firstname</param>
        /// <param name="lastName">Lastname</param>
        /// <param name="phoneNumber">Phonenumber</param>
        /// <param name="dateOfBirth">DateTime Object</param>
        /// <returns></returns>
        public bool editCustomer(string username, string firstName, string lastName, string phoneNumber, DateTime dateOfBirth)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            return true;
        }

        /// <summary>
        /// Prints customer in the form a string
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString()
        {
            return
                $"Customer ID: {CustomerId}\n" +
                $"Username: {Username}\n" +
                $"First Name: {FirstName}\n" +
                $"Last Name: {LastName}\n" +
                $"Phone Number: {PhoneNumber}\n" +
                $"Date Of Birth: {DateOfBirth.ToShortDateString()}";
        }

    }
}
