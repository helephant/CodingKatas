using System.Diagnostics;
using NUnit.Framework;

namespace PrintEnglishNumber
{
    [TestFixture]
    public class PrintEnglishNumberTests
    {
        [Test]
        public void SupportsZero()
        {
            AssertNumberEqualsWord(0, "Zero");
        }

        [Test]
        public void SupportsNumbersWithNames()
        {
            AssertNumberEqualsWord(1, "One");
            AssertNumberEqualsWord(13, "Thirteen");
            AssertNumberEqualsWord(40, "Forty");
        }

        [Test]
        public void SupportsUnitsUpToABillion()
        {
            AssertNumberEqualsWord(300, "Three Hundred");
            AssertNumberEqualsWord(6000, "Six Thousand");
            AssertNumberEqualsWord(9000000, "Nine Million");
            AssertNumberEqualsWord(4000000000, "Four Billion");
        }

        [Test]
        public void DashBetweenTensAndOnes()
        {
            AssertNumberEqualsWord(44, "Forty-Four");
        }

        [Test]
        public void AndBetweenTensAndOnesAndLargerNumbers()
        {
            AssertNumberEqualsWord(365, "Three Hundred and Sixty-Five");
            AssertNumberEqualsWord(832751, "Eight Hundred and Thirty-Two Thousand, Seven Hundred and Fifty-One");
            AssertNumberEqualsWord(1044, "One Thousand and Forty-Four");
            AssertNumberEqualsWord(7001, "Seven Thousand and One");
        }

        [Test]
        public void CommaEveryThreeDecimalPlaces()
        {
            AssertNumberEqualsWord(365751, "Three Hundred and Sixty-Five Thousand, Seven Hundred and Fifty-One");
            AssertNumberEqualsWord(6000836, "Six Million, Eight Hundred and Thirty-Six");
        }

        private void AssertNumberEqualsWord(long number, string word)
        {
            var translator = new NumberToWordTranslator();
            Assert.That(translator.Translate(number), Is.EqualTo(word));

            Debug.WriteLine("{0:n0} = {1}", number, word);
        }
    }
}