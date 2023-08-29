using static System.Console;
internal class Program {
   static void Main () {
      GetResponse ("Enter the number\t: ", out int a);
      GetResponse ("Enter the length\t: ", out int b);
      var (c, d) = (1, b.ToString ().Length + 1);
      WriteLine ();
      while (c <= b) WriteLine ($"{a} x" + $"{c}".PadLeft (d) + $" = {a * c++}");
   }

   static bool GetResponse (string prompt, out int n) {
      Write (prompt); return int.TryParse (ReadLine (), out n);
   }
}