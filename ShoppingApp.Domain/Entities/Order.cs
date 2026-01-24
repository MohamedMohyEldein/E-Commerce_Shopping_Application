using ShoppingApp.Domain.Enums;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Domain.Entities
{
    public class Order
    {
        public Ulid Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        //public decimal TotalAmount { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
        public AppUser User { get; set; }
    }
}
