﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    public class CustomerModel : ICustomerModel
    {
        public static int CustomerIdSeed { get; set; } = 1;
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public CustomerModel()
        {
            CustomerId = 0;
            Username = "";
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            DateOfBirth = new DateTime();
        }

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

        public bool editCustomer(string username, string firstName, string lastName, string phoneNumber, DateTime dateOfBirth)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            return true;
        }

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