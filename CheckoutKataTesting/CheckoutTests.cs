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
            Item apple = new Item { SKU = "A99", UnitPrice = 0.5m };

            //Act
            checkout.ScanItem(apple);

            //Assert
            int totalExpextedItems = checkout.TotalItemCount();
            Assert.AreEqual(totalExpextedItems, 1);
        }

        [TestMethod]
        public void CheckTotalCalculationWithoutSpecialOffers()
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

        [TestMethod]
        public void CheckTotalCalculationWithOneSpecialOfferItem()
        {
            //Arrange
            Checkout checkout = new Checkout();
            Item apple = new Item { SKU = "A99", UnitPrice = 0.5m };
            ItemSpecialOffer appleOffer = new ItemSpecialOffer { NewUnitPrice = 1.3m, Quantity = 3, SKU = "A99" };

            //Act
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.AddSpecialOffer(appleOffer);

            //Assert
            decimal expectedPrice = 1.3m;
            Assert.AreEqual(checkout.CalulateTotal(), expectedPrice);

        }
        [TestMethod]
        public void CheckTotalCalculationswithOneSpecialOfferItemPlusOne()
        {
            //Arrange
            Checkout checkout = new Checkout();
            Item apple = new Item { SKU = "A99", UnitPrice = 0.5m };
            ItemSpecialOffer appleOffer = new ItemSpecialOffer { NewUnitPrice = 1.3m, Quantity = 3, SKU = "A99" };

            //Act
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.AddSpecialOffer(appleOffer);

            //Assert
            decimal expectedPrice = 1.8m;
            Assert.AreEqual(checkout.CalulateTotal(), expectedPrice);
        }
        [TestMethod]
        public void CheckTotalCalculationsWithMultipleItemsAndSpecialOffers()
        {
            Checkout checkout = new Checkout();
            Item apple = new Item { SKU = "A99", UnitPrice = 0.5m };
            Item biscuits = new Item { SKU = "B15", UnitPrice = 0.3m };
            ItemSpecialOffer appleOffer = new ItemSpecialOffer { NewUnitPrice = 1.3m, Quantity = 3, SKU = "A99" };
            ItemSpecialOffer biscuitsOffer = new ItemSpecialOffer { NewUnitPrice = 0.45m, Quantity = 2, SKU = "B15" };

            //Act
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(biscuits);
            checkout.ScanItem(biscuits);
            checkout.AddSpecialOffer(appleOffer);
            checkout.AddSpecialOffer(biscuitsOffer);

            //Assert
            decimal expectedPrice = 1.75m;
            Assert.AreEqual(checkout.CalulateTotal(), expectedPrice);

        }

        [TestMethod]
        public void CheckCalculationsWithMultipleSpecialOffersAndItemsWithout()
        {
            Checkout checkout = new Checkout();
            Item apple = new Item { SKU = "A99", UnitPrice = 0.5m };
            Item biscuits = new Item { SKU = "B15", UnitPrice = 0.3m };
            Item carrot = new Item { SKU = "C40", UnitPrice = 0.6m };
            ItemSpecialOffer appleOffer = new ItemSpecialOffer { NewUnitPrice = 1.3m, Quantity = 3, SKU = "A99" };
            ItemSpecialOffer biscuitsOffer = new ItemSpecialOffer { NewUnitPrice = 0.45m, Quantity = 2, SKU = "B15" };

            //Act
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(apple);
            checkout.ScanItem(biscuits);
            checkout.ScanItem(biscuits);
            checkout.ScanItem(biscuits);
            checkout.ScanItem(carrot);
            checkout.ScanItem(carrot);
            checkout.AddSpecialOffer(appleOffer);
            checkout.AddSpecialOffer(biscuitsOffer);

            //Assert
            decimal expectedPrice = 3.25m;
            Assert.AreEqual(checkout.CalulateTotal(), expectedPrice);
        }




    }
}
