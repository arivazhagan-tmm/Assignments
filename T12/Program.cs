using static System.Console;
internal class Program {
   static void Main () {
      while (true) {
         Write ("\nEnter the no of rows\t : ");
         if (int.TryParse (ReadLine (), out int row)) {
            int num = 1;
            for (int i = 0; i < row; i++) {
               for (int j = 1; j <= row - i; j++) Write ("".PadLeft (2));
               for (int k = 0; k <= i; k++) {
                  num = k is 0 ? 1 : num * (i - k + 1) / k;
                  var tmp = num.ToString ();
                  Write (tmp.PadLeft (tmp.Length + 3));
               }
               WriteLine ();
            }
         } else break;
      }
   }
}