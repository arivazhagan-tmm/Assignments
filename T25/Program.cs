using static System.Console;
using static Order;

while (true) {
   GetString ("Enter the letters: ", out var letters);
   GetChar ("Enter the special character: ", out var splChar);
   WriteLine ();
   GetChar ("Type \'A\' for ascending order or \'D\' for descending order: ", out var ch, true);
   var chars = letters.ToCharArray ();
   var order = ch is 'd' ? Descending : Ascending;
   chars = Sort (chars, splChar, order);
   Write ($"\nLetters:{letters}\nSorted:");
   foreach (char c in chars) Write (c);
   Write ("\n\nDo you want to continue? (y/n): ");
   if (ReadKey ().Key is not ConsoleKey.Y) break;
   WriteLine ("\n");
}

//Getting the valid character from the user for the given prompt.
static void GetChar (string prompt, out char ch, bool askingOrder = false) {
   Write (prompt);
   while (true) {
      ch = ReadKey ().KeyChar;
      ch = char.ToLower (ch);
      bool isValid = askingOrder ? ch is 'a' or 'd' : char.IsLetter (ch);
      if (isValid) break;
      ForegroundColor = ConsoleColor.Red;
      WriteLine ($"\nInput should be {(askingOrder ? "either A or D" : "an alphabet")}.");
      ResetColor ();
      Write (prompt);
   }
}

//Getting the valid string from the user for the given prompt.
static void GetString (string prompt, out string response) {
   Write (prompt);
   while (true) {
      response = ReadLine () ?? "";
      if (response.All (char.IsLetter)) break;
      ForegroundColor = ConsoleColor.Red;
      WriteLine ("Input should contain only alphabets.\n");
      ResetColor ();
      Write (prompt);
   }
}

//Sorts and returns the given character array in the given order.
static char[] Sort (char[] chars, char splChar, Order order = Ascending) {
   var upperChar = char.ToUpper (splChar);
   var a = chars.Where (c => c != splChar && c != upperChar);
   var comparer = new CharComparer ();
   var sortedArr = order == Ascending ? a.Order (comparer) : a.OrderDescending (comparer);
   return sortedArr.Concat (chars.Where (c => c == splChar || c == upperChar)).ToArray ();
}

public enum Order { Ascending, Descending }

public class CharComparer : IComparer<char> {
   public int Compare (char x, char y) {
      var (ch1, ch2) = (char.ToLower (x), char.ToLower (y));
      if (ch1 == ch2) return 0;
      if (ch1 > ch2) return 1;
      return -1;
   }
}