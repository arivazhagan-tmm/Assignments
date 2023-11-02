using static System.Console;

internal class Program {
   // Finds the LCM and GCD of user given integers.
   static void Main () {
      Write ("Enter the numbers count: ");
      if (int.TryParse (ReadLine (), out int n)) {
         int[] arr = new int[n];
         for (int i = 1; i <= n; i++) {
            Write ($"Enter Number {i}: ");
            if (int.TryParse (ReadLine (), out int tmp))
               arr[i - 1] = tmp;
            else {
               WriteLine ("Invalid Input!");
               i--;
            }
         }
         WriteLine ($"\nLCM: {LCM (arr)}\nGCD: {GCD (arr)}");
      }
   }

   // Computes and returns the GCD of given integers.
   static int GCD (params int[] arr) => arr.Aggregate (GCD);

   // Computes and returns the GCD of given two integers.
   static int GCD (int a, int b) => b is 0 ? a : GCD (b, a % b);

   // Computes and returns the LCM of given integers.
   static int LCM (int[] arr) {
      int lcm = 1;
      for (int i = 0, len = arr.Length; i < len; i++) {
         var tmp = arr[i];
         lcm = lcm * tmp / GCD (lcm, tmp);
      }
      return lcm;
   }
}