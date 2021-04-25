using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBowling
{
    public class BowlingGame
    {
        private int currentFrame = 0;
        private int ball = 1;

        public int[] CalculateScore(int[] rolls)
        {
            int[] frames = new int[11];

            for (int i = 0; i < rolls.Length; i++)
            {
                frames[currentFrame] += rolls[i];

                if (rolls[i] == 10) //Strike!
                {
                    //look ahead two rolls (if available) and add them as bonus points
                    //and then advance the frame
                    if (rolls.Length >= i + 2)
                    {
                        frames[currentFrame] += rolls[i + 1] + rolls[i + 2];
                        AdvanceFrame();
                        continue;
                    }
                }
                
                if (ball == 2)
                {
                    AdvanceFrame();
                    continue;
                }

                ball++;
            }

            frames[10] = frames.Sum();

            return frames;
        }

        private void AdvanceFrame()
        {
            ball = 1;
            currentFrame++;
        }
    }
}
