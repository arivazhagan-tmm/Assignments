using static System.Console;
internal class Program {
   static void Main () {
      while (true) {
         Write ("Type the word\t: ");
         var word = ReadLine ();
         if (!string.IsNullOrEmpty (word)) Write (Isogram (word) ? "\t\tIsogram\n" : "\t\tNot Isogram\n");
         else break;
      }
   }
   static bool Isogram (string word) => word.Distinct ().Count () == word.Length;
}