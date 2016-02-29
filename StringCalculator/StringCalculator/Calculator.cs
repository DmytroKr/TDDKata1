using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var numbersParser = new NumbersParser(numbers);

            return numbersParser.Numbers.Sum();
        }
    }
}
