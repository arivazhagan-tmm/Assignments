using static System.Console;

internal class Program {

   // Swaps digits of a number based on user given index values.
   private static void Main () {
      var rand = new Random ();
      while (true) {
         var num = rand.Next (1000, 100000).ToString ();
         var len = num.Length;
         WriteLine ($"The number is {num}.");
         GetResponse ("Enter the first index\t: ", len, out int i);
         GetResponse ("Enter the second index\t: ", len, out int j);
         Write ($"Result:");
         for (int k = 0; k < len; k++) {
            if (k == i) Print (num[j]);
            else if (k == j) Print (num[i]);
            else Write ($" {num[k]}");
         }
         WriteLine ();
         WriteLine ();
         Write ("Do you want to continue? (Y/N): ");
         if (ReadLine ()?.ToLower () is not "y") break;
         WriteLine ();
      }
   }

   // Getting a valid numeric value from the user for the given prompt.
   static void GetResponse (string prompt, int max, out int i) {
      Write (prompt);
      while (!int.TryParse (ReadLine (), out i) || i >= max || i < 0) {
         ForegroundColor = ConsoleColor.Red;
         string message1 = $"\tPlease choose the index between 0 and {max - 1}.",
                message2 = "\tPlease enter valid numeric value.";
         var message = i >= max || i < 0 ? message1 : message2;
         WriteLine (message);
         ResetColor ();
         Write (prompt);
      }
   }

   // Prints the given character in green color.
   static void Print (char c) {
      ForegroundColor = ConsoleColor.Green;
      Write ($" {c}");
      ResetColor ();
   }
}