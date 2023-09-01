using static System.Console;

class Program {
   static void Main () {
      var words = File.ReadAllLines (@"C:\etc\words.txt");
      var result = words[0];
      foreach (var word in words) {
         if (IsAbecedarian (word)) {
            result = word.Length > result.Length ? word : result;
            WriteLine (word);
         }
      }
      WriteLine ($"Longest abecedarian word\t: {result}");
   }

   static bool IsAbecedarian (string str) {
      for (int i = 0; i < str.Length - 1; i++)
         if (str[i] > str[i + 1]) return false;
      return true;
   }
}
