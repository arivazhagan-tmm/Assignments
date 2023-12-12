using Game;
using System.Reflection;
using static System.ConsoleKey;
using static System.ConsoleColor;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace A14._2;

#region class WordleTest -----------------------------------------------------------------------
/// <summary>A test class to test all the functionality of the class "Wordle"</summary>
[TestClass]
public class WordleTest {
   #region Methods -----------------------------------------------
   /// <summary>Initiates the procedural testing of each functinalities</summary>
   [TestMethod]
   public void InitiateTest () {
      TestGameStart ();
      TestInvalidInputs ();
      TestPresentation ();
      TestValidInputs ();
      TestInputDeletion ();
      TestGameEnd ();
   }
   #endregion

   #region Implementation ----------------------------------------
   //Tests the end status of wordle game and the end message
   void TestGameEnd () {
      mWordle.SecretWord.ToList ().ForEach (ch => mWordle.ProcessInput ((ConsoleKey)ch));
      mWordle.ProcessInput (Enter);
      IsTrue (mWordle.GameOver && mWordle.FoundWord);
      IsTrue (mWordle.GuessWord.Equals (mWordle.SecretWord));
      mWordle.EndGame ();
      TestMessage ($"You found word in {mWordle.Tries} tries");
      UpdateTextFile ();
      sw.WriteLine ($"If user found the secret word, the message is: {mWordle.Message}".PadLeft (100));
      sw.Dispose ();
   }

   //Tests the start of game with and the welcome message
   void TestGameStart () {
      mWordle.ShowGame ();
      TestMessage ("Game started! Type the word!");
   }

   //Tests the state of game with the respective messsage to user, if user submits an invalid word
   //Tests the state of game on console if user clears the input using backspace and leftarrow.
   void TestInputDeletion () {
      var invalid = new List<char> { 'A', 'E', 'I', 'O', 'U' };
      invalid.ForEach (ch => mWordle.ProcessInput ((ConsoleKey)ch));
      mWordle.ProcessInput (Enter);
      TestMessage ($"{mWordle.GuessWord} is not a valid word!");
      UpdateTextFile ();
      sw.WriteLine ($"If user submits an invalid word, warning message is: {mWordle.Message}".PadLeft (100));
      IsTrue (mWordle.InvalidWord);
      for (int i = 4; i > 0;) {
         mWordle.ProcessInput (Backspace);
         TestMessage (string.Empty);
         UpdateTextFile ();
         sw.WriteLine ("User pressed back space".PadLeft (80));
         AreEqual (--i, mWordle.Column);
         mWordle.ProcessInput (LeftArrow);
         UpdateTextFile ();
         sw.WriteLine ("User pressed left arrow".PadLeft (80));
         AreEqual (--i, mWordle.Column);
      }
      mWordle.ProcessInput (Backspace);
      UpdateTextFile ();
      sw.WriteLine ("User pressed back space".PadLeft (80));
   }

   //Tests all the invalid inputs given by the user and their respective message
   void TestInvalidInputs () {
      var keys = Enum.GetValues<ConsoleKey> ();
      keys.Where (IsInvalid).ToList ().ForEach (k => { mWordle.ProcessInput (k); AreEqual (mWordle.Message, "Please enter valid input!"); });
      UpdateTextFile ();
      sw.WriteLine ($"If user presses any key other than alphabets, enter, backspace, leftarrow the warning message is: {mWordle.Message}".PadLeft (100));
      bool IsInvalid (ConsoleKey key) => key is not Enter and not Backspace and not LeftArrow and not >= A and <= Z;
   }

   //Tests the given message with the message displayed by the game using assertion equality
   void TestMessage (string msg) => AreEqual (mWordle.Message, msg);

   //Tests the representation of game on the console window and replicates the same on text file
   void TestPresentation () {
      IsNotNull (mWordle);
      AreEqual (mWordle.Options[0].Ch, mWordle.Circle);
      IsTrue (mWordle.Options[1..].All (opt => opt.Ch == mWordle.Dot));
      IsTrue (mWordle.Options.All (opt => opt.Color == White));
      IsTrue (mWordle.Letters.All (opt => opt.Color == White));
   }

   //Tests all the valid inputs given by the user and tests the corresponding messages shown to the user
   void TestValidInputs () {
      int row = 0;
      var r = new Random ();
      foreach (var ch in mWordle.SecretWord) {
         var validWords = mWordle.Words.Where (w => w != mWordle.SecretWord && w[row] == ch).ToArray ();
         var guess = validWords[r.Next (0, validWords.Length)];
         for (int i = 0; i < 5; i++) {
            mWordle.ProcessInput ((ConsoleKey)guess[i]);
            UpdateTextFile ();
            TestMessage (string.Empty);
            if (i is 4) {
               mWordle.ProcessInput (A);
               TestMessage ("Press \'Enter\' to check the word!");
               UpdateTextFile ();
               sw.WriteLine ($"If user presses one more key, warning message is: {mWordle.Message}".PadLeft (100));
            }
            mWordle.ProcessInput (Enter);
            AreEqual (mWordle.Row, row);
            AreEqual (mWordle.Column, i);
            if (i is 3) {
               UpdateTextFile ();
               sw.WriteLine ($"If user presses 'Enter' key, warning message is: {mWordle.Message}".PadLeft (100));
            }
            if (i < 4) TestMessage ("Word should contain 5 letters!");
         }
         var currentInput = mWordle.Options[(row * 5)..(++row * 5)];
         foreach (var ip in currentInput) {
            var tmp = mWordle.Letters[ip.Ch - 65].Color;
            if (tmp is not Green) AreEqual (ip.Color, tmp);
         }
         AreEqual (mWordle.Tries, row);
         AreEqual (mWordle.Options[row * 5].Ch, mWordle.Circle);
         TestMessage (string.Empty);
      }
   }

   //Updates the text file with the current status of game
   void UpdateTextFile () {
      WriteToTextFile (mWordle.Options);
      WriteToTextFile (mWordle.Letters);
   }

   //Writes given characters to the text file with their current state in the game
   void WriteToTextFile (Character[] chars) {
      sw.WriteLine ();
      sw.WriteLine ();
      for (int i = 0, x = 0, len = chars.Length; i < len - 1;) {
         var (opt1, opt2) = (chars[i], chars[i + 1]);
         var (dy, padLeft) = (opt2.Y - opt1.Y, opt1.X - x);
         sw.Write (Decorate (opt1).PadLeft (padLeft));
         if (dy > 0) {
            for (int j = 0; j < dy; j++) sw.WriteLine ();
            x = 0;
         } else x = opt1.X;
         if (++i == len - 1) sw.Write (Decorate (opt2).PadLeft (padLeft));
      }
      sw.WriteLine ();
      sw.WriteLine ();
      //Decorates and returns the given character
      string Decorate (Character ch) {
         return ch.Color switch {
            Blue => $"[{ch.Ch}]",
            Green => $"{{{ch.Ch}}}",
            DarkGray => $"({ch.Ch})",
            _ => $"{ch.Ch}"
         };
      }
   }
   #endregion

   #region Private data ------------------------------------------
   readonly Wordle mWordle = new ();
   readonly StreamWriter sw = new ("../../../Data/wordle.txt");
   #endregion
}
#endregion