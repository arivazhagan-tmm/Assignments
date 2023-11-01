﻿using static System.Console;

internal class Program {
   //Allows the user to predict the system generated number between 1 and 100.
   static void Main () {
      int n = new Random ().Next (1, 100);
      bool isValidInput;
      string[] guesses = { "1st", "2nd", "3rd", "4th", "5th", "6th", "last" },
               responses = { "too low", "correct..!", "too high" };
      WriteLine ("The number is chosen between 1 and 100. Find the number in 7 guesses!");
      for (int i = 0, count = 7, guess; i < count; i++) {
         var prompt = $"your {guesses[i]} guess: ";
         WriteLine ();
         do {
            Write (prompt);
            isValidInput = int.TryParse (ReadLine (), out guess) && guess > 0;
            if (!isValidInput) {
               ForegroundColor = ConsoleColor.Red;
               Write ("\tPlease enter a valid whole number!");
               ResetColor ();
               WriteLine ();
            }
         } while (!isValidInput);
         var tmp = guess.CompareTo (n);
         Write ($"\t\tyour guess is {responses[tmp + 1]}!");
         WriteLine ();
         if (tmp is 0) break;
      }
   }
}