using System;

namespace DeveloperPracticalTest
{
    class Program
    {
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

        public void runCustomerManagementSystem()
        {
            CustomerController customer = new CustomerController();
            CallNoteController callNote = new CallNoteController(customer);

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
                            customer.createCustomer();
                            break;

                        case 2:
                            customer.searchCustomers();
                            break;

                        case 3:
                            customer.editCustomer();
                            break;

                        case 4:
                            customer.addCustomersFromJson();
                            break;

                        case 5:
                            callNote.addCallNote();
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
            program.runCustomerManagementSystem();
        }
    }
}
