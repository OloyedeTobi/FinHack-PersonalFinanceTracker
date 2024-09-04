namespace FinanceTracker.DTOs
{
    public class UserLoginConfirmationDTO
    {
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
    }
}
