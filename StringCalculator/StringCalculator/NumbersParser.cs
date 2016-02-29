using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    internal class NumbersParser
    {
        private const string CustomDelimitersSign = "//";
        private const char MultiSymbolDelimiterStartSign = '[';
        private const char MultiSymbolDelimiterEndSign = ']';
        private readonly List<int> _numbers;
        private readonly List<string> _delimiters = new List<string>(new[] { ",", "\n" });

        public NumbersParser(string numbers)
        {
            numbers = ParseAndCutDelimiter(numbers);
            _numbers = ParseNumbers(numbers);
            ValidateValues(_numbers);
        }

        private List<int> ParseNumbers(string numbers)
        {
            return numbers.Split(_delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Where(num => num <= 1000).ToList();
        }

        private string ParseAndCutDelimiter(string numbers)
        {
            if (!numbers.StartsWith(CustomDelimitersSign)) return numbers;

            numbers = numbers.Substring(2);

            numbers = numbers.StartsWith(MultiSymbolDelimiterStartSign.ToString()) ? ParseAndCutMultiSymbolsDelimiters(numbers) : ParseAndCutSingleSymbolDelimiter(numbers);

            return numbers;
        }

        private string ParseAndCutSingleSymbolDelimiter(string numbers)
        {
            _delimiters.Add(numbers.Substring(0, 1));
            numbers = numbers.Substring(2);
            return numbers;
        }

        private string ParseAndCutMultiSymbolsDelimiters(string numbers)
        {
            _delimiters.AddRange(numbers.Substring(1, numbers.LastIndexOf(MultiSymbolDelimiterEndSign) - 1)
                .Split(new[] {MultiSymbolDelimiterEndSign + MultiSymbolDelimiterStartSign.ToString()},
                    StringSplitOptions.RemoveEmptyEntries));
            numbers = numbers.Substring(numbers.LastIndexOf(MultiSymbolDelimiterEndSign) + 1);
            return numbers;
        }

        private static void ValidateValues(List<int> values)
        {
            var negatives = values.FindAll(num => num < 0);
            if (negatives.Count > 0)
            {
                throw new Exception(string.Format("Negatives not allowed: {0}", string.Join(",", negatives.Select(num => num))));
            }
        }

        public List<int> Numbers
        {
            get { return _numbers; }
        }
    }
}
