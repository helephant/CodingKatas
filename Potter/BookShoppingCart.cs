using System.Collections.Generic;
using System.Linq;
using Potter.BookSetStrategies;

namespace Potter
{
    public class BookShoppingCart
    {
        private readonly IEnumerable<HarryPotterBook> _books;

        public BookShoppingCart(IEnumerable<HarryPotterBook> books)
        {
            _books = books;
        }

        public decimal CalculatePrice()
        {
            var sets = CreateSets(_books);

            var total = sets.Sum(set => set.CalculatePriceForSet());
            return total;
        }

        private IEnumerable<BookSeriesSet> CreateSets(IEnumerable<HarryPotterBook> books)
        {
            var setDistributionStrategy = new AvoidIncompleteSetsStrategy();
            var sets = new List<BookSeriesSet>();

            // find the set with the smallest number of books which doesn't already have this book
            foreach (var book in books.OrderBy(book => book))
            {
                var setToUpdate = setDistributionStrategy.FindSetForBook(sets, book);
                if (setToUpdate == null)
                {
                    setToUpdate = new BookSeriesSet();
                    sets.Add(setToUpdate);
                }
                setToUpdate.Add(book);
            }

            return sets;
        }
    }
}