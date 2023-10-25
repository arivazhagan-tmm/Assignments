using static System.Console;

class Program {

   // Prints the integral & factorial part of the decimal number given by the user.
   static void Main () {
      while (true) {
         var prompt = "Enter a decimal number: ";
         double f;
         Write (prompt);
         while (!double.TryParse (ReadLine (), out f)) {
            ForegroundColor = ConsoleColor.Red;
            WriteLine ("\tPlease enter any decimal value!");
            ResetColor ();
            Write (prompt);
         }
         var split = f.ToString ().Split ('.');
         Print ("Integral part\t: ", split[0]);
         if (split.Length > 1)
            Print ("Factorial part\t: ", split[1]);
         WriteLine ();
         WriteLine ();
         Write ("Do you want to continue? (Y/N): ");
         if (ReadLine ()?.ToLower () is not "y") break;
         WriteLine ();
      }
   }

   // Prints each character in the given string with the given message.
   static void Print (string message, string str) {
      WriteLine ();
      Write (message);
      foreach (char i in str) Write ($"{i} ");
   }

}
