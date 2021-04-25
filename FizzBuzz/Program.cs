using FizzBuzz;
using System;

//Play FizzBuzz till 100
//See: https://en.wikipedia.org/wiki/Fizz_buzz
FizzBuzzGame game = new();

for (int i = 1; i < 100; i++)
{
    Console.WriteLine(game.WhatIs(i));
}