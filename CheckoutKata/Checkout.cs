using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    { 
        private readonly List<Item> basket;
        private readonly List<ItemSpecialOffer> specialOffers;
        private CalculateTotalWithSpecialOffers calculateTotalWithSpecialOffers;

        public Checkout() 
        {
            basket = new List<Item>(); 
            specialOffers = new List<ItemSpecialOffer>();   
            calculateTotalWithSpecialOffers = new CalculateTotalWithSpecialOffers();
        } 

        public decimal CalulateTotal()
        {
            decimal totalCost = calculateTotalWithSpecialOffers.CalculateTotalPriceWithSpecialOffers(basket, specialOffers);

            return totalCost;
        }

        public void ScanItem(Item item)
        {
           basket.Add(item);
        }

        //this doesn't necessarily need to be in, its mainly for testing purposes as the special offers will come from data access usually.
        public void AddSpecialOffer(ItemSpecialOffer specialOffer)
        {
            specialOffers.Add(specialOffer);
        }
        public int TotalItemCount()
        {
            return basket.Count;    
        }
    }

    public class CalculateTotalWithSpecialOffers: ICaluculateSpecialOffers
    {
        
        public decimal CalculateTotalPriceWithSpecialOffers(List<Item> basket, List<ItemSpecialOffer> itemSpecialOffers)
        {

            //The easiest way I could think to do this would be to get a total count of each SKU in the basket then apply the logic to that
            Dictionary<string, int> groupedTotalsBySKU = new Dictionary<string, int>(basket.GroupBy(x => x.SKU).ToDictionary(y => y.Key, y => y.Count()));
            decimal totalPriceWithSpecialOffers = 0;

            foreach (var item in groupedTotalsBySKU)
            { 
                var unitPrice = basket.Where(x => x.SKU == item.Key).Select(x => x.UnitPrice).FirstOrDefault();
                int quantity = groupedTotalsBySKU.Where(x => x.Key == item.Key).Select(x => x.Value).FirstOrDefault();
                var specialOffer = (itemSpecialOffers.Where(x => x.SKU == item.Key).FirstOrDefault() ?? new ItemSpecialOffer { NewUnitPrice = 0m, Quantity = 0, SKU = item.Key });
                totalPriceWithSpecialOffers += ProcessSpecialOffers(unitPrice, quantity, specialOffer);
            }
            return totalPriceWithSpecialOffers;
        }

        private decimal ProcessSpecialOffers(decimal unitPrice, int quantity, ItemSpecialOffer specialOffer)
        {
            decimal totalPricebySKU = 0;
            if (quantity < specialOffer.Quantity || specialOffer.Quantity == 0)
            {
                totalPricebySKU += (quantity * unitPrice);
            }
            else if (quantity >= specialOffer.Quantity && specialOffer.Quantity != 0)
            {
                int nonSpecialOfferItemsCount = quantity % specialOffer.Quantity;
                totalPricebySKU += (nonSpecialOfferItemsCount * unitPrice);
                int specialOfferItemsCount = quantity - nonSpecialOfferItemsCount;
                totalPricebySKU += (specialOfferItemsCount/specialOffer.Quantity * specialOffer.NewUnitPrice);
            }
            return totalPricebySKU;

        }
    }
    interface ICaluculateSpecialOffers
    {
       decimal CalculateTotalPriceWithSpecialOffers(List<Item> basket, List<ItemSpecialOffer> specialOffers);
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

    public class ItemSpecialOffer
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal NewUnitPrice { get; set; }
    }


}
