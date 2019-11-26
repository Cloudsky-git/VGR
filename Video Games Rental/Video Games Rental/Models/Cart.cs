using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Video_Games_Rental.Models
{
    public class Cart
    {

        public game Product { get; set; }

        public int Quantity { get; set; }

        public Cart(game product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}