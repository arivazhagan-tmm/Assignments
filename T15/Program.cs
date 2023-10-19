using static System.Console;

internal class Program {
   static void Main () {
      // While loop runs till the user enters a valid word or wishes to continue.
      while (true) {
         Write ("Enter a valid word : ");
         var word = ReadLine () ?? "";
         // word is valid when it is not empty and formed only with alphabets.
         bool isValid = !string.IsNullOrEmpty (word) && word.All (char.IsLetter);
         if (isValid) {
            Write (IsIsogram (word) ? "\t\t\tIsogram" : "\t\t\tNot Isogram");
            Write ("\nDo you want to continue? (Y/N) : ");
            // Terminating the program if user's input is other than y or Y.
            if (ReadLine ()?.ToLower () is not "y") break;
            WriteLine ();
         }
         // If the word is not valid, user is prompted to enter the valid word.
      }
   }

   // The word is termed as ISOGRAM if it doesn't contain any duplicate alphabet.
   static bool IsIsogram (string word) => word.Distinct ().Count () == word.Length;
}