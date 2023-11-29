using static System.Console;
using static Order;

while (true) {
   GetString ("Enter the letters: ", out var letters);
   GetChar ("Enter the special character: ", out var splChar);
   WriteLine ();
   GetChar ("Type \'A\' for ascending order or \'D\' for descending order: ", out var ch, true);
   var chars = letters.ToCharArray ();
   var order = ch is 'D' ? Descending : Ascending;
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
      ch = char.ToUpper ((char)ReadKey ().Key);
      bool isValid = askingOrder ? ch is 'A' or 'D' : char.IsLetter (ch);
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
   splChar = char.ToLower (splChar);
   char[] a = chars.Where (c => c != splChar).ToArray ();
   var sortedArr = order == Ascending ? a.Order () : a.OrderDescending ();
   return sortedArr.Concat (chars.Where (c => c == splChar)).ToArray ();
}

public enum Order { Ascending, Descending }
