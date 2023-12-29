using System.Reflection;
using static System.Console;
using static System.ConsoleColor;
using static System.ConsoleKey;

var game = new Wordle ();
game.Start ();

#region class Wordle ------------------------------------------------------------------------------
/// <summary>A public class runs the wordle game on the console window and allows the user to find the secret word.
/// Parameterless constructor initializes the game, and the "Start" method displays the game on the console.
/// User is allowed to type alphabets to form a 5-letter word and needs to submit the formed word by pressing "Enter."
/// User can press backspace or the left arrow to remove the recently typed letter.
/// Any key other than alphabets, backspace, left arrow, and enter will be notified as an invalid key.
/// User will be given six tries to guess the word and need to type valid words.
/// Submitting invalid words will be notified.
/// User will be notified if he submits a word, even if it's valid but doesn't contain five letters.
/// If secret word is found, the number of tries will be printed, and then the game ends.
/// If secret word is not found after six tries, the word is revealed and the game ends.
/// </summary>
public class Wordle {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes the worlde game by generating a secret word</summary>
   // Reads the all valid words from the words.txt file stored in the assembly.
   // Generates the secret word by chosing a random word from the readed words list.
   public Wordle () {
      CursorVisible = false;
      (CursorLeft, CursorTop) = (mWidth, mHeight);
      OutputEncoding = System.Text.Encoding.UTF8;
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("A12.Data.words.txt");
      using var reader = stream != null ? new StreamReader (stream) : null;
      while (reader != null && !reader.EndOfStream) mWords.Add (reader?.ReadLine () ?? "");
      mSecretWord = mWords[new Random ().Next (0, mWords.Count)];
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Initializes the game by updating the display and a message</summary>
   // Processes each input from the user and validating them for updating the game.
   // Ends the game if user fails to find the word.
   public void Start () {
      ShowGame ();
      PrintMessage ("Game started! Type the word!");
      while (!mGameOver) ProcessInput (ReadKey (true).Key);
      if (mFoundWord) PrintMessage ($"You found word in {mPtr / 5} tries");
      else PrintMessage ($"Sorry - the word was {mSecretWord}");
      CursorTop += 2;
   }
   #endregion

   #region Implementation -------------------------------------------
   //Prints the given character at its specified location with given color
   void PrintCharacter (Character ch, ConsoleColor clr = White) {
      (CursorLeft, CursorTop) = (ch.X, ch.Y);
      ForegroundColor = clr;
      Write (ch.Ch);
      ResetColor ();
   }

   //Prints the line using series of underscore character
   void PrintLine () {
      CursorLeft = mWidth - 10;
      ForegroundColor = DarkGray;
      for (int i = 0; i < 36; i++) Write ("_");
      ResetColor ();
   }

   //Prints the given message in game display and clears them if message is empty
   void PrintMessage (string msg, ConsoleColor clr = Yellow) {
      ForegroundColor = clr;
      CursorLeft = mWidth - mCols;
      CursorTop = mHeight + (4 * mRows);
      if (msg.Length is 0) {
         for (int i = 0; i <= 100; i++) Write (" ");
         return;
      }
      Write (msg);
      ResetColor ();
   }

   //User typed key is processed and then game display is updated.
   //Prompts user to press "Enter" button to submit the word.
   //Prompts user to type 5 letter word if user presses "Enter" and word is not of 5 letters.
   //Prompts user to enter valid input if key is not any one of alphabets or back space or left arrow.
   void ProcessInput (ConsoleKey key) {
      if (mResponse.Count == 5 && key is not Enter and not Backspace and not LeftArrow) {
         PrintMessage ("Press \'Enter\' to check the word!");
         return;
      } else if (mResponse.Count < mCols && key is Enter) {
         PrintMessage ("Word shoud contain 5 letters!");
         return;
      }
      PrintMessage ("");
      Action todo = key switch {
         Enter => () => {
            var str = new string (mResponse.ToArray ());
            if (mWords.Contains (str)) {
               UpdateDisplay ();
               mResponse.Clear ();
               if (str == mSecretWord) { mGameOver = mFoundWord = true; return; }
               if (mPtr == mCount) mGameOver = true;
               SetChar (mCircle);
            } else PrintMessage ($"{str} is not a valid word!", Yellow);
         },
         Backspace or LeftArrow => () => {
            if (mResponse.Count > 0 && mPtr <= mCount) {
               mOptions[mPtr - 1].Ch = mCircle;
               SetChar (mDot);
               PrintCharacter (mOptions[mPtr - 1]);
               mResponse.RemoveAt (mResponse.Count - 1);
               mPtr--;
            }
         },
         <= Z and >= A => () => {
            if (mPtr < mCount) {
               var ch = (char)key;
               mOptions[mPtr].Ch = ch;
               mResponse.Add (ch);
               PrintCharacter (mOptions[mPtr]);
               mPtr++;
               if (mResponse.Count != 5 && mPtr < mCount) {
                  mOptions[mPtr].Ch = mCircle;
                  PrintCharacter (mOptions[mPtr]);
               }
            }
         },
         _ => () => PrintMessage ("Please enter a valid input!")
      };
      todo ();
      ResetColor ();
   }

   void SetChar (char ch) {
      if (mPtr < mCount) {
         mOptions[mPtr].Ch = ch;
         PrintCharacter (mOptions[mPtr]);
      }
   }

   //Initializes the game view and shows all available options to the use
   //This method stores the pixel locations of the options and letters to update them when user submit the guessings.
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
         for (int j = 0; tmp < 91 && j < 7; j++) {
            var ch = (char)tmp++;
            mLetters[ptr++] = new Character () { Ch = ch, X = CursorLeft, Y = CursorTop };
            Write (ch);
            CursorLeft += 5;
         }
         CursorLeft = mWidth - 10;
         CursorTop += 2;
      }
      PrintLine ();
      PrintMessage ("");
   }

   //Updates user given letters to the respective colors based on their indexes in the secret word
   //If user tried all 6 guessings, this method ends the game by setting mGameOver to true.
   void UpdateDisplay () {
      for (int i = 0, j = 5; i < j; i++) {
         var (ch, clr) = (mResponse[i], DarkGray);
         if (ch == mSecretWord[i]) clr = Green;
         else if (mSecretWord.Contains (ch)) clr = Blue;
         PrintCharacter (mOptions[mPtr - (j - i)], clr);
         var tmp = ch - 65;
         if (!mLetters[tmp].Colored) {
            mLetters[tmp].Colored = true;
            PrintCharacter (mLetters[tmp], clr);
         }
      }
      ResetColor ();
   }
   #endregion

   #region Private data ---------------------------------------------
   bool mFoundWord, mGameOver;
   char mCircle = '\u25cc', mDot = '\u00b7';
   string mSecretWord;
   int mRows = 6, mCols = 5, mCount = 30, mPtr, mWidth = WindowWidth / 2, mHeight = WindowHeight / 10;
   List<char> mResponse = new (); // user typed letters.
   List<string> mWords = new (); // words readed from words.txt
   Character[] mOptions = new Character[30]; // mCircle and mDot to form 6 rows and five coloumns.
   Character[] mLetters = new Character[26]; // Alphabets to display on window.
   #endregion
}
#endregion

#region struct Character --------------------------------------------------------------------------
/// <summary>A struct to store a character and its location o the console window.</summary>
public record struct Character (char Ch, int X, int Y, bool Colored) { }
#endregion