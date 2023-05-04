using System;
using System.Text;

internal class Program {
   private static void Main (string[] args) {
      Console.WriteLine ("Choose a number between 1 & 127 ");
      StringBuilder builder = new ();
      int divisor = 2, remainder = 1;
      while (divisor <= 128) {
         builder.Insert (0, GetResponse ($"\nWhen the number is divided by {divisor}, is the remainder {remainder}? (y/n) "));
         remainder = Convert.ToInt32 ($"1{builder.ToString ()}", 2);
         divisor *= 2;
      }
      Console.WriteLine ($"\n\nThe secret number is {Convert.ToInt32 (builder.ToString (), 2)}");
   }

   /// <summary>Prompts the user to respond the question and returns response in binary </summary>
   static int GetResponse (string question) {
      for (; ; ) {
         Console.Write (question);
         switch (Console.ReadKey ().Key) {
            case ConsoleKey.N: return 0;
            case ConsoleKey.Y: return 1;
            default: Console.WriteLine ("\nInvalid input"); continue;
         }
      }
   }
}