using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CheckoutKata;


namespace CheckoutKataTesting
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void CheckAddItemToBasket()
        {
            //Arrange
            Checkout checkout = new Checkout();
            Item apple = new Item{ SKU = "A99", UnitPrice = 0.5m};

            //Act
            checkout.ScanItem(apple);

            //Assert
            int totalExpextedItems = checkout.TotalItemCount();
            Assert.AreEqual(totalExpextedItems, 1);
        }

        [TestMethod]
        public void CheckTotalCalculation()
        {
            //Arrange
            Checkout checkout = new Checkout();
            Item apple = new Item { SKU = "A99", UnitPrice = 0.5m };
            Item biscuits = new Item { SKU = "B15", UnitPrice = 0.3m };
            Item carrot = new Item { SKU = "C40", UnitPrice = 0.6m };

            //Act
            checkout.ScanItem(apple);
            checkout.ScanItem(biscuits);
            checkout.ScanItem(carrot);

            //Assert
            decimal expectedPrice = 1.4m;
            Assert.AreEqual(checkout.CalulateTotal(), expectedPrice);
        }


    }
}
