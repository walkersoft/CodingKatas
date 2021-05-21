using ConsoleBowling;
using System;
using System.Collections.Generic;

ScoringCalculator scorer = new();
BallScoreSpinner spinner = new();
GameUI ui = new(spinner);

//variable related to display only
string[,] displayFrameRolls = new string[10, 3];
int displayFrameRollsIndex = 0;
string[] displayFrames = new string[11];

//variables related to game logic
List<int> gameRolls = new();
int ballIndex = 0;
int[] frames = new int[11];

PopulateScorePlaceholders(ref displayFrameRolls, ref displayFrames);
Console.Clear();

//this is the primary loop
while (true)
{
    //draws the scoreboard with the latest and greatest of score data
    ui.DrawScoreboard(gameRolls.ToArray(), displayFrameRolls, displayFrames);

    if (ballIndex == 3)
    {
        //this occurs at the end of the game and will break the loop
        break;
    }

    //get roll input from the user (spinner based) 
    int inputScore = ui.GetInput();

    //add the score to rolls and let the score calculator work its magic
    gameRolls.Add(inputScore);
    frames = scorer.CalculateScore(gameRolls.ToArray());

    //calculate the running total for all frames played thus far
    int runningTotal = 0;
    for (int i = 0; i < frames.Length; i++)
    {
        runningTotal += frames[i];
        displayFrames[i] = frames[i] == 0 ? frames[i].ToString() : runningTotal.ToString();
    }

    //logic for the first 9 frames
    if (displayFrameRollsIndex < 9)
    {
        if (ballIndex == 0)
        {
            //on strike - advance frame and continue
            if (scorer.LastScoreWasStrike)
            {
                displayFrameRolls[displayFrameRollsIndex, ballIndex] = "X";
                displayFrameRolls[displayFrameRollsIndex, ballIndex + 1] = " ";
                displayFrameRollsIndex++;
                spinner.MaxPins = 10;
                continue;
            }

            //...otherwise, setup for next ball and spare opportunity
            spinner.MaxPins = 10 - inputScore;
            displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
        }

        if (ballIndex == 1)
        {
            //update display with spare mark or score if there isn't a spare
            if (scorer.LastScoreWasSpare)
            {
                displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
            }
            else
            {
                displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
            }
            
            //setup for next frame
            displayFrameRollsIndex++;
            ballIndex--;
            spinner.MaxPins = 10;
            continue;
        }

        //finish setup for spare opportunity in current frame
        ballIndex++;
        continue;
    }

    //logic for the final frame
    if (displayFrameRollsIndex == 9)
    {
        if (inputScore == 10)
        {
            //look at which ball to determine which marks to show
            // - the first ball is a strike mark, regardless
            string mark = "X";
            // - if second ball was a zero, then show a spare mark the bonus ball
            if (ballIndex == 2 && gameRolls[^1] == 0) mark = "/";
            // - if the second ball was a spare mark, then the bonus ball gets a strike mark
            if (ballIndex == 2 && gameRolls[^1] + gameRolls[^2] == 10) mark = "X";

            displayFrameRolls[displayFrameRollsIndex, ballIndex] = mark;

            //setup for next ball
            ballIndex++;
            spinner.MaxPins = 10;
            continue;
        }
        else
        {
            spinner.MaxPins = 10 - inputScore;
        }

        //second ball check for spare and bonus frame
        if (ballIndex == 1 && (inputScore + gameRolls[^2] == 10))
        {
            //check of input score handles displaying zero instead of strike mark if player
            //actually rolls a zero, but the previous roll was a strike
            displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore == 0 ? "0" : "/";

            //setup for bonus (and final) ball
            ballIndex++;
            spinner.MaxPins = 10;
            continue;
        }

        //check for spare in bonus frame
        if (ballIndex == 2 && (inputScore + gameRolls[^2] == 10))
        {
            //display spare mark for final ball
            displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";

            //trigger conditions to end game
            ballIndex++;
            spinner.MaxPins = 10;
            continue;
        }

        //captures scores that fall through the gauntlet of checks above
        displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();

        //if second ball in tenth frame does not result in a spare, no bonus roll is given
        if (ballIndex == 1 && gameRolls[^1] + gameRolls[^2] < 10)
        {
            //setup conditions to break from loop after last UI display has occured
            ballIndex = 3;
            continue;
        }

        //setup for next ball
        ballIndex++;
    }
}

Console.WriteLine();
Console.WriteLine(string.Format("Game concluded. Total Score: {0}\n\n\n", frames[10]));

static void PopulateScorePlaceholders(ref string[,] displayFrameRolls, ref string[] displayFrames)
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