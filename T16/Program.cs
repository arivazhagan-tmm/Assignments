using System.Reflection;
using static System.Console;

class Program {
   static void Main () {
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("T16.Data.words.txt");
      using var reader = stream != null ? new StreamReader (stream) : null;
      if (reader != null) {
         var result = "";
         while (!reader.EndOfStream) {
            var word = reader?.ReadLine ()?.Trim ();
            if (IsAbecedarian (word ?? "")) {
               result = word?.Length > result.Length ? word : result;
               WriteLine (word);
            }
         }
         WriteLine ($"\nLongest abecedarian word\t: {result}");
      }
   }

   static bool IsAbecedarian (string str) {
      for (int i = 0; i < str.Length - 1; i++)
         if (str[i] > str[i + 1]) return false;
      return true;
   }
}
