using ShoppingApp.Domain.Enums;

namespace ShoppingApp.Application.Features.Orders.DTOs
{
    public class UpdateOrderDto
    {
        public Ulid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
