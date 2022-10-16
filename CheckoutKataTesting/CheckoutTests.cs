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
            Item item = new Item{ SKU = "A99", UnitPrice = 0.5m};

            //Act
            checkout.ScanItem(item);

            //Assert
            int totalExpextedItems = checkout.TotalItemCount();
            Assert.AreEqual(totalExpextedItems, 1);
        }
    }
}
