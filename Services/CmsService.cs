using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace DeveloperPracticalTest
{
    class CmsService : ICmsService
    {
        private readonly ILogger<CmsService> Log;
        private readonly IConfiguration Config;

        public CmsService(ILogger<CmsService> log, IConfiguration config)
        {
            Log = log;
            Config = config;
        }
        void PrintMenu()
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
            CustomerController customer = new CustomerController();
            CallNoteController callNote = new CallNoteController(customer);

            while (true)
            {
                PrintMenu();
                Console.WriteLine("\nEnter choice:");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Log.LogInformation($"User Selects choice: {choice}");
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
                            Log.LogInformation("Exiting application");
                            break;

                        default:
                            Log.LogInformation($"Please enter a valid input (1-5)");
                            break;
                    }

                }
                catch (Exception)
                {
                    Log.LogInformation($"Please enter a valid input (1-5)");
                }
            }

        }
    }
}
