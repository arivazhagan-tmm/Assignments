using static System.Console;

internal class Program {

   // Swaps digits of a number based on user given index values.
   private static void Main (string[] args) {
      var rand = new Random ();
      while (true) {
         var num = rand.Next (1000, 100000).ToString ();
         var len = num.Length;
         WriteLine ($"The number is {num}.");
         GetResponse ("Enter the first index\t: ", len, out int i);
         GetResponse ("Enter the second index\t: ", len, out int j);
         var (m, n) = (num[j], num[i]);
         Write ($"Result : ");
         for (int k = 0; k < len; k++) {
            if (k == i) Print (m);
            else if (k == j) Print (n);
            else Write ($" {num[k]}");
         }
         WriteLine ();
         WriteLine ();
         Write ("Do you want to continue? (Y/N) : ");
         if (ReadLine ()?.ToLower () is not "y") break;
         WriteLine ();
      }
   }

   // Getting a valid numeric value from the user for the given prompt.
   static void GetResponse (string prompt, int max, out int i) {
      Write (prompt);
      while (!int.TryParse (ReadLine (), out i) || i >= max) {
         ForegroundColor = ConsoleColor.Red;
         var message = i >= max ? $"\tIndex should be less than {max}" : "\tPlease enter valid numeric value!";
         WriteLine (message);
         ResetColor ();
         Write (prompt);
      }
   }

   // Prints the given character in green color.
   static void Print (char c) {
      ForegroundColor = ConsoleColor.Green;
      Write (" " + c);
      ResetColor ();
   }
}