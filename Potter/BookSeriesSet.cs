using System;
using System.Collections.Generic;

namespace Potter
{
    public class BookSeriesSet
    {
        public const decimal BasePrice = 8;
        private readonly IList<HarryPotterBook> _booksInSeries = new List<HarryPotterBook>();
        readonly DiscountRateCard _discountRateCard = new DiscountRateCard();

        public void Add(HarryPotterBook book)
        {
            if(Contains(book))
                throw new ArgumentException(book + " has already been added to this set. Each book " +
                                            "in the series can only be added once.");

            _booksInSeries.Add(book);
        }

        public bool Contains(HarryPotterBook book)
        {
            return _booksInSeries.Contains(book);
        }

        public decimal CalculatePriceForSet()
        {
            return BasePrice * _booksInSeries.Count * 
                   _discountRateCard.CalculateRateToCharge(_booksInSeries.Count);
        }

        public int Count { get { return _booksInSeries.Count; } }
    }
}