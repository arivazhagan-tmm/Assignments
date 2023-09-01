class Program {
   static void Main (string[] args) {
      if (int.TryParse (args[0], out var index) && index <= 25) {
         var (i, count, tmp) = (1, 0, 0);
         for (; i < int.MaxValue; i++) {
            if (IsArmstrong (i)) {
               tmp = i;
               count++;
            }
            if (count == index) {
               Console.WriteLine ($"{tmp}");
               return;
            }
         }
      }
      else
            Console.WriteLine("Please enter index between 1 and 25");
    }

   static bool IsArmstrong (int n) {
      double result = 0;
      var chars = n.ToString ();
      var len = chars.Length;
      foreach (char ch in chars)
         result += Math.Pow (double.Parse (ch.ToString ()), len);
      return result == n;
   }
}
