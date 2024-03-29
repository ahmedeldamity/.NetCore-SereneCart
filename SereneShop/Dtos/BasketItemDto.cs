﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero!")]
        public decimal Price { get; set; }

        public string ImageCover { get; set; }

        public string[] Images { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one item!")]
        public decimal Quantity { get; set; }

        public decimal RatingsAverage { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }
    }
}
