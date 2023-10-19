using static System.Console;

internal class Program {

   // Gets an input string from the user and checks if it is an 'Isogram'.
   static void Main () {
      while (true) {
         Write ("Enter a valid word : ");
         var word = ReadLine () ?? "";
         bool isValid = !string.IsNullOrEmpty (word) && word.All (char.IsLetter);
         if (isValid) {
            Write (IsIsogram (word) ? "\t\t\tIsogram" : "\t\t\tNot Isogram");
            Write ("\nDo you want to continue? (Y/N) : ");
            if (ReadLine ()?.ToLower () is not "y") break;
            WriteLine ();
         }
      }
   }

   // Checks if the given string is an 'Isogram' or not. Isogram is a word that doesn't contain any duplicate letters.
   static bool IsIsogram (string word) => word.Distinct ().Count () == word.Length;
}