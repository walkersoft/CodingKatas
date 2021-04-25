using System;
using Xunit;

namespace FizzBuzz.Tests
{
    public class FizzBuzzShould
    {
        FizzBuzzGame game;

        public FizzBuzzShould()
        {
            game = new();
        }

        [Fact]
        public void SayFizzWhenDivisibleByThree()
        {
            int[] tests = { 3, 6, 9, 12 };
            foreach (int n in tests)
            {
                Assert.Equal(game.Fizz, game.WhatIs(n));
            }
        }

        [Fact]
        public void SayBuzzWhenDivisibleByFive()
        {
            int[] tests = { 5, 10, 20, 40 };
            foreach (int n in tests)
            {
                Assert.Equal(game.Buzz, game.WhatIs(n));
            }
        }

        [Fact]
        public void SayFizzBuzzWhenDivisibleByFifteen()
        {
            int[] tests = { 15, 30, 45, 60 };
            foreach (int n in tests)
            {
                Assert.Equal(game.FizzBuzz, game.WhatIs(n));
            }
        }

        [Fact]
        public void SayNumberWhenNotDivisibleByThreeOrFive()
        {
            int[] tests = { 1, 2, 26, 73 };
            foreach (int n in tests)
            {
                Assert.Equal(n.ToString(), game.WhatIs(n));
            }
        }

        [Fact]
        public void SayFizzBuzzCorrectlyForOneThruTwenty()
        {
            string[] answers = {
                "1", "2", game.Fizz, "4", game.Buzz,
                game.Fizz, "7", "8", game.Fizz, game.Buzz,
                "11", game.Fizz, "13", "14", game.FizzBuzz,
                "16", "17", game.Fizz, "19", game.Buzz
            };

            for (int i = 1; i <= 20; i++)
            {
                Assert.Equal(answers[i - 1], game.WhatIs(i));
            }
        }
    }
}
