internal class Program {
   private static void Main (string[] args) {
      string[] testCases = new string[] {"12345", "0.000325", "10.54E2", "-1.5E-2", "1.5E2.5", "1.E3", "1.E+3", "12.54e3.", "12.", "12.e1", ".325", " +625 ",
                                         "6.25e0", "6.0e0", "+6.25E1", "*6.25", "1.625", "15a1", "1.567*2", "+-12", "12.-5", ".e1", "-0.325", " 12.456  " };
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
      int expnt = 0; //Exponent
      for (int i = 0; i < s.Length; i++) {
         //'.' exists and is not a first character or a last character and it's adjucent character is a number
         if (s[i] is '.' && i != 0 && i + 1 < s.Length - 1 && s[i + 1] is >= '0' and <= '9')
            validDecimal = true;
         else if (s[i] is >= '0' and <= '9') {
            var tmp = int.Parse (s[i].ToString ());
            value = validDecimal ? value + tmp * Math.Pow (10, --expnt) : value * 10 + tmp;
         } else if (s[i] is 'e' or 'E' && int.TryParse (s[(i + 1)..].ToString (), out expnt)) {
            value *= Math.Pow (10, expnt);
            break;
         } else return double.NaN;
      }
      value = Math.Round (value, s.Length);
      return sign is '-' ? -value : value;
   }
}