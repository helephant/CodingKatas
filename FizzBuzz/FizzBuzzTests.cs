using System.Collections.Generic;
using NUnit.Framework;

namespace FizzBuzz
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void ShouldReturnFizzForNumbersDivisibleByThree()
        {
            var list = FizzBuzz();
            Assert.That(list[2], Is.EqualTo("Fizz"));
        }

        [Test]
        public void ShouldReturnBuzzForNumbersDivisibleByFive()
        {
            var list = FizzBuzz();
            Assert.That(list[4], Is.EqualTo("Buzz"));
        }

        [Test]
        public void ShouldReturnFizzBuzzForNumbersDivisibleByThreeAndFive()
        {
            var list = FizzBuzz();
            Assert.That(list[14], Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void ShouldReturnNumberForNumbersNotDivisibleByThreeAndFive()
        {
            var list = FizzBuzz();
            Assert.That(list[0], Is.EqualTo("1"));
        }

        private List<string> FizzBuzz()
        {
            var fizzBuzz = new List<string>();
            for (int i = 1; i <= 100; i++)
            {
                if(i % 15 == 0)
                    fizzBuzz.Add("FizzBuzz");
                if(i % 3 == 0)
                    fizzBuzz.Add("Fizz");
                else if(i % 5 == 0)
                    fizzBuzz.Add("Buzz");
                else
                    fizzBuzz.Add(i.ToString());
            }
            return fizzBuzz;
        }
    }
}
