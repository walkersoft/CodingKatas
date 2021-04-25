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

        [Fact]
        public void CalculateFrameWithStrikeMarkAndBonuses()
        {
            int[] rolls = { 10, 4, 3 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(17, frames[0]);
            Assert.Equal(7, frames[1]);
            Assert.Equal(24, frames[10]);
        }

        [Fact]
        public void CalculateFrameWithStrikeWithoutBonusesYet()
        {
            int[] rolls = { 10, 5 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(10, frames[0]);
            Assert.Equal(5, frames[1]);
            Assert.Equal(15, frames[10]);
        }

        [Fact]
        public void CalculateSpareMarkWithBonuses()
        {
            int[] rolls = { 5, 5, 4, 2 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(14, frames[0]);
            Assert.Equal(6, frames[1]);
            Assert.Equal(20, frames[10]);
        }
    }
}
