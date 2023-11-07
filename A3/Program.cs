using System.Reflection;
using static System.Console;

internal class Program {
   // Prints all the valid words that can be formed by 7 letters.
   // The word is termed as pangram, if it contains all the 7 letters.
   // This function highlights the pangrams green in color.
   static void Main () {
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("A3.Data.words.txt");
      using var reader = stream != null ? new StreamReader (stream) : null;
      var result = new List<(string word, int score, ConsoleColor color)> ();
      int total = 0;
      if (reader is null) return;
      while (!reader.EndOfStream) {
         var word = reader.ReadLine () ?? "";
         if (IsValid (word, out int score)) {
            total += score;
            var color = score > word.Length ? ConsoleColor.Green : ForegroundColor;
            result.Add ((word, score, color));
         }
      }
      result = result.OrderByDescending (a => a.score).ThenBy (a => a.word).ToList ();
      foreach (var (word, score, color) in result) {
         ForegroundColor = color;
         WriteLine ($"{score,3}. {word}");
         ResetColor ();
      }
      var endLine = Enumerable.Repeat ("-", 4);
      foreach (var s in endLine) Write (s);
      WriteLine ();
      WriteLine ($"{total,3} total");
   }

   // Returns whether given string is valid or not based on certain conditions.
   // Updates the out parameter if the string is valid.
   static bool IsValid (string str, out int score) {
      var (bonus, len) = (7, str.Length);
      score = len > 4 ? len : 1;
      if (len < 4) return false;
      if (!str.Contains (mSplChar)) return false;
      if (!str.All (mChars.Contains)) return false;
      score += mChars.All (str.Contains) ? bonus : 0;
      return true;
   }

   static char[] mChars = new[] { 'U', 'X', 'A', 'T', 'L', 'N', 'E' };
   static char mSplChar = mChars[0]; // Special character
}