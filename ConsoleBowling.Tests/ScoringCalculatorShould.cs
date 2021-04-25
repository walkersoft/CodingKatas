using System;
using Xunit;

namespace ConsoleBowling.Tests
{
    public class ScoringCalculatorShould
    {
        private BowlingGame game;

        public ScoringCalculatorShould()
        {
            game = new();
        }

        [Fact]
        public void CalculateFrameWithoutMarks()
        {
            int[] rolls = { 4, 5 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(9, frames[0]);
            Assert.Equal(9, frames[10]);
        }

        [Fact]
        public void CalculateMultipleFramesWithoutMarks()
        {
            int[] rolls = { 4, 5, 3, 6, 2, 4 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(9, frames[0]);
            Assert.Equal(9, frames[1]);
            Assert.Equal(6, frames[2]);
            Assert.Equal(24, frames[10]);
        }
    }
}
