﻿using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Domain.Entities
{
    public class Cart
    {
        public Ulid Id { get; set; }
        public Ulid UserId { get; set; }
        public ICollection<CartItem>? Items { get; set; }
        public AppUser User { get; set; }
    }
}
