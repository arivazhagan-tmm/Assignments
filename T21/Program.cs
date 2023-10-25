using static System.Console;

internal class Program {

   // Performs the swapping for the user's input.
   private static void Main () {
      GetResponse ("Enter the first number: ", out int a);
      GetResponse ("Enter the second number: ", out int b);
      Swap (ref a, ref b);
      WriteLine ();
      WriteLine ($"After swapping, first number is {a} and second number is {b}.");
   }

   // Getting the numeric response from the user for the given prompt.
   static void GetResponse (string prompt, out int a) {
      Write (prompt);
      while (!int.TryParse (ReadLine (), out a)) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ("\tPlease enter any numeric value!");
         ResetColor ();
         Write (prompt);
      }
   }

   // Swaps given two numbers and stores into their actual address.
   static void Swap (ref int a, ref int b) => (a, b) = (b, a);

}