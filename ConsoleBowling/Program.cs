using ConsoleBowling;
using System;
using System.Collections.Generic;

Console.Clear();
ScoringCalculator scorer = new();
GameUI ui = new();

//game state vars
string[,] displayFrameRolls = new string[10, 3];
int displayFrameRollsIndex = 0;
string[] displayFrames = new string[11];
List<int> gameRolls = new();
int ballIndex = 0;
int[] frames = new int[11];


for (int i = 0; i < displayFrameRolls.GetLength(0); i++)
{
    for (int j = 0; j < displayFrameRolls.GetLength(1); j++)
    {
        displayFrameRolls[i, j] = "0";
    }
}

for (int i = 0; i < displayFrames.Length; i++)
{
    displayFrames[i] = "0";
}



do
{
    Console.SetCursorPosition(0, 0);
    DrawScoreboard(gameRolls.ToArray(), displayFrameRolls, displayFrames);

    if (ballIndex == 3)
    {
        break;
    }

    int inputScore = GetInput();


    gameRolls.Add(inputScore);
    frames = scorer.CalculateScore(gameRolls.ToArray());

    for (int i = 0; i < frames.Length; i++)
    {
        displayFrames[i] = frames[i].ToString();
    }

    if (displayFrameRollsIndex < 9)
    {
        if (ballIndex == 0)
        {
            if (scorer.LastScoreWasStrike)
            {
                displayFrameRolls[displayFrameRollsIndex, ballIndex] = "X";
                displayFrameRolls[displayFrameRollsIndex, ballIndex + 1] = " ";
                displayFrameRollsIndex++;
                continue;
            }

            if (displayFrameRollsIndex > 9)
            {
                displayFrameRollsIndex = 9;
            }

            displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
        }

        if (ballIndex == 1)
        {
            if (scorer.LastScoreWasSpare)
            {
                displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
            }
            else
            {
                displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
            }

            if (displayFrameRollsIndex < 9)
            {
                displayFrameRollsIndex++;
                ballIndex--;
            }

            continue;
        }

        ballIndex++;
        continue;
    }

    if (displayFrameRollsIndex == 9)
    {

        if (inputScore == 10)
        {
            displayFrameRolls[displayFrameRollsIndex, ballIndex] = "X";
            ballIndex++;
            continue;
        }

        if ((ballIndex == 1 || ballIndex == 2) && (inputScore + gameRolls[^2] == 10))
        {
            displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
            ballIndex++;
            continue;
        }

        displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();

        ballIndex++;      

    }    
}
while (true);

Console.WriteLine(string.Format("Game concluded. Total Score: {0}", frames[10]));

static void DrawScoreboard(int[] gameRolls, string[,] displayRolls, string[] displayFrames)
{
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

static void DrawScores(string[,] displayRolls, string[] displayFrames)
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

static void DrawBorder()
{
    Console.Write("+");
    for (int i = 0; i < 10; i++)
    {
        Console.Write("----+");
    }
    Console.WriteLine();
}

static int GetInput()
{
    (int col, int line) = Console.GetCursorPosition();
    int score;

    while (true)
    {
        Console.Write("Next ball score: ");
        if (int.TryParse(Console.ReadLine().Trim(), out score))
        {
            score = Math.Clamp(score, 0, 10);
            ClearLine(line);
            break;
        }

        ClearLine(line);
    }

    return score;
}

static void ClearLine(int line)
{
    Console.SetCursorPosition(0, line);
    Console.Write(string.Format("{0,80}", " "));
    Console.SetCursorPosition(0, line);
}

delegate void HandleFrame(int inputScore, ref int lastInputScore, ref int displayFrameRollsIndex, ref int ballIndex, ref string[,] displayFrameRolls);