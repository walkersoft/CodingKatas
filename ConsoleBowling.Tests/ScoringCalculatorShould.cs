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

        [Fact]
        public void CalculateSpareMarkWithoutBonusesYet()
        {
            int[] rolls = { 5, 5 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(10, frames[0]);
            Assert.Equal(10, frames[10]);
        }

        [Fact]
        public void CalculateFullGameWithoutAnyMarks()
        {
            int[] rolls = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            int[] frames = game.CalculateScore(rolls);

            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(4, frames[i]);
            }

            Assert.Equal(40, frames[10]);
            Assert.Equal(20, rolls.Length);
        }

        [Fact]
        public void CalculateFullGameOfSpareMarks()
        {
            int[] rolls = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            int[] frames = game.CalculateScore(rolls);

            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(15, frames[i]);
            }

            Assert.Equal(150, frames[10]);
            Assert.Equal(21, rolls.Length);
        }

        [Fact]
        public void CalculateFullGameOfStrikeMarks()
        {
            int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            int[] frames = game.CalculateScore(rolls);

            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(30, frames[i]);
            }

            Assert.Equal(300, frames[10]);
            Assert.Equal(12, rolls.Length);
        }

        [Fact]
        public void CalculateGameOfMixedScoresAndMarks()
        {
            int[] rolls = { 6, 4, 7, 2, 10, 6, 4, 6, 1, 4, 6, 8, 2, 10, 10, 10, 6, 4 };
            int[] frames = game.CalculateScore(rolls);

            Assert.Equal(183, frames[10]);
        }

        [Fact]
        public void NotCalculateRollsBeyondFrameTen()
        {
            int[] rolls = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            int[] frames = game.CalculateScore(rolls);

            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(15, frames[i]);
            }

            Assert.Equal(150, frames[10]);
            Assert.Equal(25, rolls.Length);
        }
    }
}
