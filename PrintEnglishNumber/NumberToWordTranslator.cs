namespace PrintEnglishNumber
{
    public class NumberToWordTranslator
    {
        private readonly NamedNumbers _namedNumbers = new NamedNumbers();

        public string Translate(long number)
        {
            if (_namedNumbers.IsNamedNumber(number))
                return _namedNumbers.Name(number);

            if (Unit.Tens.IsMagnitude(number))
            {
                var magnitude = Unit.Tens.Magnitude;
                return Translate((number / magnitude) * magnitude) + "-" +
                       Translate(number % magnitude);
            }

            var unit = Unit.GetUnit(number);
            var significantDigits = number / unit.Magnitude;
            var word = Translate(significantDigits) + " " + unit.UnitName;

            var remainder = number % unit.Magnitude;
            if (remainder > 0)
            {
                var remainderUnit = Unit.GetUnit(remainder);
                word += remainderUnit.Seperator + Translate(remainder);
            }

            return word;
        }
    }
}