using System.Text;
using static System.Console;
using static System.ConsoleKey;

internal class Program {
   // Predicts the number chosen by user, using binary search algorithm.
   static void Main () {
      WriteLine ("Choose a number between 1 & 127.");
      var (tmp, rem, div) = (-1, 1, 2);
      StringBuilder sb = new ();
      while (div <= 128) {
         WriteLine ("\n");
         var prompt = $"When the number is divided by {div}, is the remainder {rem}? (y/n) ";
         while (tmp is -1) {
            Write (prompt);
            tmp = ReadKey ().Key switch {
               Y => 1,
               N => 0,
               _ => -1
            };
            if (tmp is -1) {
               ForegroundColor = ConsoleColor.Red;
               WriteLine ("\tPlease enter the valid input (either y or n)");
               ResetColor ();
               WriteLine ();
            }
         }
         sb.Insert (0, tmp);
         rem = Convert.ToInt32 ($"1{sb}", 2);
         div *= 2;
         tmp = -1;
      }
      WriteLine ("\n");
      WriteLine ($"The secret number is {Convert.ToInt32 ($"{sb}", 2)}");
   }
}