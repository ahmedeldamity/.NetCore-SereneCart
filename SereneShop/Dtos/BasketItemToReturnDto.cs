﻿namespace API.Dtos
{
    public class BasketItemToReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageCover { get; set; }

        public string[] Images { get; set; }

        public decimal Quantity { get; set; }

        public decimal RatingsAverage { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }
    }
}
