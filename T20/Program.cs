using static System.Console;
class Program {
   static void Main () {
      while (true) {
         Write ("\nEnter the Number\t: ");
         var txt = ReadLine ();
         if (double.TryParse (txt, out double f)) {
            if (txt.Contains ('.')) {
               var split = txt.Split ('.');
               var (integral, factorial) = (split[0].Replace (".", ""), split[1]);
               Print ("\nIntegral Part : ", integral);
               Print ("\nFactorial Part : ", factorial);
            } else Print ("\nIntegral Part : ", txt);
         } else break;
      }
   }
   static void Print (string message, string str) {
      WriteLine (message);
      foreach (char i in str) Write ($"{i} ");
   }
}
