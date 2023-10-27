using static System.Console;

class Program {
   // Prints a number from the consecutive armstrong numbers at the user given index.
   // User is allowed to enter the index through the command prompt.
   static void Main (string[] args) {
      if (int.TryParse (args[0], out var index) && index <= 25) {
         var (i, count, tmp) = (1, 0, 0);
         while (count != index)
            (tmp, count) = IsArmstrong (i++) ? (i - 1, count + 1) : (tmp, count);
         WriteLine ($"Armstrong number at index {index} is {tmp}.");
      } else WriteLine ("Please enter index between 1 and 25.");
   }

   // Checks whether the given number is armstrong or not.
   static bool IsArmstrong (int number) {
      var (str, res) = (number.ToString (), 0.0);
      var len = str.Length;
      foreach (var ch in str) res += Math.Pow (double.Parse (ch.ToString ()), len);
      return res == number;
   }
}
