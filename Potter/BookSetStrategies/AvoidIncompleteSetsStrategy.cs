using System.Collections.Generic;
using System.Linq;

namespace Potter.BookSetStrategies
{
    public class AvoidIncompleteSetsStrategy
    {
        public BookSeriesSet FindSetForBook(List<BookSeriesSet> sets, HarryPotterBook book)
        {
            var smallestMatchingSet = (from set in sets
                                       where !set.Contains(book)
                                       orderby set.Count
                                       select set).FirstOrDefault();
            return smallestMatchingSet;
        }
    }
}