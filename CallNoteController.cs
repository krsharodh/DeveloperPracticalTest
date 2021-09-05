using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    class CallNoteController
    {
        List<CallNote> callNotes = new List<CallNote>();

        public void addCallNote(List<Customer> customers)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the customer ID:");
                    int customerID = Convert.ToInt32(Console.ReadLine());
                    var customerIndex = customers.FindIndex(x => x.CustomerId == customerID);
                    if (customerIndex < 0)
                    {
                        throw new Exception("Customer not found !");
                    }

                    Console.WriteLine("\nPlease select one of the following");
                    Console.WriteLine("1. Existing Issue");
                    Console.WriteLine("2. New Issue");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"Existing Call Notes for {customers[customerIndex].FirstName} {customers[customerIndex].LastName}");
                            List<CallNote> callNotesTemp = callNotes.FindAll(x => x.CustomerId == customerID);

                            TableUtil.PrintLine();
                            TableUtil.PrintRow("Call Note ID", "Parent Call Note ID", "Text");
                            TableUtil.PrintLine();
                            foreach (CallNote c in callNotesTemp)
                            {
                                TableUtil.PrintRow(c.CustomerId.ToString(), c.ParentCallNoteId.ToString(), c.Text);
                            }
                            TableUtil.PrintLine();

                            Console.WriteLine("Please Enter Issue ID");
                            int issueID = Convert.ToInt32(Console.ReadLine());
                            var callNoteIndex = callNotes.FindIndex(x => x.CallNoteId == customerID);

                            if (callNoteIndex < 0)
                            {
                                throw new Exception("Call Note not found");
                            }

                            Console.WriteLine("Please Enter Call Note Text");
                            string t1 = Console.ReadLine();

                            CallNote callNote1 = new CallNote(issueID, customerID, t1);
                            callNotes.Add(callNote1);

                            Console.WriteLine("Call Note Added Successfully");
                            break;

                        case 2:
                            Console.WriteLine("Please Enter Call Note Text");
                            string t2 = Console.ReadLine();

                            CallNote callNote2 = new CallNote(customerID, t2);
                            callNotes.Add(callNote2);

                            Console.WriteLine("Call Note Added Successfully");
                            break;

                        default:
                            Console.WriteLine("Please enter a valid choice");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return;
            }

        }
    }
}
