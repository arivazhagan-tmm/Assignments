using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

internal class Program {
   private static void Main (string[] args) {
      var randomLetters = new[] { 'U', 'X', 'A', 'T', 'L', 'N', 'E' };
      var specialLetter = randomLetters[0];
      var words = File.ReadAllLines ("C:/etc/words.txt");
      var validWords = words.Where (word => word.Length >= 4 && word.Contains (specialLetter) && word.All (randomLetters.Contains));
      var wordScorePair = new List<(string word, int score)> ();
      foreach (var word in validWords)
         wordScorePair.Add ((word, GetScore (word, randomLetters)));
      wordScorePair = wordScorePair.OrderByDescending (pair => pair.score).ThenBy (pair => pair.word).ToList ();
      int totalScoreLength = 0, totalScore = 0;
      wordScorePair.ForEach (pair => totalScore += pair.score);
      totalScoreLength = totalScore.ToString ().Length;
      var separator = "";
      for (int i = 0; i < totalScoreLength; i++)
         separator += "-";
      foreach (var (word, score) in wordScorePair) {
         Console.ForegroundColor = score > word.Length ? ConsoleColor.Green : Console.ForegroundColor;
         Console.WriteLine ($"{score.ToString ().PadLeft (totalScoreLength)}. {word}");
         Console.ResetColor ();
      }
      Console.WriteLine ($"{separator}\n{totalScore.ToString ().PadLeft (totalScoreLength)} total");
   }

   /// <summary> Returns unique score based on word's length</summary>
   static int GetScore (string word, char[] letters) {
      int nLength = word.Length, score = nLength > 4 ? nLength : 1;
      if (letters.All (word.Contains)) score += 7;
      return score;
   }
}