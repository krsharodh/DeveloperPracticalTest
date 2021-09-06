using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    class CallNoteModel : ICallNoteModel
    {
        public static int CallNoteIdSeed { get; set; } = 1;

        public int CallNoteId { get; set; }

        public int? ParentCallNoteId { get; set; }

        public int CustomerId { get; set; }

        public string Text { get; set; }

        public CallNoteModel(int parentCallNoteId, int customerId, string text)
        {
            CallNoteId = CallNoteIdSeed;
            CallNoteIdSeed++;
            ParentCallNoteId = parentCallNoteId;
            CustomerId = customerId;
            Text = text;
        }

        public CallNoteModel(int customerId, string text)
        {
            CallNoteId = CallNoteIdSeed;
            CallNoteIdSeed++;
            ParentCallNoteId = null;
            CustomerId = customerId;
            Text = text;
        }
    }
}
