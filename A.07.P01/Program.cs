internal class Program {
   private static void Main (string[] args) {
      var testValues = new string[] {"10.54E23.4E3","-1.546234E-4","0","0.0", "12345", "0.00000325", "10.54E2", "1.5E2.5", "1.E3", "1.E+3", "12.54e3.", "12.", "12.e1", ".325", " +625 ",
                                     "6.25e0", "6.0e0", "6.25e-1", "+6.25E1", "*6.25", "10.625", "15a1", "1.567*2", "+-12", "12.-5", ".e1", "-0.325", "  12.456    " };
      foreach (var val in testValues) {
         double.TryParse (val, out double value);
         Console.WriteLine ($"\nText          : \"{val}\"\nCustom Parse  : {Parse (val.Trim ())}\ndouble.Parse  : {value}\n------------\n");
      }
   }

   /// <summary> Parses the given string into double value and returns NaN if parsing fails </summary>
   static double Parse (string str) {
      int sign = 1, start = 0, length = str.Length, exp = 0;
      char ch = str[start];
      if (ch is '-') {
         sign = -1;
         start++;
      } else if (ch is '+') start++;
      double value = 0.0;
      bool validDecimal = false;
      for (; start < length; start++) {
         ch = str[start];
         int num = ch - '0';
         switch (ch) {
            case '.':
               validDecimal = start != 0 && start + 1 <= length - 1 && str[start + 1] is >= '0' and <= '9';
               if (!validDecimal) return double.NaN;
               break;
            case 'e' or 'E':
               var subStr = str[(start + 1)..];
               if (subStr.Any (c => c is 'e' or 'E' or '.'))
                  return double.NaN;
               var tmp = Parse (subStr);
               value *= Math.Pow (10, tmp);
               start = length;
               break;
            case >= '0' and <= '9':
               if (validDecimal) {
                  exp--;
                  value += num * Math.Pow (10, exp);
               } else
                  value = value * 10 + num;
               break;
            default: return double.NaN;
         }
      }
      value = Math.Round (value, 3);
      return sign * value;
   }
}