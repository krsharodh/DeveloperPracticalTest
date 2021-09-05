using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    class CallNote
    {        
        public static int CallNoteIdSeed { get; set; } = 1;

        public int CallNoteId { get; set; }

        public int ?ParentCallNoteId { get; set; }

        public int CustomerId { get; set; }
        
        public string Text { get; set; }

        public CallNote(int parentCallNoteId, int customerId, string text)
        {
            CallNoteId = CallNoteIdSeed;
            CallNoteIdSeed++;
            ParentCallNoteId = parentCallNoteId;
            CustomerId = customerId;
            Text = text;
        }

        public CallNote(int customerId, string text)
        {
            CallNoteId = CallNoteIdSeed;
            CallNoteIdSeed++;
            ParentCallNoteId = null;
            CustomerId = customerId;
            Text = text;
        }
    }
}
