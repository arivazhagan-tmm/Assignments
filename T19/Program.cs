using static System.Console;

class Program {
   // Prints the factorial of the user given number.
   static void Main () {
      while (true) {
         var prompt = "Enter the number: ";
         int n;
         Write (prompt);
         while (!int.TryParse (ReadLine (), out n) || n > 25) {
            ForegroundColor = ConsoleColor.Red;
            WriteLine ("\tPlease enter any whole number between 0 and 25.");
            ResetColor ();
            Write (prompt);
         }
         Write ($"Factorial of {n}: {GetFactorial (n)}");
         WriteLine ();
         WriteLine ();
         Write ("Do you want to continue? (Y/N): ");
         if (ReadLine ()?.ToLower () is not "y") break;
         WriteLine ();
      }
   }

   // Computes and returns the factorial of given number.
   static long GetFactorial (int n) {
      long fact = 1;
      do fact *= n * --n; while ((--n - 1) > 0);
      return fact;
   }
}
