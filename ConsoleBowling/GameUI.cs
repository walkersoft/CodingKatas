using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBowling
{
    public class GameUI
    {
        private BallScoreSpinner spinner;

        public GameUI(BallScoreSpinner spinner)
        {
            this.spinner = spinner;
        }

        public void DrawScoreboard(int[] gameRolls, string[,] displayRolls, string[] displayFrames)
        {
            Console.SetCursorPosition(0, 0);
            DrawBorder();
            Console.Write("|");

            for (int f = 1; f <= 10; f++)
            {
                Console.Write(string.Format("F:{0:00}|", f));
            }

            Console.WriteLine();
            DrawBorder();
            DrawScores(displayRolls, displayFrames);
            DrawBorder();
        }

        private void DrawScores(string[,] displayRolls, string[] displayFrames)
        {
            Console.Write("|");
            for (int i = 0; i < 9; i++)
            {
                Console.Write(string.Format("{0} {1} |", displayRolls[i, 0], displayRolls[i, 1]));
            }
            Console.Write(string.Format("{0} {1}{2}|", displayRolls[9, 0], displayRolls[9, 1], displayRolls[9, 2]));
            Console.WriteLine();
            DrawBorder();
            Console.Write("|");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(string.Format(" {0,3}|", displayFrames[i]));
            }
            Console.WriteLine();
        }

        private void DrawBorder()
        {
            Console.Write("+");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("----+");
            }
            Console.WriteLine();
        }

        public int GetInput()
        {
            (int col, int line) = Console.GetCursorPosition();

            while (true)
            {
                spinner.DisplayColumn = col;
                spinner.DisplayRow = line;
                ClearLine(line);
                ClearLine(line + 1);
                return spinner.StartSpinner();
            }
        }

        private void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(string.Format("{0,80}", " "));
            Console.SetCursorPosition(0, line);
        }
    }
}
