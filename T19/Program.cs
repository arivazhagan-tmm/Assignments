using static System.Console;
class Program {
   static void Main (string[] args) {
      while (true) {
         Write ("Enter the number : ");
         if (int.TryParse (ReadLine (), out int n)) {
            long fact = 1;
            do fact *= n * --n; while ((--n - 1) > 0);
            Write ($"Factorial : {fact}\n");
         } else break;
      }
   }
}