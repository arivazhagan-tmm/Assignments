namespace T02;
using System.Text;
using static State;
using static System.Console;

class Program {
   static void Main () {
      Random rand = new ();
      for (int i = 0; i < 100; i++) ToWordAndRoman (rand.Next (1, 4000));
      while (true) {
         Write ("Enter the number : ");
         if (int.TryParse (ReadLine (), out int n)) {
            if (n <= 3999) ToWordAndRoman (n);
            else WriteLine ("Max Limit is 3999\n");
         } else { WriteLine ("Conversion Failed"); break; }
      }
   }

   static (string word, string roman) Convert (int n) {
      var (tmp, prev, last) = (n, 0, 0);
      Action none = () => { }, todo;
      State s = A;
      StringBuilder word = new (), roman = new ();
      while (tmp > 0) {
         var rem = tmp % 10; // Remainder
         tmp /= 10;
         var (remWord, remRoman) = (words[rem], romans[rem]);
         bool remZero = rem is 0;
         var val = int.Parse (rem is 1 ? $"{rem}{prev}" : $"{rem}0");
         (s, todo) = (s, remZero) switch {
            (A, true) => (B, none),
            (A, false) => (B, () => { word.Insert (0, remWord); roman.Append (remRoman); prev = rem; last = rem; }),
            (B, true) => (C, none),
            (B, false) => (C, () => {
               if (rem is 1) word.Clear (); word.Insert (0, words[val] + " ");
               switch (val) {
                  case 40 or 90: roman.Insert (0, romans[val]); break;
                  case >= 50 and < 90:
                     for (int j = 0, k = (val - 50) / 10; j < k; j++) roman.Insert (0, "X"); roman.Insert (0, "L"); break;
                  default: for (int j = 0, k = val / 10; j < k; j++) roman.Insert (0, "X"); break;
               }
               last = rem;
            }
            ),
            (C, true) => (D, none),
            (C, false) => (D, () => {
               var extnd = last is 0 ? "" : "and"; word.Insert (0, $"{remWord} Hundred {extnd} ");
               switch (val) {
                  case 40 or 90: roman.Insert (0, romans[val * 10]); break;
                  case < 50: for (int j = 0, k = val / 10; j < k; j++) roman.Insert (0, "C"); break;
                  default: for (int j = 0, k = (val - 50) / 10; j < k; j++) roman.Insert (0, "C"); roman.Insert (0, "D"); break;
               }
            }
            ),
            (D, true) => (E, () => prev = rem),
            (D, false) => (E, () => {
               word.Insert (0, $"{remWord} Thousand "); prev = rem;
               for (int j = 0, k = val / 10; j < k; j++) roman.Insert (0, "M");
            }
            ),
            _ => (G, none)
         };
         todo ();
      }
      return (word.ToString (), roman.ToString ());
   }

   static void ToWordAndRoman (int n) {
      var (word, roman) = Convert (n);
      WriteLine ($"Number\t: {n}\n");
      ForegroundColor = ConsoleColor.Green;
      WriteLine ("Conversion By Digits Count");
      ResetColor ();
      WriteLine ($"Word\t: {GetWord (n)}\nRoman\t: {GetRoman (n)}\n");
      ForegroundColor = ConsoleColor.Green;
      WriteLine ("Conversion By State Machines");
      ResetColor ();
      WriteLine ($"Word\t: {word}\nRoman\t: {roman}");
      WriteLine ("_________________________________________");
   }

   /// <summary> Returns word format of given number </summary>
   static string GetWord (int n) {
      var( word, str) = ("", n.ToString());
      var (len, firstDigit, lastDigit) = (str.Length, int.Parse (str[0].ToString ()), n % 10);
      Action todo = () => { };
      todo = len switch {
         2 => () => {
            var tmp = firstDigit is 0 || lastDigit is 0 || n is >= 10 and <= 20 ? words[n] : $"{words[firstDigit * 10]} {words[lastDigit]}";
            word = tmp;
         }
         ,
         3 => () => {
            var hundreds = n % 100 is 0;
            var extension = hundreds ? "Hundred" : "Hundred and";
            var result = hundreds ? "" : GetWord (n - (firstDigit * 100));
            word = $"{words[firstDigit]} {extension} {result}";
         }
         ,
         4 => () => word = $"{words[firstDigit]} Thousand {GetWord (n - (firstDigit * 1000))}",
         _ => () => word = words[n]
      };
      todo ();
      return word;
   }

   /// <summary> Returns roman format of given number </summary>
   static string GetRoman (int n) {
      string? roman;
      if (n <= 10) roman = romans[n];
      else if (n is > 10 and < 40) roman = $"X{GetRoman (n - 10)}";
      else if (n is >= 40 and < 50) roman = $"XL{GetRoman (n - 40)}";
      else if (n is >= 50 and < 90) roman = $"L{GetRoman (n - 50)}";
      else if (n is >= 90 and < 100) roman = $"XC{GetRoman (n - 90)}";
      else if (n is >= 100 and < 400) roman = $"C{GetRoman (n - 100)}";
      else if (n is >= 400 and < 500) roman = $"CD{GetRoman (n - 400)}";
      else if (n is >= 500 and < 900) roman = $"D{GetRoman (n - 500)}";
      else if (n is >= 900 and < 1000) roman = $"CM{GetRoman (n - 900)}";
      else if (n is >= 1000 and < 4000) roman = $"M{GetRoman (n - 1000)}";
      else roman = "Limit Exceeded";
      return roman;
   }

   static Dictionary<int, string> words = new () {
         {0, "Zero"}, {1, "One"}, {2, "Two"}, {3, "Three"}, {4, "Four"}, {5, "Five"}, {6, "Six"}, {7, "Seven"}, {8, "Eight"}, {9, "Nine"}, {10, "Ten" },
         {11, "Eleven"}, {12, "Twelve"}, {13, "Thirteen"}, {14, "Fourteen"}, {15, "Fifteen"}, {16, "Sixteen"}, {17, "Seventeen"}, {18, "Eighteen"},
         {19, "Nineteen"}, {20, "Twenty"}, {30, "Thirty"}, {40, "Forty"}, {50, "Fifty"}, {60, "Sixty"}, {70, "Seventy"}, {80, "Eighty"}, {90, "Ninety"},
      };

   static Dictionary<int, string> romans = new () {
         { 0, ""}, { 1, "I"}, { 2, "II"}, { 3, "III"}, { 4, "IV"}, { 5, "V"}, { 6, "VI"}, { 7, "VII"},
         { 8, "VIII"}, { 9, "IX"}, {10, "X" }, { 40, "XL"}, {50, "L" }, { 90, "XC"}, { 100, "C"},
         { 400, "CD"}, { 500, "D"}, { 900, "CM"}, { 1000, "M"}
      };
}

public enum State {
   A, B, C, D, E, F, G
}
