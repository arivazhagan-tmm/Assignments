namespace Eval;

class Program {
   static void Main (string[] args) {
      var rand = new Random ();
      Console.WriteLine ($"Number\t: Reversed\n");
      for (int i = 0; i < 100; i++) {
         int n = rand.Next (100, 99999), result = 0, tmp = n;
         while (tmp > 0) {
            result = result * 10 + (tmp % 10);
            tmp /= 10;
         }
         var output = result == n ? $"{n}\t: {result}\t\t: Palindrome" : $"{n}\t: {result}";
         Console.ForegroundColor = result == n ? ConsoleColor.Green : Console.ForegroundColor; 
         Console.WriteLine (output);
         Console.ResetColor ();
      }
   }
}
