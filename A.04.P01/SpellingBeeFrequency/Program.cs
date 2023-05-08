using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program {
   private static void Main (string[] args) {
      Dictionary<char, int> charFrequency = new ();
      foreach (var character in File.ReadAllText ("C:/etc/words.txt")) {
         if (character >= 'A' && character <= 'Z')
            charFrequency[character] = charFrequency.ContainsKey (character) ? charFrequency[character] += 1 : 0;
      }
      charFrequency = charFrequency.OrderByDescending (pair => pair.Value).ToDictionary (pair => pair.Key, pair => pair.Value);
      int padLength = charFrequency.Values.First ().ToString ().Length;
      foreach (var (key, value) in charFrequency.Take (7))
         Console.WriteLine ($"{key,2} {value.ToString ().PadLeft (padLength)}");
   }
}