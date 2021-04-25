using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBowling
{
    public class BowlingGame
    {
        public int[] CalculateScore(int[] rolls)
        {
            int currentFrame = 0;
            int[] frames = new int[11];

            for (int i = 0; i < rolls.Length; i++)
            {
                frames[currentFrame] += rolls[i];
                
                if (i % 2 == 1)
                {
                    currentFrame++;
                }
            }

            frames[10] = frames.Sum();

            return frames;
        }
    }
}
