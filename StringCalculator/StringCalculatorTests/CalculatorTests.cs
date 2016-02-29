using System;
using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            string input = "";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 0);
        }

        [Test]
        public void Add_SingleNumber_ReturnsNumber()
        {
            string input = "1";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Add_TwoCommaSeparatedNumbers_ReturnSum()
        {
            string input = "1,2";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 3);
        }

        [Test]
        public void Add_FiveCommaSeparatedNumbers_ReturnSum()
        {
            string input = "1,2,3,4,5";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 15);
        }

        [Test]
        public void Add_FiveCommaAndEnterSeparatedNumbers_ReturnSum()
        {
            string input = "1\n2,3,4\n5";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 15);
        }

        [Test]
        public void Add_DifferentDelimeter_ReturnSum()
        {
            string input = "//;\n1;2";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 3);
        }

        [Test]
        public void Add_NegativeNumberIsPresent_ThrowsException()
        {
            string input = "1\n-2,3,4\n5";

            Assert.Throws<Exception>(() => _calculator.Add(input));
        }

        [Test]
        public void Add_NegativeNumbersArePresent_ThrowsExceptionWithSpecificMessage()
        {
            string input = "1\n-2,3,-4\n-5";

            var ex = Assert.Throws<Exception>(() => _calculator.Add(input));

            Assert.That(ex.Message, Is.EqualTo("Negatives not allowed: -2,-4,-5"));
        }

        [Test]
        public void Add_NumbersBiggerThousandShouldBeIgnored_ReturnSum()
        {
            string input = "1,1001,1000,2";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 1003);
        }

        [Test]
        public void Add_DelimiterLengthMoreThanOneSymbol_ReturnSum()
        {
            string input = "//[***]\n1***2***3***4";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 10);
        }

        [Test]
        public void Add_MultipleCustomDelimiters_ReturnSum()
        {
            string input = "//[*][%]\n1*2%3";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 6);
        }

        [Test]
        public void Add_MultipleCustomDelimitersWithCustomLength_ReturnSum()
        {
            string input = "//[*][%%%][qq]\n1*2%%%3qq4*5qq6\n7*8";

            var result = _calculator.Add(input);

            Assert.AreEqual(result, 36);
        }
    }
}
