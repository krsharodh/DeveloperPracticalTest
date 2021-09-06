using System;

namespace DeveloperPracticalTest
{
    interface ICustomerModel
    {
        int CustomerId { get; set; }
        DateTime DateOfBirth { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Username { get; set; }

        bool editCustomer(string username, string firstName, string lastName, string phoneNumber, DateTime dateOfBirth);
        string ToString();
    }
}