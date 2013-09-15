using System.Collections.Generic;

namespace PrintEnglishNumber
{
    public class NamedNumbers
    {
        private readonly Dictionary<long, string> _english = new Dictionary<long, string>
                                                                 {
                                                                     {0, "Zero"},
                                                                     {1, "One"},
                                                                     {2, "Two"},
                                                                     {3, "Three"},
                                                                     {4, "Four"},
                                                                     {5, "Five"},
                                                                     {6, "Six"},
                                                                     {7, "Seven"},
                                                                     {8, "Eight"},
                                                                     {9, "Nine"},
                                                                     {10, "Ten"},
                                                                     {11, "Eleven"},
                                                                     {12, "Twelve"},
                                                                     {13, "Thirteen"},
                                                                     {14, "Fourteen"},
                                                                     {15, "Fifteen"},
                                                                     {16, "Sixteen"},
                                                                     {17, "Seventeen"},
                                                                     {18, "Eighteen"},
                                                                     {19, "Nineteen"},
                                                                     {20, "Twenty"},
                                                                     {30, "Thirty"},
                                                                     {40, "Forty"},
                                                                     {50, "Fifty"},
                                                                     {60, "Sixty"},
                                                                     {70, "Seventy"},
                                                                     {80, "Eighty"},
                                                                     {90, "Ninety"},
                                                                 };

        public string Name(long number)
        {
            if (IsNamedNumber(number))
                return _english[number];
            return null;
        }

        public bool IsNamedNumber(long number)
        {
            return _english.ContainsKey(number);
        }
    }
}