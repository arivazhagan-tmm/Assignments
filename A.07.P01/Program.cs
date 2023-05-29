
/// <summary>
/// Logic used in Parsing Implementation : 
/// 1. Given string is invalid if it contains more than one '.' or 'e' or 'E' or alphabets other than 'e' or 'E' in which cases NaN is returned;
/// 
/// 2. a. If string is valid and contains any '.' character then split the string by '.' and store them in an array, Initialize variable Value = 0.0.
///    b. Example : string is "1.5" --> split1 = [1] , split2 = [5] length of split1 - 1 is stored in a variable called power.
///    c. For each character in string , try to parse the character as integer and then multiply by 10 to the power.
///    d. Value += 1x10^power since power is 0 in this case , value is 1.
///    e. Decrement the power value by -1 in each iteration i.e, power-- and hence power is -1
///    f. If character is '.' this can be avoided by giving continue statement and then proceed to next character 5
///    g. Value += 5x10^power since power is decreased to -1 in previous, 5x10^(-1) gives 0.5 and hence value is 1.5 which will be returned.
///    
/// 3. a. If string contains any character 'e' or 'E' then follow the step 2 until character 'E' is found.
///    b. When character 'E' is found then take the substring after E's index and try to parse them as integer.
///    c. On successful parsing, take the out value as power and then perform Value *= 10^(power) and return the same.
///    d. If parsing fails return NaN. 
///       Example : "1.5E-1" here parsing "-1" as integer gives -1 which is valid but "1.5E" or "1.5E-" or "1.5E*1" gives invalid parsing hence return NaN.
///    e. If string has character 'E' or 'e' and also has '.' in succeeding indices, return NaN. For example 2E1.5 cannot be parsed.
///    
/// 4. a. If string starts with any sign character either '+' or '-' remove the sign from the string and perform STEPS 2 and 3
///    b. On succesfull parsing, if sign character is '-' return the Value as negative. Example :  return -Value
/// </summary>
internal class Program {
   private static void Main (string[] args) {
      string[] testCases = new string[] { "+625", "+6.25E1", "*6.25", "1.625", "15a1", "1.567*2",
                                          "-0.325", ".325", "1.5E2", "-1.5E-2", "1.5E2.5", "1.E3", "1.E+3" };
      foreach (var s in testCases) {
         double.TryParse (s, out double value);
         Console.WriteLine ($"\nText          : {s}\nCustom Parse  : {Parse (s)}\ndouble.Parse  : {value}\n------------\n");
      }
   }

   /// <summary> Parses the given string into double value and returns NaN if parsing fails </summary>
   static double Parse (string s) {
      // string s is invalid if it contains more than one 'e' or 'E' character or '.' character or any other alphabets hence return NaN.
      if (s.Where (IsCharE).Count () > 1 || s.Where (c => c is '.').Count () > 1 || s.Any (c => c < 122 && c > 65 && !IsCharE (c)))
         return double.NaN;
      double value = 0.0;
      char firstChar = s[0];
      s = firstChar is '-' or '+' ? s[1..] : s;
      var split = s.Contains ('.') ? s.Split ('.') : new string[] { s };
      // returning NaN if string s is in the format 1E1.5 or 1E-1.5 ( Point 4 in the above document )
      if (split.Length > 1 && split[0].Any (IsCharE)) return double.NaN;
      var power = split.Length == 1 && split[0].Any (IsCharE) ? split[0].IndexOf (split[0].Where (IsCharE).First ()) - 1 : split[0].Length - 1;
      foreach (var c in s) {
         if (c is '.')
            continue;
         // If character 'e' or 'E' is found and string s is in the format 1.5E-5 or 1.e5 or 1.5E+5 etc..
         if (IsCharE (c)) {
            value *= int.TryParse (s[(s.IndexOf (c) + 1)..].ToString (), out power) ? Math.Pow (10, power) : double.NaN;
            break;
         }
         // converting to character into a numerical values untill character 'e' or 'E' is found
         else if (int.TryParse (c.ToString (), out int n)) {
            value += n * Math.Pow (10, power);
            power--;
         }
         // returning NaN if character is a symbol other than '-' or '+' 
         else return double.NaN;
      }
      return firstChar is '-' ? -value : value;
   }

   /// <summary> Returns true if given chracter is e or E otherwise returns false </summary>
   static bool IsCharE (char c) => c is 'e' or 'E';
}