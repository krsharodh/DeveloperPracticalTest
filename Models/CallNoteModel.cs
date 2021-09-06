using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    /// <summary>
    /// Call Note Model
    /// </summary>
    class CallNoteModel : ICallNoteModel
    {
        public static int CallNoteIdSeed { get; set; } = 1;

        public int CallNoteId { get; set; }

        public int? ParentCallNoteId { get; set; }

        public int CustomerId { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Creates a call Note
        /// </summary>
        /// <param name="parentCallNoteId">Parent Call Note ID</param>
        /// <param name="customerId">Customer ID</param>
        /// <param name="text">Call Note Text</param>
        public CallNoteModel(int parentCallNoteId, int customerId, string text)
        {
            CallNoteId = CallNoteIdSeed;
            CallNoteIdSeed++;
            ParentCallNoteId = parentCallNoteId;
            CustomerId = customerId;
            Text = text;
        }

        /// <summary>
        /// Creates a Call Note
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <param name="text">Call Note Text</param>
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
