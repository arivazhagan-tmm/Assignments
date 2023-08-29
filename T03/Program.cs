internal class Program {
   static void Main (string[] args) {
      Console.Write ("Enter the number of sequence : ");
      if (int.TryParse (Console.ReadLine (), out int n)) {
         var (a, b) = (0, 1);
         Console.Write ($"{a}, {b}");
         for (int i = 2; i < n; i++) {
            int c = a + b;
            Console.Write ($", {c}");
            (a, b) = (b, c);
         }
         Console.WriteLine ();
      } else { Console.WriteLine ("Invalid Input"); }
   }
}