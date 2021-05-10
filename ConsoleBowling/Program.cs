using ConsoleBowling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

ScoringCalculator scorer = new();
GameUI ui = new();

//game state vars
string[,] displayFrameRolls = new string[10, 3];
int displayFrameRollsIndex = 0;
string[] displayFrames = new string[11];
List<int> gameRolls = new();
int ballIndex = 0;
int[] frames = new int[11];
int[] scoreZones;



//InitGameState(ref displayFrameRolls, ref displayFrames);
Console.Clear();

int pos = 0;
bool moveRight = true;

static (int, bool) MoveBar(StringBuilder spinner, int pos, bool moveRight, int[] scoreZones)
{
    pos = moveRight ? ++pos : --pos;
    spinner = spinner.Remove(pos, 1).Insert(pos, "=");
    Console.SetCursorPosition(0, 0);
    Console.Write($"{spinner} ({scoreZones[pos - 1]})  ");
    spinner = spinner.Remove(pos, 1).Insert(pos, " ");
    if (pos >= spinner.Length - 2) moveRight = false; //deduction accounts for chars on the end
    if (pos <= 1) moveRight = true;

    return (pos, moveRight);
}

static StringBuilder GetSpinner(int maxPins, int multiplier)
{
    //int spinnerSize = multiplier * (maxPins - 1) + 1;
    int halfSpinnerSize = multiplier * (maxPins - 1);
    //int halfSpinnerSize = (spinnerSize - 1) / 2;
    int targetPos = maxPins + 1;
    
    var spinner = new StringBuilder();
    spinner.Append('<')
        .Append('-', halfSpinnerSize)
        .Append('V')
        .Append('-', halfSpinnerSize)
        .Append('>');

    return spinner;
}

static int[] BuildScoreZones(int maxPins, int multiplier)
{
    int[] halfZone = new int[(maxPins - 1) * multiplier];
    int lastIndex = 0;
    for (int i = 1; i < maxPins; i++)
    {
        for (int j = 0; j < multiplier; j++, lastIndex++)
        {
            halfZone[lastIndex] = i;
        }
    }

    int[] scoreZones = new int[(halfZone.Length * 2) + 1];
    halfZone.CopyTo(scoreZones, 0);
    Array.Reverse(halfZone);
    halfZone.CopyTo(scoreZones, halfZone.Length + 1);
    scoreZones[halfZone.Length] = maxPins;

    return scoreZones;
}

int maxPins = 7;
int multiplier = 3;

scoreZones = BuildScoreZones(maxPins, multiplier);
var spinner = GetSpinner(maxPins, multiplier);
var timer = new System.Timers.Timer();
timer.Interval = 200;
timer.Elapsed += (o, e) => { (pos, moveRight) = MoveBar(spinner, pos, moveRight, scoreZones); };
timer.Start();
Console.CursorVisible = false;

while (true)
{
    var key = Console.ReadKey(true);
    if (key.Key == ConsoleKey.Enter)
    {
        Console.WriteLine();
        Console.WriteLine(pos);
        break;
    }
}

//do
//{
//    Console.SetCursorPosition(0, 0);
//    ui.DrawScoreboard(gameRolls.ToArray(), displayFrameRolls, displayFrames);

//    if (ballIndex == 3)
//    {
//        break;
//    }

//    int inputScore = ui.GetInput();


//    gameRolls.Add(inputScore);
//    frames = scorer.CalculateScore(gameRolls.ToArray());

//    int runningTotal = 0;
//    for (int i = 0; i < frames.Length; i++)
//    {
//        runningTotal += frames[i];
//        displayFrames[i] = frames[i] == 0 ? frames[i].ToString() : runningTotal.ToString();
//    }

//    if (displayFrameRollsIndex < 9)
//    {
//        if (ballIndex == 0)
//        {
//            if (scorer.LastScoreWasStrike)
//            {
//                displayFrameRolls[displayFrameRollsIndex, ballIndex] = "X";
//                displayFrameRolls[displayFrameRollsIndex, ballIndex + 1] = " ";
//                displayFrameRollsIndex++;
//                continue;
//            }

//            if (displayFrameRollsIndex > 9)
//            {
//                displayFrameRollsIndex = 9;
//            }

//            displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
//        }

//        if (ballIndex == 1)
//        {
//            if (scorer.LastScoreWasSpare)
//            {
//                displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
//            }
//            else
//            {
//                displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();
//            }

//            if (displayFrameRollsIndex < 9)
//            {
//                displayFrameRollsIndex++;
//                ballIndex--;
//            }

//            continue;
//        }

//        ballIndex++;
//        continue;
//    }

//    if (displayFrameRollsIndex == 9)
//    {

//        if (inputScore == 10)
//        {
//            displayFrameRolls[displayFrameRollsIndex, ballIndex] = "X";
//            ballIndex++;
//            continue;
//        }

//        if ((ballIndex == 1 || ballIndex == 2) && (inputScore + gameRolls[^2] == 10))
//        {
//            displayFrameRolls[displayFrameRollsIndex, ballIndex] = "/";
//            ballIndex++;
//            continue;
//        }

//        displayFrameRolls[displayFrameRollsIndex, ballIndex] = inputScore.ToString();

//        ballIndex++;      

//    }    
//}
//while (true);

//Console.WriteLine(string.Format("Game concluded. Total Score: {0}", frames[10]));

//static void InitGameState(ref string[,] displayFrameRolls, ref string[] displayFrames)
//{
//    for (int i = 0; i < displayFrameRolls.GetLength(0); i++)
//    {
//        for (int j = 0; j < displayFrameRolls.GetLength(1); j++)
//        {
//            displayFrameRolls[i, j] = "0";
//        }
//    }

//    for (int i = 0; i < displayFrames.Length; i++)
//    {
//        displayFrames[i] = "0";
//    }
//}