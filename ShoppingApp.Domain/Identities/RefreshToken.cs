namespace ShoppingApp.Domain.Identities
{
    public class RefreshToken
    {
        public Ulid Id { get; set; }
        public Ulid UserId { get; set; }
        public string Token { get; set; } = null!;
        public bool IsRevoked { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public AppUser User { get; set; } = null!;
    }
}
