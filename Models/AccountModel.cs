namespace FinanceTracker.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public decimal Balance { get; set; }
        public string AccountType { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }
}
