using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program {
   private static void Main (string[] args) {
      Dictionary<char, int> charFrequency = new ();
      var characters = string.Concat (File.ReadAllText ("C:/etc/words.txt")).ToList ();
      characters.Distinct ().Where (c => c >= 'A' && c <= 'Z').ToList ().ForEach (char1 => charFrequency.Add (char1, characters.FindAll (char2 => char2 == char1).Count));
      charFrequency = charFrequency.OrderByDescending (pair => pair.Value).ToDictionary (pair => pair.Key, pair => pair.Value);
      int padLength = charFrequency.Values.First ().ToString ().Length;
      foreach (var (key, value) in charFrequency.Take (7))
         Console.WriteLine ($"{key,2} {value.ToString ().PadLeft (padLength)}");
   }
}