using System.Text;
using static System.Console;

class Program {
   // Generates and prints the permutation of user given 3 letter word.
   private static void Main () {
      while (true) {
         var word = GetResponse ("Type the 3 Letter word : ");
         WriteLine ($"\nPermutaions:");
         var (len, padding) = (word.Length, 15);
         var sb = new StringBuilder ();
         for (int i = 0; i < len; i++) {
            for (int j = i; j - i < len; j++) sb.Append (word[j % len]);
            WriteLine ($"{sb}".PadLeft (padding));
            WriteLine ($"{new string (sb.ToString ().Reverse ().ToArray ())}".PadLeft (padding));
            sb.Clear ();
         }
         WriteLine ();
         Write ("Do you want to continue? (Y/N): ");
         if (ReadLine ()?.ToLower () is not "y") break;
         WriteLine ();
      }
   }

   // Getting valid response from the user for the given prompt.
   static string GetResponse (string prompt) {
      bool isValid;
      string response;
      do {
         Write (prompt);
         response = ReadLine () ?? "";
         isValid = !string.IsNullOrEmpty (response) && response.Length is 3;
         if (!isValid) {
            ForegroundColor = ConsoleColor.Red;
            Write ("\t The word should contain 3 letters.");
            ResetColor ();
            WriteLine ();
         }
      } while (!isValid);
      return response;
   }
}