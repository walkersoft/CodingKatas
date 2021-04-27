using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBowling
{
    public class ScoringCalculator
    {
        private int currentFrame = 0;
        private int ball = 1;

        public int[] CalculateScore(int[] rolls)
        {
            int[] frames = new int[11];

            //any roll can never be more than 10
            rolls = ClampRolls(rolls);

            for (int i = 0; i < rolls.Length; i++)
            {
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

                if (rolls[i] == 10) //Strike!
                {
                    //add the next two rolls as bonus points, if they exist
                    if (rolls.Length > i + 2)
                    {
                        frames[currentFrame] += rolls[i + 1] + rolls[i + 2];
                    }
                    
                    AdvanceFrame();
                    continue;
                }

                if (ball == 2)
                {
                    if ((rolls[i] + rolls[i-1]) == 10) //Spare!
                    {
                        //add the next roll as bonus point, if it exists
                        if (rolls.Length > i + 1)
                        {
                            frames[currentFrame] += rolls[i + 1];
                        }
                    }

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
    }
}
