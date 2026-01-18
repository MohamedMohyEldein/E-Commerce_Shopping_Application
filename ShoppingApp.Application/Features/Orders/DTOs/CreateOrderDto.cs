using ShoppingApp.Domain.Enums;

namespace ShoppingApp.Application.Features.Orders.DTOs
{
    public class CreateOrderDto
    {
        public Ulid UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
