namespace DeveloperPracticalTest
{
    interface ICallNoteModel
    {
        int CallNoteId { get; set; }
        int CustomerId { get; set; }
        int? ParentCallNoteId { get; set; }
        string Text { get; set; }
    }
}