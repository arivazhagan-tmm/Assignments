using static System.Console;
class Program {
   static void Main (string[] args) {
      while (true) {
         Write ("Enter the number\t: ");
         if (int.TryParse (ReadLine (), out int n) && n > 0) {
            var (str, result) = (n.ToString (), 0.0);
            var len = str.Length;
            foreach (var ch in str) result += Math.Pow (double.Parse (ch.ToString ()), len);
            WriteLine (result == n ? "Armstrong" : "Not Armstrong");
         } else break;
      }
   }
}
