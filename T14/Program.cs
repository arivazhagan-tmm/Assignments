using System.Text;
using static System.Console;

internal class Program {
   // Prints the reduced form of user given sequence.
   static void Main () {
      string[] testCases = { "aaabccddd", "aaaabbbb", "abba", "aba", "cassette",
                            "entry", "eennttrraannccee", "eeennntttrrraaannnccceee" };
      Write ("Following are the sample test cases : ");
      WriteLine ();
      foreach (string testCase in testCases) {
         WriteLine ();
         Write ($"\t{testCase} - {GetReducedString (testCase)}");
      }
      WriteLine ();
      WriteLine ();
      while (true) {
         var str = GetResponse ("Type the sequence: ");
         var reduced = GetReducedString (str);
         var message = reduced?.Length is 0 ? "completely reduced."
                       : str.Equals (reduced) ? "not reduced." : $"reduced to: {reduced}";
         WriteLine ($"Sequence is {message}");
         WriteLine ();
         Write ("Do you want to continue? (Y/N): ");
         if (ReadLine ()?.ToLower () is not "y") break;
         WriteLine ();
      }
   }

   // Returns the reduced form of the given string by eliminating similar adjucent characters.
   static string GetReducedString (string str) {
      var reducedStr = new StringBuilder ();
      for (int i = 0, len = str.Length; i < len;) {
         if (i < len - 1 && str[i] == str[i + 1]) i += 2;
         else reducedStr.Append (str[i++]);
      }
      return reducedStr.ToString ();
   }

   // Getting the valid string sequence from the user for the given prompt.
   static string GetResponse (string prompt) {
      string sequence;
      bool isValid;
      do {
         Write (prompt);
         sequence = ReadLine () ?? "";
         var len = sequence?.Length;
         isValid = len >= 3;
         if (!isValid) {
            ForegroundColor = ConsoleColor.Red;
            Write ("\tSequence should contain atleast 3 characters and atleast 2 adjucent duplicates.");
            ResetColor ();
         }
         WriteLine ();
      } while (!isValid);
      return sequence ?? "";
   }
}