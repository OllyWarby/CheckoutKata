using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    { 
        private readonly List<Item> basket;

        public Checkout() 
        {
            basket = new List<Item>(); 
        } 

        public decimal CalulateTotal(List<Item> basket)
        {
            return 0m;
        }

        public void ScanItem(Item item)
        {
           basket.Add(item);
        }
        //Added this in after writing the first unit test to return the total items for the Assert as the basket is private
        public int TotalItemCount()
        {
            return basket.Count;    
        }
    }

    interface ICheckout
    {
        void ScanItem(Item item);
        decimal CalulateTotal(List<Item> basket);
    }

    public class Item
    {
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
    }


}
