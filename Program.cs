using System;

namespace DeveloperPracticalTest
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                Console.WriteLine("Customer Management System");
                Console.WriteLine("1. Add new customer");
                Console.WriteLine("2. Search Customer");
                Console.WriteLine("3. Edit Customer");
                Console.WriteLine("4. Add customer from a Json file");
                Console.WriteLine("5. Exit");

                Console.WriteLine("\nEnter choice:");
                int choice;
                try
                {
                    choice = Convert.ToInt32(Console.ReadKey());
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.WriteLine("\nEnter username:");
                                Console.WriteLine("\nEnter first name:");
                                Console.WriteLine("\nEnter last name:");
                                Console.WriteLine("\nEnter phone number:");
                                Console.WriteLine("\nEnter date of birth (yyyy,mm,dd):");
                                break;
                            }


                        default:
                            break;
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please enter a valid input (1-5)");
                }

     
            }


            Customer customer = new Customer("sharodh", "Sharodh", "KR", "0491957363", new DateTime(1996, 09, 03));

            Console.WriteLine(customer.ToString());

        }
    }
}
