using System.Reflection;
using static System.Console;
using static System.ConsoleColor;
using static System.ConsoleKey;

#region Internal Class Program----------------------------------------------------------------------
/// <summary> An Internal class runs the wordle game by creating its object in main method. </summary>
internal class Program {
   static void Main () => new Wordle ().Start ();
}
#endregion

#region Public Class Wordle-------------------------------------------------------------------------
/// <summary>A public class runs the wordle game on the console window and allows the user to find the secret word.
/// A parameterless constructor initializes the game, and the "Start" method displays the game on the console.
/// The user is allowed to type alphabets to form a 5-letter word and needs to submit the formed word by pressing "Enter."
/// The user can press backspace or the left arrow to remove the recently typed letter.
/// Any key other than alphabets, backspace, left arrow, and enter will be notified as an invalid key.
/// The user will be given six tries to guess the word and will need to type valid words.
/// Submitting invalid words will be notified.
/// The user will be notified if he submits a word, even if it's valid but doesn't contain five letters.
/// When the user finds the secret word, the number of tries will be printed, and then the game ends.
/// If the user can't find the secret word after six tries, the secret word will be printed, and the game ends.
/// </summary>
public class Wordle {
   #region Constructor------------------------------------------------
   /// <summary>Initializes the worlde game by generating a secret word and setting the cursor properties.</summary>
   // Reads the all valid words from the words.txt file stored in the assembly.
   // Generates the secret word by chosing a random word from the readed words list.
   public Wordle () {
      CursorVisible = false;
      CursorLeft = mWidth;
      CursorTop = mHeight;
      OutputEncoding = System.Text.Encoding.UTF8;
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("A12.Data.words.txt");
      using var reader = stream != null ? new StreamReader (stream) : null;
      var words = new List<string> ();
      while (reader != null && !reader.EndOfStream) mWords.Add (reader?.ReadLine () ?? "");
      mSecretWord = mWords[new Random ().Next (1, mWords.Count)];
   }
   #endregion

   #region Public Methods---------------------------------------------
   /// <summary> Initializes the game by updating the display and a message.</summary>
   // Processes each input from the user and validating them for updating the game.
   // Ends the game if user fails to find the word.
   public void Start () {
      ShowGame ();
      PrintMessage ("Game started! Type the word!");
      while (!mGameOver) ProcessInput (ReadKey (true).Key);
      if (!mFoundWord) PrintMessage ($"Sorry - the word was {mSecretWord}");
   }
   #endregion

   #region Private Methods--------------------------------------------
   /// <summary> Prints the line using underscore characters.</summary>
   void PrintLine () {
      CursorLeft = mWidth - 10;
      ForegroundColor = DarkGray;
      for (int i = 0; i < 36; i++) Write ("_");
      ResetColor ();
   }

   /// <summary> Prints the given message in game display and clears them if message is empty. </summary>
   void PrintMessage (string msg, ConsoleColor clr = Yellow) {
      ForegroundColor = clr;
      CursorLeft = mWidth - mCols; CursorTop = mHeight + (4 * mRows);
      if (msg.Length is 0) {
         for (int i = 0; i <= 100; i++) Write (" ");
         return;
      }
      Write (msg);
      ResetColor ();
   }

   /// <summary> Prints the given character at its specified location with given color. </summary>
   void PrintCharacter (Character ch, ConsoleColor clr = White) {
      (CursorLeft, CursorTop) = (ch.X, ch.Y);
      ForegroundColor = clr;
      Write (ch.Ch);
      ResetColor ();
   }

   /// <summary> User typed key is processed and then game display is updated.</summary>
   // Prompts user to press "Enter" button to submit the word.
   // Prompts user to type 5 letter word if user presses "Enter" and word is not of 5 letters.
   // Prompts user to enter valid input if key is not any one of alphabets or back space or left arrow.
   void ProcessInput (ConsoleKey key) {
      if (mResponse.Count == mCols && key is not Enter and not Backspace and not LeftArrow) {
         PrintMessage ("Press \'Enter\' to check the word!");
         return;
      } else if (mResponse.Count < mCols && key is Enter) {
         PrintMessage ("Word shoud contain 5 letters!");
         return;
      }
      PrintMessage ("");
      Action todo = key switch {
         Enter => () => {
            CheckWord ();
            if (mIsValidWord) UpdateDisplay ();
         }
         ,
         Backspace or LeftArrow => () => {
            if (mResponse.Count > 0 && mPtr <= mCount) {
               mOptions[mPtr - 1].Ch = mCircle;
               PrintCharacter (mOptions[mPtr - 1]);
               if (mPtr < mCount) {
                  mOptions[mPtr].Ch = mDot;
                  PrintCharacter (mOptions[mPtr]);
               }
               mResponse.RemoveAt (mResponse.Count - 1);
               mPtr--;
            }
         }
         ,
         <= Z and >= A => () => {
            if (mPtr < mCount) {
               var ch = (char)key;
               mOptions[mPtr].Ch = ch;
               mResponse.Add (ch);
               PrintCharacter (mOptions[mPtr]);
               mPtr++;
               if (mPtr < mCount) {
                  mOptions[mPtr].Ch = mCircle;
                  PrintCharacter (mOptions[mPtr]);
               }
            }
         }
         ,
         _ => () => PrintMessage ("Please enter valid input!")
      };
      todo ();
      ResetColor ();
   }

   /// <summary> Initializes the game view and shows all available options to the user.</summary>
   // This method stores the pixel locations of the options and letters to update them when user submit the guessings.
   void ShowGame () {
      var (ptr, tmp) = (0, 0);
      /*Printing options to enter the letters.*/
      for (int i = 0; i < mRows; i++) {
         CursorLeft = mWidth;
         for (int j = 0; j < mCols; j++) {
            var ch = mDot;
            if (i is 0 && j is 0) ch = mCircle;
            mOptions[tmp++] = new Character () { Ch = ch, X = CursorLeft, Y = CursorTop };
            Write (ch);
            CursorLeft += 3;
         }
         CursorTop += 2;
      }
      PrintLine ();
      CursorLeft = mWidth - 10;
      CursorTop += 2;
      tmp = 65;
      /*Printing Alphabets.*/
      for (int i = 0; i < 4; i++) {
         for (int j = 0; tmp is < 91 && j < 7; j++) {
            var ch = (char)tmp++;
            mLetters[ptr++] = new Character () { Ch = ch, X = CursorLeft, Y = CursorTop };
            Write (ch);
            CursorLeft += 5;
         }
         CursorLeft = mWidth - 10;
         CursorTop += 2;
      }
      PrintLine ();
   }

   /// <summary> Updates user given letters to the respective colors based on their indexes in the secret word.</summary>
   // If user tried all 6 guessings, this method ends the game by setting mGameOver to true.
   void UpdateDisplay () {
      for (int i = mPtr - mCols; i < mPtr; i++) {
         var (a, b) = (mResponse[i % mCols], mSecretWord[i % mCols]);
         var clr = DarkGray;
         if (a == b) clr = Green;
         else if (mSecretWord.Contains (a)) clr = Blue;
         PrintCharacter (mOptions[i], clr);
         PrintCharacter (mLetters[a - 65], clr);
      }
      mResponse.Clear ();
      mGameOver = mPtr == mCount;
      ResetColor ();
   }

   /// <summary> Checks the submitted word whether it's invalid or user found the secret word.</summary>
   void CheckWord () {
      var str = new string (mResponse.ToArray ());
      mIsValidWord = mWords.Contains (str);
      if (!mIsValidWord) PrintMessage ($"{str} is not a valid word!", Yellow);
      else if (str == mSecretWord) {
         PrintMessage ($"You found word in {mPtr / mCols} tries");
         mGameOver = mFoundWord = true;
      }
   }
   #endregion

   #region Private Fields---------------------------------------------
   bool mFoundWord, mGameOver, mIsValidWord;
   char mCircle = '\u25cc', mDot = '\u00b7';
   string mSecretWord = "";
   int mRows = 6, mCols = 5, mCount = 30, mPtr, mWidth = WindowWidth / 2, mHeight = WindowHeight / 2;
   List<char> mResponse = new (); // user typed letters.
   List<string> mWords = new (); // words readed from words.txt
   Character[] mOptions = new Character[30]; // mCircle and mDot to form 6 rows and five coloumns.
   Character[] mLetters = new Character[26]; // Alphabets to display on window.
   #endregion
}
#endregion

#region Public Struct Character---------------------------------------------------------------------
/// <summary>A struct to store a character and its location o the console window.</summary>
public record struct Character (char Ch, int X, int Y) { }
#endregion