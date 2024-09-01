namespace FinanceTracker.DTOs
{
    public class UserLoginConfirmationDTO
    {
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }
}
