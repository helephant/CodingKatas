using System.Collections.Generic;

namespace PrintEnglishNumber
{
    public class Unit
    {
        public Unit(long magnitude, string unitName, string seperator)
        {
            Magnitude = magnitude;
            UnitName = unitName;
            Seperator = seperator;
        }

        public bool IsMagnitude(long number)
        {
            return number >= Magnitude && number < Magnitude * 10 ;    
        }

        public long Magnitude { get; private set; }
        public string UnitName { get; private set; }
        public string Seperator { get; private set; }


        public static Unit GetUnit(long number)
        {
            var units = new List<Unit>
                            {
                                Billions,
                                Millions,
                                Thousands,
                                Hundreds,
                                Tens,
                                Ones,
                            };

            var e = units.GetEnumerator();
            while (e.MoveNext() && number < e.Current.Magnitude) ;
            return e.Current;
        }

        public static Unit Billions = new Unit(1000000000, "Billion", ", ");
        public static Unit Millions = new Unit(1000000, "Million", ", ");
        public static Unit Thousands = new Unit(1000, "Thousand", ", ");
        public static Unit Hundreds = new Unit(100, "Hundred", ", ");
        public static Unit Tens = new Unit(10, "", " and ");
        public static Unit Ones = new Unit(0, "", " and ");
    }
}