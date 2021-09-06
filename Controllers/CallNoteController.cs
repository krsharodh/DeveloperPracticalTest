using Serilog;
using System;
using System.Collections.Generic;

namespace DeveloperPracticalTest
{
    /// <summary>
    /// Handles various functionalities of call note management
    /// </summary>
    class CallNoteController
    {
        CustomerController CustomerHandler;
        List<CallNoteModel> callNotes = new List<CallNoteModel>();

        /// <summary>
        /// Call Note Controller Constructor
        /// </summary>
        /// <param name="customerHandler"></param>
        public CallNoteController (CustomerController customerHandler)
        {
            CustomerHandler = customerHandler;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        /// <summary>
        /// Prints menu of the Call Note functionality
        /// </summary>
        void printMenu()
        {
            Console.WriteLine("\nPlease select one of the following");
            Console.WriteLine("1. Existing Issue");
            Console.WriteLine("2. New Issue");
        }

        /// <summary>
        /// Prints Call Notes for the customer in the form of a table
        /// </summary>
        /// <param name="customer">Customer object for which call notes need to printed</param>
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

        /// <summary>
        /// Gets call note ID as input from the user and validates it
        /// </summary>
        /// <returns>Valid Call Note ID</returns>
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

        /// <summary>
        /// Gets valid call note text as a input from the user
        /// </summary>
        /// <returns>Valid Call Note text</returns>
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

        /// <summary>
        /// Entry point for adding new call note
        /// </summary>
        public void addCallNote()
        {
            CustomerModel customer = CustomerHandler.getCustomerData();
            
            while (true)
            {
                try
                {  
                    printMenu();
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Log.Information($"User selects option {choice}");

                    switch (choice)
                    {
                        // Add Call Note to Existing Issue
                        case 1:
                            printCallNotes(customer);
                            int callNoteID = getCallNoteID();
                            callNotes.Add(new CallNoteModel(callNoteID, customer.CustomerId, getCallNoteText()));
                            break;

                        // Adds Call Note as New Issue
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
