using static System.Console;

internal class Program {
   // Prints the reduced form of user given sequence.
   static void Main () {
      string[] testCases = {"abba", "aaabccddd", "aaaabbbb", "aba", "cassette", "assassination",
                            "entry", "eennttrraannccee", "eeennntttrrraaannnccceee" };
      Write ("Following are the sample test cases : \n");
      foreach (string testCase in testCases)
         Write ($"\n\t{testCase} - {GetReducedString (testCase)}");
      Write ("\n\n");
      while (true) {
         var str = GetResponse ("Type the sequence: ");
         var reduced = GetReducedString (str);
         var message = reduced.Length is 0 ? "completely reduced."
                       : str.Equals (reduced) ? "not reduced." : $"reduced to: {reduced}";
         WriteLine ($"Sequence is {message}\n");
         Write ("Do you want to continue? (Y/N): ");
         if (ReadKey ().Key is not ConsoleKey.Y) break;
         Write ("\n\n");
      }
   }

   // Returns the reduced form of the given string by eliminating similar adjacent characters.
   static string GetReducedString (string str) {
      while (HasPair (str, out int index)) str = str.Remove (index, 2);
      return str;
   }

   // Checks if the given string has pair of similar adjacent characters or not.
   static bool HasPair (string str, out int index) {
      index = -1;
      for (int i = 0, len = str.Length; i < len - 1; i++)
         if (str[i] == str[i + 1]) {
            index = i;
            return true;
         }
      return false;
   }

   // Getting the valid string sequence from the user for the given prompt.
   static string GetResponse (string prompt) {
      string sequence;
      bool isValid;
      do {
         Write (prompt);
         sequence = ReadLine () ?? "";
         var len = sequence.Length;
         isValid = len >= 3;
         if (!isValid) {
            ForegroundColor = ConsoleColor.Red;
            Write ("\tSequence should contain atleast 3 characters and atleast 2 adjucent duplicates.");
            ResetColor ();
         }
         WriteLine ();
      } while (!isValid);
      return sequence;
   }
}