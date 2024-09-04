namespace FinanceTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = "";
        public DateTime Date { get; set; }
        public string Category { get; set; } = "";
        public bool IsRecurring { get; set; }
        public string Frequency { get; set; } = "";

        public required Account Account { get; set; } 
    }
}
