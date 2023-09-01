using System.Text;
using static System.Console;

class Program {
   private static void Main (string[] args) {
      while (true) {
         Write ("\nType the 3 Letter word : ");
         var word = ReadLine ();
         if (!string.IsNullOrEmpty (word)) {
            WriteLine ($"\nPermutaions:");
            var len = word.Length;
            var padding = 15;
            var sb = new StringBuilder ();
            for (int i = 0; i < len; i++) {
               for (int j = i; j - i < len; j++) sb.Append (word[j % len]);
               WriteLine ($"{sb}".PadLeft (padding));
               WriteLine ($"{new string (sb.ToString ().Reverse ().ToArray ())}".PadLeft (padding));
               sb.Clear ();
            }
         } else break;
      }
   }
}