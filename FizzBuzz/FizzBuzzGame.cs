using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public class FizzBuzzGame
    {
        public string Fizz => "Fizz";
        public string Buzz => "Buzz";
        public string FizzBuzz => Fizz + Buzz;

        public string WhatIs(int number)
        {
            var answer = "";
            if (number % 3 == 0) answer += Fizz;
            if (number % 5 == 0) answer += Buzz;

            return string.IsNullOrEmpty(answer) 
                ? number.ToString() 
                : answer;
        }
    }
}
