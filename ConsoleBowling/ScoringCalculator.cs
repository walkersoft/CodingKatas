using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBowling
{
    public class ScoringCalculator
    {
        private int currentFrame;
        private int ball;
        private int[] rolls;
        private int[] frames;

        public bool LastScoreWasStrike { get; private set; }
        public bool LastScoreWasSpare { get; private set; }

        public int[] CalculateScore(int[] incomingRolls)
        {
            rolls = incomingRolls;
            frames = new int[11];
            currentFrame = 0;
            ball = 1;

            //clamp values between 0 and 10
            rolls = ClampRolls(rolls);

            for (int i = 0; i < rolls.Length; i++)
            {
                LastScoreWasStrike = false;
                LastScoreWasSpare = false;

                if (currentFrame == 10) 
                {
                    //Game Over - all frames counted
                    break;
                }

                //check second ball in frame isn't more than a spare amount
                if (ball == 2)
                {
                    rolls[i] = Math.Clamp(rolls[i], 0, 10 - rolls[i - 1]);
                }

                //add the roll score to the current frame
                frames[currentFrame] += rolls[i];

                if (StrikeHandled(i))
                {
                    LastScoreWasStrike = true;
                    AdvanceFrame();
                    continue;
                }

                if (SpareHandled(i))
                {
                    LastScoreWasSpare = true;
                    AdvanceFrame();
                    continue;
                }

                if (ball == 2)
                {
                    AdvanceFrame();
                    continue;
                }

                ball++;
            }

            //last element is the player's total score
            frames[10] = frames.Sum();

            return frames;
        }

        private void AdvanceFrame()
        {
            ball = 1;
            currentFrame++;
        }

        private int[] ClampRolls(int[] rolls)
        {
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = Math.Clamp(rolls[i], 0, 10);
            }

            return rolls;
        }

        private bool StrikeHandled(int rollIndex)
        {
            if (ball == 1 && rolls[rollIndex] == 10)
            {
                //add the next two rolls as bonus points, if they exist
                if (rolls.Length > rollIndex + 2)
                {
                    frames[currentFrame] += rolls[rollIndex + 1] + rolls[rollIndex + 2];
                }

                return true;
            }

            return false;
        }

        private bool SpareHandled(int rollIndex)
        {
            if (ball == 2 && (rolls[rollIndex] + rolls[rollIndex - 1]) == 10)
            {
                //add the next roll as bonus point, if it exists
                if (rolls.Length > rollIndex + 1)
                {
                    frames[currentFrame] += rolls[rollIndex + 1];
                }

                return true;
            }

            return false;
        }
    }
}
