using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    class CallNote
    {

        public int CallNoteId { get; set; }

        public int ParentCallNoteId { get; set; }

        public int CustomerId { get; set; }
        
        public string Text { get; set; }
    }
}
