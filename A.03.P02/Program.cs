using System;
using System.Text;

internal class Program {
   private static void Main (string[] args) {
      Console.WriteLine ("Choose a number between 1 & 127 ");
      var builder = new StringBuilder ();
      int divisor = 2, remainder = 1;
      do {
         builder.Insert (0, GetResponse ($"\nWhen the number is divided by {divisor}, Is the remainder {remainder}? (Y/N) "));
         remainder = Convert.ToInt32 (builder.Insert (0, "1").ToString (), 2);
         builder.Remove (0, 1);
         divisor *= 2;
      } while (divisor <= 128);
      Console.WriteLine ($"\nThe secret number is {Convert.ToInt32 (builder.ToString (), 2)}");
   }

   /// <summary>Prompts the user to respond the question and returns response in binary </summary>
   static int GetResponse (string question) {
      for (; ; ) {
         Console.Write (question);
         switch (Console.ReadKey ().Key) {
            case ConsoleKey.N: return 0;
            case ConsoleKey.Y: return 1;
            default: Console.WriteLine ("\nInvalid Input"); continue;
         }
      }
   }
}