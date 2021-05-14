using ConsoleBowling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

ScoringCalculator scorer = new();
BallScoreSpinner spinner = new();

GameUI ui = new(spinner);

//game state vars
string[,] displayFrameRolls = new string[10, 3];
int displayFrameRollsIndex = 0;
string[] displayFrames = new string[11];
List<int> gameRolls = new();
int ballIndex = 0;
int[] frames = new int[11];



InitGameState(ref displayFrameRolls, ref displayFrames);
Console.Clear();


do
{
    ui.DrawScoreboard(gameRolls.ToArray(), displayFrameRolls, displayFrames);

    if (ballIndex == 3)
    {
        break;
    }

    int inputScore = ui.GetInput();


    gameRolls.Add(inputScore);
    frames = scorer.CalculateScore(gameRolls.ToArray());

    int runningTotal = 0;
    for (int i = 0; i < frames.Length; i++)
    {
        runningTotal += frames[i];
        displayFrames[i] = frames[i] == 0 ? frames[i].ToString() : runningTotal.ToString();
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
                spinner.MaxPins = 10;
                continue;
            }

            if (displayFrameRollsIndex > 9)
            {
                displayFrameRollsIndex = 9;
            }

            spinner.MaxPins = 10 - inputScore;
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
            
            spinner.MaxPins = 10;
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
            spinner.MaxPins = 10;
            continue;
        }
        else
        {
            spinner.MaxPins = 10 - inputScore;
        }

        if ((ballIndex == 1 || ballIndex == 2) && (inputScore + gameRolls[^2] == 10))
        {
            displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
            ballIndex++;
            spinner.MaxPins = 10;
            continue;
        }

        displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();

        if (ballIndex == 1)
        {
            if (gameRolls[^1] + gameRolls[^2] < 10)
            {
                //10th frame, ball 2, and no spare - game over time
                ballIndex = 3;
                continue;
            }
        }

        ballIndex++;

    }
}
while (true);

Console.WriteLine();
Console.WriteLine(string.Format("Game concluded. Total Score: {0}", frames[10]));
Console.WriteLine();

static void InitGameState(ref string[,] displayFrameRolls, ref string[] displayFrames)
{
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
}