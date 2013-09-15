using System.Collections.Generic;
using System.Linq;

namespace Potter.BookSetStrategies
{
    public class PreferCompleteSetsStrategy
    {
        public BookSeriesSet FindSetForBook(IEnumerable<BookSeriesSet> sets, HarryPotterBook book)
        {
            // works because the first sets in the collection get filled first
            var firstMatchingSet = (from set in sets
                                    where !set.Contains(book)
                                    select set).FirstOrDefault();
            return firstMatchingSet;
        }
    }
}