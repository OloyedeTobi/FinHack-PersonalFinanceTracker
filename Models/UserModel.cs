namespace FinanceTracker.Models
{
    public class UserComplete
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Gender { get; set; } = "";
        public string JobTitle { get; set; } = "";
        public bool Active {get; set; }
    }

}