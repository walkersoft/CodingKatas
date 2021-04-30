using ConsoleBowling;
using System;
using System.Collections.Generic;

Console.Clear();
ScoringCalculator scorer = new();

//game state vars
string[] displayFrames = new string[11];
string[] displayRolls = new string[21];
List<int> gameRolls = new();

for (int i = 0; i < displayRolls.GetLength(0); i++)
{
    displayRolls[i] = "0";
}

for (int i = 0; i < displayFrames.Length; i++)
{
    displayFrames[i] = "0";
}


int displayRollsIndex = 0;
do
{
    Console.SetCursorPosition(0, 0);
    DrawScoreboard(gameRolls.ToArray(), displayRolls, displayFrames);
    int inputScore = GetInput();

    gameRolls.Add(inputScore);
    displayRolls[displayRollsIndex++] = inputScore.ToString();

}
while (true);



static void DrawScoreboard(int[] gameRolls, string[] displayRolls, string[] displayFrames)
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

static void DrawScores(string[] displayRolls, string[] displayFrames)
{
    Console.Write("|");
    for (int i = 0; i < 9; i++)
    {
        Console.Write(string.Format("{0} {1} |", displayRolls[i], displayRolls[i + 1]));
    }
    Console.Write(string.Format("{0} {1}{2}|", displayRolls[18], displayRolls[19], displayRolls[20]));
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
        string input = Console.ReadLine();
        if (int.TryParse(input.Trim(), out score))
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