using System;
using System.Text;
using System.Timers;

namespace ConsoleBowling
{
    public class BallScoreSpinner
    {
        private bool movingRight = true;
        private int multiplier;
        private int interval;
        private int maxPins;
        private int spinnerPos = 0;
        private int[] scoreZones;
        private StringBuilder spinner, indicator;
        private Timer timer;

        public int DisplayColumn { get; set; }
        public int DisplayRow { get; set; }

        public int Interval
        {
            get => interval;
            set => interval = Math.Clamp(value, 1, 5000);
        }
        
        public int ZoneMultiplier
        {
            get => multiplier;
            set => multiplier = Math.Clamp(value, 1, 4);
        }

        public int MaxPins 
        { 
            get => maxPins; 
            set
            {
                //The setter for this value also sets the difficulty interval used by
                //the spinner since the two are closely related.
                maxPins = Math.Clamp(value, 1, 10);

                if (maxPins < 11)
                {
                    ZoneMultiplier = 1;
                    Interval = 45;
                }

                if (maxPins < 6)
                {
                    ZoneMultiplier = 2;
                    Interval = 50;
                }

                if (maxPins < 2)
                {
                    ZoneMultiplier = 4;
                    Interval = 55;
                }
            }
        }

        public BallScoreSpinner()
        {
            MaxPins = 10;
        }        

        /// <summary>
        /// Handles the setup and initiation of the "spinner" used by the game to collect player input.
        /// 
        /// A spinner is a vertical bar that moves a character back and forth.  The entire area is 
        /// internally broken up into "zones" that hold the score a player will achieve upon pressing
        /// the enter key.  The speed of the moving character is based on the speed interval set in the
        /// class and is moved via a method on a timer event.
        /// </summary>
        /// <returns>Returns the amount of pins scored by the player.</returns>
        public int StartSpinner()
        {
            BuildScoreZones();
            BuildSpinner();
            Console.CursorVisible = false;
            timer = new();
            timer.Interval = Interval;
            timer.Elapsed += (o, e) => ProgressSpinner();
            timer.Start();

            //input from user determines score
            int roll = ReadUserInput();

            //reset spinner
            spinnerPos = 0;
            movingRight = true;

            return roll;
        }

        private int ReadUserInput()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter && spinnerPos > 0)
                {
                    //spinnerPos check is needed in case user hits key before 
                    //spinner had a chance to progress to a valid score zone
                    timer.Stop();
                    timer.Elapsed -= (o, e) => ProgressSpinner();
                    return scoreZones[spinnerPos - 1];                    
                }
            }
        }

        private void ProgressSpinner()
        {
            spinnerPos = movingRight ? ++spinnerPos : --spinnerPos;
            spinner = spinner.Remove(spinnerPos, 1).Insert(spinnerPos, "=");
            
            Console.SetCursorPosition(DisplayColumn, DisplayRow);
            Console.WriteLine(indicator.ToString());
            Console.Write($"{spinner} ({scoreZones[spinnerPos - 1]})  ");
            spinner = spinner.Remove(spinnerPos, 1).Insert(spinnerPos, " ");
            
            if (spinnerPos >= spinner.Length - 2) movingRight = false; //deduction accounts for chars at either end
            if (spinnerPos <= 1) movingRight = true;
        }

        private void BuildScoreZones()
        {
            //score zones are based on the amount of pins available to score and
            //the multiplier for a zone.  The zone multiplier determines how many
            //spaces on each side of the max score the lower scores will have.
            int[] halfZone = new int[MaxPins * ZoneMultiplier];
            int lastIndex = 0;
            for (int i = 0; i < MaxPins; i++)
            {
                for (int j = 0; j < ZoneMultiplier; j++, lastIndex++)
                {
                    halfZone[lastIndex] = i;
                }
            }

            scoreZones = new int[(halfZone.Length * 2) + 1];
            halfZone.CopyTo(scoreZones, 0);
            Array.Reverse(halfZone);
            halfZone.CopyTo(scoreZones, halfZone.Length + 1);
            scoreZones[halfZone.Length] = MaxPins;
        }

        private void BuildSpinner()
        {
            int halfSpinnerSize = MaxPins * ZoneMultiplier;

            indicator = new StringBuilder();
            indicator.Append('|')
                .Append(' ', halfSpinnerSize)
                .Append(MaxPins == 10 ? 'X' : '/')
                .Append(' ', halfSpinnerSize)
                .Append('|');

            spinner = new StringBuilder();
            spinner.Append('|')
                .Append(' ', halfSpinnerSize * 2 + 1)
                .Append('|');
        }
    }
}
