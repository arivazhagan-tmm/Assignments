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
         var splits = f.ToString ().Split ('.');
         Print ("Integral part\t: ", splits[0]);
         if (splits.Length > 1)
            Print ("Factorial part\t: ", splits[1]);
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
      Write ($"{message}{string.Join (" ", str.ToCharArray ())}");
   }
}
