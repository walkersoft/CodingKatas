﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleBowling
{
    public class BallScoreSpinner
    {
        bool movingRight = true;
        int multiplier;
        int interval;
        int maxPins;
        int spinnerPos = 0;
        int[] scoreZones;
        StringBuilder spinner, indicator;
        Timer timer;

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
            set => maxPins = Math.Clamp(value, 1, 10); 
        }

        public BallScoreSpinner()
        {
            MaxPins = 10;
            ZoneMultiplier = 1;
            Interval = 200;
        }        

        public void StartSpinner()
        {
            BuildScoreZones();
            BuildSpinner();
            Console.CursorVisible = false;
            timer = new();
            timer.Interval = Interval;
            timer.Elapsed += (o, e) => ProgressSpinner();
            timer.Start();
            ReadUserInput();
        }

        private void ReadUserInput()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Console.WriteLine(scoreZones[spinnerPos - 1]);
                    break;
                }
            }
        }

        private void ProgressSpinner()
        {
            spinnerPos = movingRight ? ++spinnerPos : --spinnerPos;
            spinner = spinner.Remove(spinnerPos, 1).Insert(spinnerPos, "=");
            //need to fix this line so cursor position can be dynamic
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(indicator.ToString());
            Console.Write($"{spinner} ({scoreZones[spinnerPos - 1]})  ");
            spinner = spinner.Remove(spinnerPos, 1).Insert(spinnerPos, " ");
            if (spinnerPos >= spinner.Length - 2) movingRight = false; //deduction accounts for chars at either end
            if (spinnerPos <= 1) movingRight = true;
        }

        private void BuildScoreZones()
        {
            int[] halfZone = new int[(MaxPins - 1) * ZoneMultiplier];
            int lastIndex = 0;
            for (int i = 1; i < MaxPins; i++)
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
            int halfSpinnerSize = ZoneMultiplier * (MaxPins - 1);

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
