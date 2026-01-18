using ShoppingApp.Domain.Enums;

namespace ShoppingApp.Application.Features.Orders.DTOs
{
    public class OrderDto
    {
        public Ulid Id { get; set; }
        public Ulid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
