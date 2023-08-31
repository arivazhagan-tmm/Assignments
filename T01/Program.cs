using System.Text;
using static System.Console;
internal class Program {
   static void Main () {
      while (true) {
         Write ("\nEnter the number\t: ");
         if (int.TryParse (ReadLine (), out int n)) {
            var (Binary, Hexa) = Convert (n);
            WriteLine ($"Binary Format\t\t: {Binary}\nString Conversion\t: {Convert (n, 2)}" +
               $"\nHexaDecimal Format\t: {Hexa}\nString Conversion\t: {Convert (n, 16)}");
         } else
            break;
      }
   }

   static (string Binary, string Hexa) Convert (int n) {
      StringBuilder sb = new ();
      var tmp = n;
      while (tmp > 0) {
         sb.Insert (0, tmp % 2);
         tmp /= 2;
      }
      var binary = sb.ToString ();
      sb.Clear ();
      tmp = n;
      while (tmp > 0) {
         var rem = tmp % 16;
         var str = rem > 9 ? hexaPairs[rem] : rem.ToString ();
         sb.Insert (0, str);
         tmp /= 16;
      }
      return (binary, sb.ToString ());
   }

   static string Convert (int n, int b) => System.Convert.ToString (n, b);

   static Dictionary<int, string> hexaPairs = new () {
      {10, "A" },{11, "B" },{12, "C" },{13, "D" },{14, "E" },{15, "F" },
   };
}