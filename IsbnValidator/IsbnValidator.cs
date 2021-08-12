using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsbnValidator
{
    public class IsbnValidator
    {
        public string ParsedIsbn { get; private set; }
        public int CheckDigit { get; private set; }

        private delegate bool CheckDigitValidator(string inputIsbn);
        private CheckDigitValidator validator;

        public IsbnValidator()
        {

        }

        public bool ParseIsbn(string inputIsbn, bool convertToIsbn13 = false)
        {
            ParsedIsbn = inputIsbn.Replace("-", null).ToUpper().Trim();
            
            if (convertToIsbn13)
            {
                ParsedIsbn = "978" + ParsedIsbn;
                ParsedIsbn = RecalculateCheckDigit(ParsedIsbn);
            }

            return VerifyCheckDigit();
        }

        private bool VerifyCheckDigit()
        {
            if (ParsedIsbn.Length == 13) validator = CalculateIsbn13CheckDigit;
            if (ParsedIsbn.Length == 10) validator = CalculateIsbn10CheckDigit;

            return validator(ParsedIsbn);
        }

        private string RecalculateCheckDigit(string inputIsbn)
        {
            CalculateIsbn13CheckDigit(inputIsbn);
            return inputIsbn.Remove(12).Insert(12, CheckDigit.ToString());
        }

        private bool CalculateIsbn13CheckDigit(string inputIsbn)
        {
            // Calcuation rules gathered from Wikipedia
            // For more details see: https://en.wikipedia.org/wiki/International_Standard_Book_Number#ISBN-13_check_digit_calculation
            
            int weightOne = 0;
            foreach (var index in new int[] { 0, 2, 4, 6, 8, 10 })
            {
                weightOne += int.Parse(inputIsbn[index].ToString());
            }

            int weightThree = 0;
            foreach (var index in new int[] { 1, 3, 5, 7, 9, 11 })
            {
                weightThree += int.Parse(inputIsbn[index].ToString());
            }

            CheckDigit = 10 - ((weightOne + weightThree * 3) % 10);
            return ParsedIsbn[12].ToString().CompareTo(CheckDigit.ToString()) == 0;
        }

        private bool CalculateIsbn10CheckDigit(string inputIsbn)
        {
            // Calculation rules gathered from Wikipedia
            // For more details see: https://en.wikipedia.org/wiki/International_Standard_Book_Number#ISBN-10_check_digit_calculation

            int weight = 0;
            for (int i = 0, n = 10; i < 9; i++, n--)
            {
                weight += int.Parse(inputIsbn[i].ToString()) * n;
            }

            CheckDigit = (11 - (weight % 11)) % 11;
            return CheckDigit == 10 ? ParsedIsbn[9].ToString().CompareTo("X") == 0 : ParsedIsbn[9].ToString().CompareTo(CheckDigit.ToString()) == 0;
        }
    }
}
