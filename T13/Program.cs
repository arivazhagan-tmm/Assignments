using static System.Console;
using static State;

internal class Program {
   // Validates the password created by user based on certain conditions.
   static void Main () {
      var stateMessage = new Dictionary<State, string> {
         {A, "Password should contain minimum 6 characters."},
         {B, "Password should contain atleast one digit between 0 and 9"},
         {C, "Password should contain atleast one english alphabet"},
         {D, "Password should contain atleast one of the english alphabet in upper and lower case"},
         {E, "Password should contain atleast any one of the given characters ! @ # $ % ^ & * (  ) _ - +"},
         {F, "Password is strong."}
      };
      string prompt = "Type the password: ";
      State state;
      ConsoleKeyInfo info;
      while (true) {
         Write (prompt);
         string password = "";
         info = ReadKey (true);
         while (info.Key != ConsoleKey.Enter) {
            Write ("*");
            password += info.KeyChar;
            info = ReadKey (true);
         }
         WriteLine ();
         state = Validate (password);
         WriteLine (stateMessage[state]);
         if (state is not F) prompt = "\nRe - enter the password: ";
         else break;
      }
   }

   // Validates the string by it's characters and returns its state.
   static State Validate (string str) {
      var len = str.Length;
      bool hasDigit = false, hasLetter = false, hasLowerCase = false, hasUpperCase = false, hasSymbol = false;
      if (len < 6) return A;
      for (int i = 0; i < len; i++) {
         char ch = str[i];
         hasDigit = !hasDigit ? char.IsDigit (ch) : hasDigit;
         hasLetter = !hasLetter ? char.IsLetter (ch) : hasLetter;
         hasLowerCase = !hasLowerCase ? char.IsLower (ch) : hasLowerCase;
         hasUpperCase = !hasUpperCase ? char.IsUpper (ch) : hasUpperCase;
         hasSymbol = !hasSymbol ? mSymbols.Contains (ch) : hasSymbol;
      }
      if (!hasDigit) return B;
      if (!hasLetter) return C;
      if (!hasLowerCase || !hasUpperCase) return D;
      if (!hasSymbol) return E;
      return F;
   }

   static string mSymbols = "!@#$%^&*()_-+";
}

public enum State { A, B, C, D, E, F }