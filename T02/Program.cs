namespace T02;
using System.Text;
using static State;

class Program {
   static void Main () {
      Random rand = new ();
      for (int i = 0; i < 50; i++) PrintResult (rand.Next (1, 10000));
      while (true) {
         Console.Write (" Enter the number : ");
         if (int.TryParse (Console.ReadLine (), out int n)) PrintResult (n);
         else { Console.WriteLine ("Conversion Failed"); break; }
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
            (E, true) => (F, none),
            (E, false) => (F, () => {
               var extnd = prev is 0 ? " Thousand " : " ";
               if (rem is 1 && prev != 0) word = word.Remove (0, words[prev].ToString ().Length);
               word.Insert (0, words[val] + extnd);
               roman.Clear ();
               roman.Append ("Limit Exceeded");
            }
            ),
            (F, false) => (G, () => word.Insert (0, $"{remWord} Lakh ")),
            _ => (G, none)
         };
         todo ();
      }
      return (word.ToString (), roman.ToString ());
   }

   static void PrintResult (int n) {
      var (word, roman) = Convert (n);
      Console.WriteLine ($" Number\t: {n}\n Word\t: {word}\n Roman\t: {roman}\n");
   }

   static Dictionary<int, string> words = new () {
         {0, "Zero"},{1, "One"}, {2, "Two"},{3, "Three"},{4, "Four"},{5, "Five"},{6, "Six"}, {7, "Seven"}, {8, "Eight"},{9, "Nine"},{10, "Ten" },
         {11, "Eleven"}, {12, "Twelve"}, {13, "Thirteen"}, {14, "Fourteen"}, {15, "Fifteen"}, {16, "Sixteen"},{17, "Seventeen"},{18, "Eighteen"},
         {19, "Nineteen"},{20, "Twenty"},{30, "Thirty"},{40, "Forty"},{50, "Fifty"},{60, "Sixty"},{70, "Seventy"},{80, "Eighty"},{90, "Ninety"},
      };

   static Dictionary<int, string> romans = new () {
         { 0, ""},  { 1, "I"}, { 2, "II"},{ 3, "III"},{ 4, "IV"},{ 5, "V"},{ 6, "VI"}, { 7, "VII"}, { 8, "VIII"},{ 9, "IX"}, { 40, "XL"},
         { 90, "XC"},{ 400, "CD"}, { 900, "CM"}
      };
}

public enum State {
   A, B, C, D, E, F, G
}
