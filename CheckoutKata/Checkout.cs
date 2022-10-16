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

        public decimal CalulateTotal()
        {
            decimal total = basket.Sum(x => x.UnitPrice);
            return total;   
        }

        public void ScanItem(Item item)
        {
           basket.Add(item);
        }
        
        public int TotalItemCount()
        {
            return basket.Count;    
        }
    }

    interface ICheckout
    {
        void ScanItem(Item item);
        decimal CalulateTotal();
    }

    public class Item
    {
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
    }


}
