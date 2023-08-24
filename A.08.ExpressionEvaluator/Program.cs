namespace Eval;
using System.Text;
using static State;

class Program {
   static void Main (string[] args) {
      Random rand = new ();
      for (int i = 0; i < 50; i++) {
         var n = rand.Next (1, 10000);
         var (word, roman) = Convert (n);
         Console.WriteLine ($" Number\t: {n}\n Word\t: {word}\n Roman\t: {roman}\n");
      }
      while (true) {
         Console.Write (" Enter the number : ");
         var str = Console.ReadLine ();
         if (int.TryParse (str, out int n)) {
            var (word, roman) = Convert (n);
            Console.WriteLine ($" Number\t: {n}\n Word\t: {word}\n Roman\t: {roman}\n");
         } else { Console.WriteLine ("Conversion Failed"); break; }
      }
   }

   static (string word, string roman) Convert (int n) {
      int tmp = n, prev = 0, last = 0;
      Action none = () => { }, todo;
      State s = A;
      StringBuilder word = new (), roman = new ();
      while (tmp > 0) {
         var rem = tmp % 10; // Remainder
         tmp /= 10;
         var (remWord, remRoman) = ((Words)rem, (Roman)rem);
         bool remZero = rem is 0;
         var val = int.Parse (rem is 1 ? $"{rem}{prev}" : $"{rem}0");
         (s, todo) = (s, remZero) switch {
            (A, true) => (B, none),
            (A, false) => (B, () => { word.Insert (0, remWord); roman.Append (remRoman); prev = rem; last = rem; }),
            (B, true) => (C, none),
            (B, false) => (C, () => {
               if (rem is 1) word.Clear (); word.Insert (0, (Words)val + " ");
               switch (val) {
                  case 40 or 90: roman.Insert (0, (Roman)val); break;
                  case >= 50 and < 90:
                     for (int j = 0, k = (val - 50) / 10; j < k; j++) roman.Insert (0, (Roman)10); roman.Insert (0, (Roman)50); break;
                  default: for (int j = 0, k = val / 10; j < k; j++) roman.Insert (0, (Roman)10); break;
               }
               last = rem;
            }
            ),
            (C, true) => (D, none),
            (C, false) => (D, () => {
               var extnd = last is 0 ? "" : "and"; word.Insert (0, $"{remWord} Hundred {extnd} ");
               switch (val) {
                  case 40 or 90: roman.Insert (0, (Roman)(val * 10)); break;
                  case < 50: for (int j = 0, k = val / 10; j < k; j++) roman.Insert (0, (Roman)100); break;
                  default: for (int j = 0, k = (val - 50) / 10; j < k; j++) roman.Insert (0, (Roman)100); roman.Insert (0, (Roman)500); break;
               }
            }
            ),
            (D, false) => (E, () => {
               word.Insert (0, $"{remWord} Thousand "); prev = rem;
               for (int j = 0, k = val / 10; j < k; j++) roman.Insert (0, (Roman)1000);
            }
            ),
            (D, true) => (E, () => prev = rem),
            (E, false) => (F, () => {
               var extnd = prev is 0 ? " Thousand " : " ";
               if (rem is 1 && prev != 0) word = word.Remove (0, ((Words)prev).ToString ().Length);
               word.Insert (0, (Words)val + extnd);
               roman.Clear ();
               roman.Append ("Limit Exceeded");
            }
            ),
            (E, true) => (F, none),
            (F, false) => (G, () => word.Insert (0, $"{remWord} Lakh ")),
            _ => (G, none)
         };
         todo ();
      }
      return (word.ToString (), roman.ToString ());
   }
}

public enum State {
   A, B, C, D, E, F, G
}

public enum Words {
   Zero = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9,
   Eleven = 11, Twelve = 12, Thirteen = 13, Fourteen = 14, Fifteen = 15, Sixteen = 16, Seventeen = 17, Eighteen = 18, Nineteen = 19,
   Ten = 10, Twenty = 20, Thirty = 30, Forty = 40, Fifty = 50, Sixty = 60, Seventy = 70, Eighty = 80, Ninety = 90
}

public enum Roman {
   I = 1, II = 2, III = 3, IV = 4, V = 5, VI = 6, VII = 7, VIII = 8,
   IX = 9, X = 10, XL = 40, L = 50, XC = 90, C = 100, CD = 400, D = 500, CM = 900, M = 1000
}
