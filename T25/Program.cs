using static System.Console;
using static Order;

while (true) {
   GetString ("Enter the letters: ", out var letters);
   GetChar ("Enter the special character: ", out var splChar);
   WriteLine ();
   GetChar ("Type \'A\' for ascending order or \'D\' for descending order: ", out var ch, true);
   var chars = letters.ToCharArray ();
   var order = char.ToUpper (ch) is 'D' ? Descending : Ascending;
   chars = Sort (chars, splChar, order);
   Write ($"\nLetters:{letters}\nSorted:");
   foreach (char c in chars) Write (c);
   Write ("\n\nDo you want to continue? (y/n): ");
   if (ReadKey ().Key is not ConsoleKey.Y) break;
   WriteLine ("\n");
}

// Getting the valid character from the user for the given prompt.
static void GetChar (string prompt, out char ch, bool askingOrder = false) {
   Write (prompt);
   var (msg, msgUpdated) = ("", false);
   while (true) {
      ch = (char)ReadKey ().Key;
      bool isValid = askingOrder ? ch is 'A' or 'a' or 'd' or 'D' : char.IsLetter (ch);
      if (isValid) break;
      ForegroundColor = ConsoleColor.Red;
      if (!msgUpdated) msg = askingOrder ? "either A or D" : "an alphabet";
      WriteLine ($"\nInput should be {msg}.");
      ResetColor ();
      Write (prompt);
   }
}

// Getting the valid string from the user for the given prompt.
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

// Sorts the given character array in the given order.
static char[] Sort (char[] chars, char splChar, Order order = Ascending) {
   splChar = char.ToLower (splChar);
   var (len, list) = (chars.Length, chars.ToList ());
   list.RemoveAll (x => x == splChar);
   var splCharCount = len - list.Count;
   var (ptr, increment) = order == Ascending ? (0, 1) : (len - (splCharCount + 1), -1);
   var res = new char[len];
   while (list.Count > 0) {
      res[ptr] = list.Min ();
      list.Remove (res[ptr]);
      ptr += increment;
      if (splCharCount > 0) res[len - splCharCount--] = splChar;
   }
   return res;
}

public enum Order { Ascending, Descending }
