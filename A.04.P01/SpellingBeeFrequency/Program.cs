using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program {
   private static void Main (string[] args) {
      Dictionary<char, int> dict = new ();
      foreach (var ch in File.ReadAllText ("C:/etc/words.txt").Where (ch => ch >= 'A' && ch <= 'Z'))
         dict[ch] = dict.TryGetValue (ch, out var val) ? ++val : 1;
      dict = dict.OrderByDescending (p => p.Value).ToDictionary (k => k.Key, p => p.Value);
      int len = dict.Values.First ().ToString ().Length;
      foreach (var (k, val) in dict.Take (7))
         Console.WriteLine ($"{k,2} {val.ToString ().PadLeft (len)}");
   }
}