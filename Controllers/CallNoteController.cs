using Serilog;
using System;
using System.Collections.Generic;

namespace DeveloperPracticalTest
{
    class CallNoteController
    {
        CustomerController CustomerHandler;
        List<CallNoteModel> callNotes = new List<CallNoteModel>();

        public CallNoteController (CustomerController customerHandler)
        {
            CustomerHandler = customerHandler;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        CustomerModel getCustomerData()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the customer ID:");
                    int customerID = Convert.ToInt32(Console.ReadLine());

                    CustomerModel customer = CustomerHandler.getCustomerByID(customerID);
                    if (customer == null)
                    {
                        throw new Exception("Customer not found !");
                    }
                    return customer;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
                } 
            }
        }

        void printMenu()
        {
            Console.WriteLine("\nPlease select one of the following");
            Console.WriteLine("1. Existing Issue");
            Console.WriteLine("2. New Issue");
        }

        void printCallNotes (CustomerModel customer)
        {
            Console.WriteLine($"Existing Call Notes for {customer.FirstName} {customer.LastName}");
            List<CallNoteModel> callNotesTemp = callNotes.FindAll(x => x.CustomerId == customer.CustomerId);

            TableUtil tableUtil = new TableUtil();

            tableUtil.PrintLine();
            tableUtil.PrintRow("Call Note ID", "Parent Call Note ID", "Text");
            tableUtil.PrintLine();
            foreach (CallNoteModel c in callNotesTemp)
            {
                tableUtil.PrintRow(c.CallNoteId.ToString(), c.ParentCallNoteId.ToString(), c.Text);
            }
            tableUtil.PrintLine();
        }

        public int getCallNoteID()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please Enter Issue ID");
                    int issueID = Convert.ToInt32(Console.ReadLine());
                    CallNoteModel callNote = callNotes.Find(x => x.CallNoteId == issueID);

                    if (callNote == null)
                    {
                        throw new Exception("Call Note not found");
                    }
                    return issueID;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
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
                    Log.Information(ex.Message);
                }
            }
        }

        public void addCallNote()
        {
            CustomerModel customer = getCustomerData();
            
            while (true)
            {
                try
                {  
                    printMenu();
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Log.Information($"User selects option {choice}");

                    switch (choice)
                    {
                        case 1:
                            printCallNotes(customer);
                            int callNoteID = getCallNoteID();
                            callNotes.Add(new CallNoteModel(callNoteID, customer.CustomerId, getCallNoteText()));
                            break;

                        case 2:
                            CallNoteModel callNote2 = new CallNoteModel(customer.CustomerId, getCallNoteText());
                            callNotes.Add(callNote2);
                            break;

                        default:
                            throw new Exception("Please enter a valid choice");
                    }
                    Log.Information("Call Note Added Successfully");
                    return;
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
                }
            }
        }
    }
}
