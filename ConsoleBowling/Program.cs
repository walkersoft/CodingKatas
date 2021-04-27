using ConsoleBowling;
using System;

var scorer = new ScoringCalculator();
int[] rolls = { 2, 4 };
int[] frames = scorer.CalculateScore(rolls);


DrawScoreboard();

static void DrawScoreboard()
{
    DrawBorder();
    Console.Write("|");
    
    for (int f = 1; f <= 10; f++)
    {
        Console.Write(string.Format("F:{0:00}|", f));
    }

    Console.WriteLine();
    DrawBorder();
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