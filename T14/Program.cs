using System.Text;
using static System.Console;
internal class Program {
   static void Main () {
      while (true) {
         Write ("Type the sequence : ");
         var str = ReadLine ();
         if (!string.IsNullOrEmpty (str) && str.Length > 3) {
            var sb = new StringBuilder ();
            for (int i = 0, len = str.Length; i < len;) {
               if (i == len - 1) sb.Append (str[i++]);
               else if (str[i] == str[i + 1]) i += 2;
               else sb.Append (str[i++]);
            }
            WriteLine ($"\nSequence is reduced to\t : {sb}\n");
         }
      }
   }
}