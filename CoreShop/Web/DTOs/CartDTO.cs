using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.DTOs
{
    public class CartDTO
    {
        [Required]
        public string Id { get; set; }

        public List<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();
    }
}
