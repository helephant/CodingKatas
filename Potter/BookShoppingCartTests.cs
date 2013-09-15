using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Potter
{
    [TestFixture]
    public class BookShoppingCartTests
    {
        [Test]
        public void NoDiscountGivenForASingleBook()
        {
            var books = new List<HarryPotterBook> {HarryPotterBook.PhilosopherStone};
            AssertBookPrice(books, BookSeriesSet.BasePrice);
        }

        [Test]
        public void DiscountGivenForMultipleBooksInTheSameSet()
        {
            var books = new List<HarryPotterBook>
                            {
                                HarryPotterBook.PhilosopherStone,
                                HarryPotterBook.ChamberOfSecrets,
                                HarryPotterBook.PrisonerOfAzkaban,
                                HarryPotterBook.GobletOfFire,
                                HarryPotterBook.OrderOfThePhoenix,
                                HarryPotterBook.HalfBloodPrince,
                                HarryPotterBook.DeathlyHallows,
                            };

            AssertBookPrice(books, 42);
        }

        [Test]
        public void NoDiscountForTwoBooksThatAreTheSame()
        {
            var books = new List<HarryPotterBook>
                            {
                                HarryPotterBook.PhilosopherStone,
                                HarryPotterBook.PhilosopherStone,
                            };

            AssertBookPrice(books, 16);
        }

        [Test]
        public void DiscountAppliesOnlyForBooksInTheSameSet()
        {
            var books = new List<HarryPotterBook>
                                {
                                    HarryPotterBook.PhilosopherStone,
                                    HarryPotterBook.PhilosopherStone,
                                    HarryPotterBook.ChamberOfSecrets,
                                };

            AssertBookPrice(books, 23.2m);
        }

        [Test]
        public void SetsAreOptimizedForCheapestCombination()
        {
            var books = new List<HarryPotterBook>
                            {
                                HarryPotterBook.PhilosopherStone,
                                HarryPotterBook.PhilosopherStone,
                                HarryPotterBook.ChamberOfSecrets,
                                HarryPotterBook.ChamberOfSecrets,
                                HarryPotterBook.GobletOfFire,
                                HarryPotterBook.GobletOfFire,
                                HarryPotterBook.PrisonerOfAzkaban,
                                HarryPotterBook.OrderOfThePhoenix,
                            };

            AssertBookPrice(books, 51.20m);
        }

        [Test]
        public void SetOptimizationIsNotOrderDependant()
        {
            var books = new List<HarryPotterBook>
                            {
                                HarryPotterBook.PrisonerOfAzkaban,
                                HarryPotterBook.OrderOfThePhoenix,
                                HarryPotterBook.PhilosopherStone,
                                HarryPotterBook.PhilosopherStone,
                                HarryPotterBook.ChamberOfSecrets,
                                HarryPotterBook.ChamberOfSecrets,
                                HarryPotterBook.GobletOfFire,
                                HarryPotterBook.GobletOfFire,
                            };

            AssertBookPrice(books, 51.20m);
        }

        private void AssertBookPrice(IList<HarryPotterBook> books, decimal price)
        {
            var cart = new BookShoppingCart(books);
            var total = cart.CalculatePrice();

            Debug.WriteLine("Books: " + string.Join(", ", books));
            Debug.WriteLine("Price: " + total);

            Assert.That(total, Is.EqualTo(price));
        }
    }
}