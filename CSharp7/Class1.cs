using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSharp7
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void OutVariables()
        {
            if(int.TryParse("1234", out int value))
                Debug.WriteLine(value);
        }

        [Test]
        public void IsPatterns()
        {
            object o = 1234;
            if(o is int i)
                Debug.WriteLine(i + " is an int");
            else
                Debug.Write(o + "is not an int");
        }

        [Test]
        public void SwitchPatterns()
        {
            object o = "hello world";

            switch (o)
            {
                case int i:
                    Debug.WriteLine(i + " is an int");
                    break;
                case string s:
                    Debug.WriteLine(s + " is an string");
                    break;
                default: 
                    Debug.WriteLine(o + " is not a string or an int");
                    break;
            }
        }
    }
}
