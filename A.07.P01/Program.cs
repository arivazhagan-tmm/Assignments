internal class Program {
   private static void Main (string[] args) {
      string[] testCases = new string[] {"-1.5E-2", "12345", "0.000325", "10.54E2", "1.5E2.5", "1.E3", "1.E+3", "12.54e3.", "12.", "12.e1", ".325", " +625 ",
                                         "6.25e0", "6.0e0", "6.25e-1", "+6.25E1", "*6.25", "1.625", "15a1", "1.567*2", "+-12", "12.-5", ".e1", "-0.325", "  12.456    " };
      foreach (var s in testCases) {
         double.TryParse (s, out double value);
         Console.WriteLine ($"\nText          : \"{s}\"\nCustom Parse  : {Parse (s)}\ndouble.Parse  : {value}\n------------\n");
      }
   }

   /// <summary> Parses the given string into double value and returns NaN if parsing fails </summary>
   static double Parse (string s) {
      s = s.Trim ();
      char sign = s[0];
      s = sign is '-' or '+' ? s[1..] : s;
      double value = 0.0;
      bool validDecimal = false;
      int expnt = 0; // Exponent
      for (int i = 0; i < s.Length; i++) {
         char c = s[i];
         switch (c) {
            case '.':
               validDecimal = i != 0 && i + 1 < s.Length - 1 && s[i + 1] is >= '0' and <= '9';
               if (!validDecimal) return double.NaN;
               break;
            case 'e' or 'E':
               var tmp = Parse (s[(i + 1)..]);
               if (tmp == (int)tmp) {
                  for (int j = 0; j < Math.Abs (tmp); j++)
                     value *= tmp < 0 ? 0.1 : 10;
               } else
                  value = double.NaN;
               i = s.Length;
               break;
            case >= '0' and <= '9':
               value = validDecimal ? value + (c - 48) * Math.Pow (10, --expnt) : value * 10 + c - 48;
               break;
            default: return double.NaN;
         }
      }
      value = Math.Round (value, s.Length);
      return sign is '-' ? -value : value;
   }
}