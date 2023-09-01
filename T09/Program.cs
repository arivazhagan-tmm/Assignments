using System.Text;
using static System.Console;
internal class Program {
   static void Main () {
      while (true) {
         Write ("\nEnter the no of rows\t : ");
         if (int.TryParse (ReadLine (), out int row)) {
            var (i, padding) = (1, WindowWidth / 3);
            for (; i <= row;) Print ((2 * i++) - 1, padding--);
            for (; i >= 1;) Print ((2 * i--) - 1, padding++);
         } else break;
      }
   }

   static void Print (int count, int padding) {
      CursorLeft = padding;
      WriteLine (new string ('*', count));
   }
}