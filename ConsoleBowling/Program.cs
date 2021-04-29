using ConsoleBowling;
using System;

var scorer = new ScoringCalculator();
int[] rolls = { 2, 4 };
int[] frames = scorer.CalculateScore(rolls);

//game state vars
string[] displayFrames = new string[11];
string[] displayRolls = new string[21];
int[] gameFrames;
int currentRollScore;

for (int i = 0; i < displayRolls.GetLength(0); i++)
{
    displayRolls[i] = "0";
}

for (int i = 0; i < displayFrames.Length; i++)
{
    displayFrames[i] = "0";
}

DrawScoreboard(displayRolls, displayFrames);
Console.Write("Next score: ");
Console.ReadKey();

static void DrawScoreboard(string[] displayRolls, string[] displayFrames)
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