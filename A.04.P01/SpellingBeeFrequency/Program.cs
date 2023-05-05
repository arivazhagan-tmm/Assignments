using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program {
   private static void Main (string[] args) {
      Dictionary<char, int> charFrequency = new ();
      var characters = string.Concat (File.ReadAllLines ("C:/etc/words.txt"));
      characters.Distinct ().ToList ().ForEach (char1 => charFrequency.Add (char1, characters.ToList ().FindAll (char2 => char2 == char1).Count));
      charFrequency = charFrequency.OrderByDescending (pair => pair.Value).ToDictionary (pair => pair.Key, pair => pair.Value);
      int padLength = charFrequency.Values.First ().ToString ().Length;
      foreach (var (key, value) in charFrequency)
         Console.WriteLine ($"{key} : {value.ToString ().PadLeft (padLength)}");
   }
}