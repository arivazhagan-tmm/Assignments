internal class Program {
   private static void Main () {
      while (true) {
         Console.Write ("Type the word : ");
         var word = Console.ReadLine ();
         if (word?.Length >= 3) {
            var (i, len) = (0, word.Length);
            bool isPalindrome = true;
            for (; i < len; i++) {
               if (word[i] != word[^(i + 1)]) {
                  isPalindrome = false;
                  break;
               }
            }
            Console.WriteLine (isPalindrome ? $"{word}\t: Palindrome\n" : $"{word}\t : Not a palindrome\n");
         } else
            break;
      }
   }
}