using System;
using System.Collections.Generic;

namespace Potter
{
    public class DiscountRateCard
    {
        readonly IDictionary<int, int> _discountRates = new Dictionary<int, int>
                                                                  {
                                                                      {1, 0},
                                                                      {2, 5},
                                                                      {3, 10},
                                                                      {4, 20},
                                                                      {5, 25},
                                                                      {6, 25},
                                                                      {7, 25},
                                                                  };

        public int CalculateDiscountRate(int numberOfBooksInSet)
        {
            if(_discountRates.ContainsKey(numberOfBooksInSet))
                return _discountRates[numberOfBooksInSet];

            throw new ArgumentException("{0} is not a valid number of Harry Potter books.");
        }

        public decimal CalculateRateToCharge(int numberOfBooksInSet)
        {
            return (100m - CalculateDiscountRate(numberOfBooksInSet)) / 100;
        }
    }
}