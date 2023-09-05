using static System.Console;
internal class Program {
   static void Main () {
      Write ("Enter the numbers count : ");
      if (int.TryParse (ReadLine (), out int n)) {
         int[] numbers = new int[n];
         for (int i = 1; i <= n; i++) {
            Write ($"Enter Number {i}: ");
            if (int.TryParse (ReadLine (), out int tmp))
               numbers[i - 1] = tmp;
            else {
               WriteLine ("Invalid Input!");
               i--;
            }
         }
         WriteLine ($"\nLCM : {LCM (numbers)}\nGCD : {GCD (numbers)}");
      }
   }

   static int GCD (params int[] numbers) {
      var (gcd, len, i) = (numbers[0], numbers.Length, 1);
      for (; i < len; i++) {
         var tmp = numbers[i];
         if (gcd is 1 || tmp is 1) return 1;
         else if (gcd is 0) gcd = tmp;
         else {
            gcd = gcd > tmp ? GCD (gcd - tmp, tmp) : GCD (gcd, tmp - gcd);
         }
      }
      return gcd;
   }

   static int LCM (int[] numbers) {
      var (lcm, len, i) = (1, numbers.Length, 0);
      for (; i < len; i++) {
         var tmp = numbers[i];
         lcm = lcm * tmp / GCD (lcm, tmp);
      }
      return lcm;
   }
}