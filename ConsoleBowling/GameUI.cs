using System;

namespace ConsoleBowling
{
    public class GameUI
    {
        private BallScoreSpinner spinner;

        public GameUI(BallScoreSpinner spinner)
        {
            this.spinner = spinner;
        }

        /// <summary>
        /// Draws the entire scoreboard to the console window.
        /// </summary>
        /// <param name="gameRolls">An array of rolls the player has taken.</param>
        /// <param name="displayRolls">An array of rolls, as they are to be displayed.</param>
        /// <param name="displayFrames">An array of frames, as they ar eto be displayed.</param>
        public void DrawScoreboard(int[] gameRolls, string[,] displayRolls, string[] displayFrames)
        {
            Console.SetCursorPosition(0, 0);
            DrawHeader();
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

        /// <summary>
        /// Starts the bowling "spinner" and prompts the user to press enter to bowl.
        /// </summary>
        /// <returns>Returns an integer of the amount of pins the player scored.</returns>
        public int GetInput()
        {
            Console.WriteLine();
            Console.WriteLine("Press <Enter> to bowl...");
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

        private void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(string.Format("{0,80}", " "));
            Console.SetCursorPosition(0, line);
        }

        private void DrawHeader()
        {
            //my ascii art skills suck!
            Console.WriteLine(@"                        __");
            Console.WriteLine(@"                  _    /  \    _");
            Console.WriteLine(@"                 / \   |  |   / \");
            Console.WriteLine(@"                 | |   \__/   | |");
            Console.WriteLine(@"                 \_/   /  \   \_/");
            Console.WriteLine(@"                 / \  /    \  / \");
            Console.WriteLine(@"                /   \ |----| /   \");
            Console.WriteLine(@"                |---| |----| |---|");
            Console.WriteLine(@"                |---| |    | |---|");
            Console.WriteLine(@"                |   | |    | |   |");
            Console.WriteLine(@"                \___/ \____/ \___/");
            Console.WriteLine(@"            <====CONSOLE==BOWLING====>");
        }
    }
}
