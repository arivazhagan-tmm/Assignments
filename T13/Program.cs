using static System.Console;

internal class Program {
   // Validates the password created by user based on certain conditions.
   static void Main () {
      string prompt = "Type the password: ", password;
      while (mState is not State.F) {
         password = "";
         Write (prompt);
         mInfo = ReadKey (true);
         while (mInfo.Key != ConsoleKey.Enter) {
            Write ("*");
            password += mInfo.KeyChar;
            mInfo = ReadKey (true);
         }
         WriteLine ();
         mState = GetState (password);
         WriteLine (mStateMsgPairs[mState]);
         prompt = "\nRe-Enter the password: ";
      }
   }

   // Returns the state of given string by validating its characters.
   static State GetState (string str) {
      var len = str.Length;
      string symbols = "!@#$%^&*()_-+";
      var (hasDigit, hasLetter, hasLowerCase, hasUpperCase, hasSymbol) = (false, false, false, false, false);
      if (len < 6) return State.A;
      for (int i = 0; i < len; i++) {
         char ch = str[i];
         if (!hasDigit) hasDigit = char.IsDigit (ch);
         if (!hasLetter) hasLetter = char.IsLetter (ch);
         if (!hasLowerCase) hasLowerCase = char.IsLower (ch);
         if (!hasUpperCase) hasUpperCase = char.IsUpper (ch);
         if (!hasSymbol) hasSymbol = symbols.Contains (ch);
      }
      if (!hasDigit) return State.B;
      if (!hasLetter) return State.C;
      if (!hasLowerCase || !hasUpperCase) return State.D;
      if (!hasSymbol) return State.E;
      return State.F;
   }

   static ConsoleKeyInfo mInfo;
   static State mState;
   static Dictionary<State, string> mStateMsgPairs = new () {
         {State.A, "Password should contain minimum 6 characters."},
         {State.B, "Password should contain atleast one digit between 0 and 9"},
         {State.C, "Password should contain atleast one english alphabet"},
         {State.D, "Password should contain atleast one of the english alphabet in upper and lower case"},
         {State.E, "Password should contain atleast any one of the given characters ! @ # $ % ^ & * (  ) _ - +"},
         {State.F, "Password is strong."}
      };
   enum State { A, B, C, D, E, F }
}

