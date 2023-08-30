using static System.Console;
using static State;
internal class Program {
   static void Main () {
      var stateMessage = new Dictionary<State, string> {
         {A,  " Password should contain minimum 6 characters."},
         {B,  " Password should contain atleast one digit between 0 and 9"},
         {C,  " Password should contain atleast one english alphabet"},
         {D,  " Password should contain atleast one of the english alphabet in upper and lower case"},
         {E,  " Password should contain atleast any one of the given characters ! @ # $ % ^ & * (  ) _ - +"},
         {F,  " Password is strong."}
      };
      string prompt = "Type the password\t: ";
      State state = F;
      while (true) {
         Write (prompt);
         var password = ReadLine ();
         if (!string.IsNullOrEmpty (password)) {
            state = Validate (password);
            WriteLine (stateMessage[state]);
         }
         if (state is not F) prompt = "\nRe - enter the password : ";
         else break;
      }
   }

   static State Validate (string str) {
      if (str.Length < 6) return A;
      if (!str.Any (char.IsDigit)) return B;
      if (!str.Any (char.IsLetter)) return C;
      if (!str.Any (char.IsLower) || (!str.Any (char.IsUpper))) return D;
      if (!str.Any (mSymbols.Contains)) return E;
      return F;
   }

   static string mSymbols = "!@#$%^&*()_-+";
}
public enum State { A, B, C, D, E, F }