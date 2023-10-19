﻿using System.Reflection;
using static System.Console;

class Program {

   // Reads the strings from a text file and prints the abecedarian strings.
   static void Main () {
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("T16.Data.words.txt");
      using var reader = stream != null ? new StreamReader (stream) : null;
      if (reader != null) {
         var words = new List<string> ();
         while (!reader.EndOfStream)
            words.Add (reader?.ReadLine () ?? "");
         var result = GetLongestAbecedarian (words.ToArray (), out string[] abecedarians);
         WriteLine ("Following are the abecedarian words and their length : ");
         int count = 1;
         foreach (var word in abecedarians) WriteLine ($"{count++}.\t{word}\t: {word.Length}");
         WriteLine ();
         WriteLine ($"Longest abecedarian word: {result}");
      }
   }

   // Checks if the given string is abecedarian or not. In abecedarian string, letters are arranged in alphabetical order.
   static bool IsAbecedarian (string str) {
      for (int i = 0, len = str.Length; i < len - 1; i++)
         if (str[i] > str[i + 1]) return false;
      return true;
   }

   // Returns a longest abecedarian word from the given words.
   static string GetLongestAbecedarian (string[] words, out string[] abecedarians) {
      var result = "";
      var tmp = new List<string> ();
      foreach (var word in words) {
         if (IsAbecedarian (word)) {
            result = word?.Length > result.Length ? word : result;
            tmp.Add (word ?? "");
         }
      }
      abecedarians = tmp.ToArray ();
      return result;
   }
}
