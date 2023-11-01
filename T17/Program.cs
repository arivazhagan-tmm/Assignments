using System.Text;
using static System.Console;

class Program {
   // Generates and prints the permutation of user given 3 letter word.
   static void Main () {
      while (true) {
         var word = GetResponse ("Type the 3 letter word: ");
         WriteLine ($"\nPermutations:");
         var (len, padding) = (word.Length, 15);
         StringBuilder sb = new ();
         for (int i = 0; i < len; i++) {
            for (int j = i; j - i < len; j++) sb.Append (word[j % len]);
            WriteLine ($"{sb}".PadLeft (padding));
            WriteLine ($"{new string (sb.ToString ().Reverse ().ToArray ())}".PadLeft (padding));
            sb.Clear ();
         }
         Write ("\nDo you want to continue? (y/n): ");
         if (ReadKey ().Key is not ConsoleKey.Y) break;
         WriteLine ("\n");
      }
   }

   // Getting valid response from the user for the given prompt.
   static string GetResponse (string prompt) {
      bool isValid;
      string response;
      do {
         Write (prompt);
         response = ReadLine ()!;
         isValid = response.Length is 3;
         if (!isValid) {
            ForegroundColor = ConsoleColor.Red;
            Write ("\tThe word should contain 3 letters.\n");
            ResetColor ();
         }
      } while (!isValid);
      return response;
   }
}