using static System.Console;
internal class Program {
   static void Main () {
      var rand = new Random ();
      WriteLine ($"Number\t: Reversed\n");
      for (int i = 0; i < 100; i++) {
         int n = rand.Next (100, 99999), result = 0, tmp = n;
         while (tmp > 0) {
            result = result * 10 + (tmp % 10);
            tmp /= 10;
         }
         var output = result == n ? $"{n}\t: {result}\t\t: Palindrome" : $"{n}\t: {result}";
         ForegroundColor = result == n ? ConsoleColor.Green : ForegroundColor;
         WriteLine (output);
         ResetColor ();
      }

   }
}