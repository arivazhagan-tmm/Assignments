using static System.Console;
internal class Program {
   static void Main () {
      var rand = new Random ();
      WriteLine ($"Number\t: Digital Root \n");
      for (int i = 0; i < 50; i++) {
         int n = rand.Next (100, 99999), sum = 0, tmp = n;
         do {
            sum += tmp % 10;
            tmp /= 10;
            if (tmp is 0 && sum >= 10)
               (tmp, sum) = (sum, 0);
         } while (tmp > 0);
         WriteLine ($"{n}\t: {sum}");
      }
   }
}