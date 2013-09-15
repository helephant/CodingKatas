using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Potter
{
    [TestFixture]
    public class DiscountRateCardTests
    {
        [Test]
        public void NoDiscountGivenForSingleBookInSet()
        {
            AssertDiscountRate(1, 0);
        }

        [Test]
        public void DiscountGivenForMultipleBooksInSet()
        {
            AssertDiscountRate(2, 5);
            AssertDiscountRate(3, 10);
            AssertDiscountRate(4, 20);
            AssertDiscountRate(5, 25);
            AssertDiscountRate(6, 25);
            AssertDiscountRate(7, 25);
        }

        [Test]
        public void DiscountRateIsThePercentageOfTotalPriceToBePaid()
        {
            var rateCard = new DiscountRateCard();
            Assert.That(rateCard.CalculateRateToCharge(2), Is.EqualTo(0.95m));
        }

        [Test]
        public void PassingDiscountRateCardAnInvalidNumberOfBooksCausesException()
        {
            var rateCard = new DiscountRateCard();
            Assert.Throws<ArgumentException>(() => rateCard.CalculateDiscountRate(8));
        }

        private void AssertDiscountRate(int numberOfBooks, int expectedDiscountPercentage)
        {
            var rateCard = new DiscountRateCard();
            var discount = rateCard.CalculateDiscountRate(numberOfBooks);
            Assert.That(discount, Is.EqualTo(expectedDiscountPercentage));

            Debug.WriteLine("{0} books gives {1}% discount", numberOfBooks, expectedDiscountPercentage);
        }
    }
}