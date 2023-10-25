using static System.Console;

class Program {

   // Prints the integral & factorial part of the decimal number given by the user.
   static void Main () {
      while (true) {
         GetResponse ("Enter a decimal number : ", out double f);
         var txt = f.ToString ();
         if (txt.Contains ('.')) {
            var split = txt.Split ('.');
            var (integral, factorial) = (split[0].Replace (".", ""), split[1]);
            Print ("Integral Part\t: ", integral);
            Print ("Factorial Part\t: ", factorial);
         } else Print ("Integral Part\t: ", txt);
         WriteLine ();
         WriteLine ();
         Write ("Do you want to continue? (Y/N) : ");
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

   // Getting the decimal value from the user for the given prompt.
   static void GetResponse (string prompt, out double f) {
      Write (prompt);
      while (!double.TryParse (ReadLine (), out f)) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ("\tPlease enter any decimal value!");
         ResetColor ();
         Write (prompt);
      }
   }
}
