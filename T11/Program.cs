using static System.Console;
using static System.Math;
internal class Program {
   static void Main () {
      var rand = new Random ();
      WriteLine ($"Number\t: Digital Root \n");
      for (int i = 0; i < 50; i++) {
         int n = rand.Next (100, 99999), result = n;
         while (Abs (result) >= 10) {
            var (sum, rem) = (0, result % 10);
            while (result != 0) {
               sum += rem;
               result /= 10;
               rem = result % 10;
            }
            result = sum;
         }
         WriteLine ($"{n}\t: {result}");
      }
   }
}