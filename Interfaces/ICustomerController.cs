using System;

namespace DeveloperPracticalTest
{
    public interface ICustomerController
    {
        void addCustomersFromJson();
        void createCustomer();
        void editCustomer();
        CustomerModel getCustomerByID(int id);
        DateTime getDateUtil(string dateString);
        void searchCustomers();
    }
}