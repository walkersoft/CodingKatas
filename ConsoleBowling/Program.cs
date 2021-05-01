using ConsoleBowling;
using System;
using System.Collections.Generic;

Console.Clear();
ScoringCalculator scorer = new();

//game state vars
string[,] displayFrameRolls = new string[10, 3];
int displayFrameRollsIndex = 0;
string[] displayFrames = new string[11];
List<int> gameRolls = new();
int ballIndex = 0;
int lastInputScore = 0;

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
    int inputScore = GetInput();


    gameRolls.Add(inputScore);
    if (ballIndex == 0 && inputScore == 10)
    {
        displayFrameRolls[displayFrameRollsIndex, ballIndex] = "X";
        ballIndex++;
        displayFrameRolls[displayFrameRollsIndex, ballIndex] = " ";
    }
    else if (ballIndex == 1 && inputScore + lastInputScore >= 10)
    {
        displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
    }
    else
    {
        displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
        lastInputScore = inputScore;
    }

    if (ballIndex == 1)
    {
        lastInputScore = 0;
        displayFrameRollsIndex++;
        ballIndex--;
        continue;
    }


    ballIndex++;
}
while (true);

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