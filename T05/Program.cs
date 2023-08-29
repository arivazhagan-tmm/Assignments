internal class Program {
   private static void Main (string[] args) {
      Random r = new ();
      for (int i = 0; i < 100; i++) {
         int n = r.Next (1, 500);
         bool isPrime = true;
         var proof = "";
         for (int j = 2; j < n; j++)
            if (n % j == 0) { isPrime = false; proof = $" {j} x {n / j} = {n}"; break; }
         Console.ForegroundColor = isPrime ? ConsoleColor.Green : Console.ForegroundColor;
         var output = isPrime ? $"{n}\t: Prime" : $"{n}\t: Not  {proof}";
         Console.WriteLine (output);
         Console.ResetColor ();
      }
   }
}