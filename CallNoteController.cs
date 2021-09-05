using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    class CallNoteController
    {
        CustomerController CustomerHandler;

        public CallNoteController (CustomerController customerHandler)
        {
            CustomerHandler = customerHandler;
        }
        List<CallNote> callNotes = new List<CallNote>();

        Customer getCustomerData()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the customer ID:");
                    int customerID = Convert.ToInt32(Console.ReadLine());

                    Customer customer = CustomerHandler.getCustomerByID(customerID);
                    if (customer == null)
                    {
                        throw new Exception("Customer not found !");
                    }
                    return customer;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                } 
            }
        }

        void printMenu()
        {
            Console.WriteLine("\nPlease select one of the following");
            Console.WriteLine("1. Existing Issue");
            Console.WriteLine("2. New Issue");
        }

        void printCallNotes (Customer customer)
        {
            Console.WriteLine($"Existing Call Notes for {customer.FirstName} {customer.LastName}");
            List<CallNote> callNotesTemp = callNotes.FindAll(x => x.CustomerId == customer.CustomerId);

            TableUtil.PrintLine();
            TableUtil.PrintRow("Call Note ID", "Parent Call Note ID", "Text");
            TableUtil.PrintLine();
            foreach (CallNote c in callNotesTemp)
            {
                TableUtil.PrintRow(c.CallNoteId.ToString(), c.ParentCallNoteId.ToString(), c.Text);
            }
            TableUtil.PrintLine();
        }

        public int getCallNoteID()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please Enter Issue ID");
                    int issueID = Convert.ToInt32(Console.ReadLine());
                    CallNote callNote = callNotes.Find(x => x.CallNoteId == issueID);

                    if (callNote == null)
                    {
                        throw new Exception("Call Note not found");
                    }
                    return issueID;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string getCallNoteText()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please Enter Call Note Text");
                    return Console.ReadLine().Trim();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void addCallNote()
        {
            Customer customer = getCustomerData();
            
            while (true)
            {
                try
                {  
                    printMenu();
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            printCallNotes(customer);
                            int callNoteID = getCallNoteID();
                            callNotes.Add(new CallNote(callNoteID, customer.CustomerId, getCallNoteText()));
                            break;

                        case 2:
                            CallNote callNote2 = new CallNote(customer.CustomerId, getCallNoteText());
                            callNotes.Add(callNote2);
                            break;

                        default:
                            throw new Exception("Please enter a valid choice");
                    }
                    Console.WriteLine("Call Note Added Successfully");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
