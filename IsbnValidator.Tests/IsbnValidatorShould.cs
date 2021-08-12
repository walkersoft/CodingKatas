using System;
using Xunit;
using IsbnValidator;

namespace IsbnValidator.Tests
{
    public class IsbnValidatorShould
    {
        private IsbnValidator validator;
        private string validIsbn10 = "0-321-47127-X";
        private string validIsbn10Parsed = "032147127X";
        private string validIsbn13 = "978-0-321-47127-7";
        private string validIsbn13Parsed = "9780321471277";

        public IsbnValidatorShould()
        {
            validator = new();
        }

        [Fact]
        public void RemoveHyphensFromIsbn()
        {
            validator.ParseIsbn(validIsbn10);
            Assert.Equal(validIsbn10Parsed, validator.ParsedIsbn);
        }

        [Fact]
        public void TrimWhiteSpacesFromIsbn()
        {
            validator.ParseIsbn("  0-321-47127-X");
            Assert.Equal(validIsbn10Parsed, validator.ParsedIsbn);

            validator.ParseIsbn("0-321-47127-X  ");
            Assert.Equal(validIsbn10Parsed, validator.ParsedIsbn);
        }

        [Fact]
        public void FixCasingOfCheckDigit()
        {
            validator.ParseIsbn("0-321-47127-x");
            Assert.Equal(validIsbn10Parsed, validator.ParsedIsbn);
        }

        [Fact]
        public void ConvertIsbn10ToIsbn13()
        {
            validator.ParseIsbn(validIsbn10, true);
            Assert.Equal(validIsbn13Parsed, validator.ParsedIsbn);
        }

        [Fact]
        public void VerifyValidIsbn13CheckDigit()
        {
            Assert.True(validator.ParseIsbn(validIsbn13));
        }

        [Fact]
        public void VerifyValidIsbn10CheckDigitThatIsX()
        {
            Assert.True(validator.ParseIsbn(validIsbn10));
        }
    }
}
