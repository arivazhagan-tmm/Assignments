using static System.Console;
using static Order;

while (true) {
   GetString ("Enter the letters: ", out var letters);
   GetChar ("Enter the reference letter: ", out var refChar);
   WriteLine ();
   GetChar ("Type \'A\' for ascending order or \'D\' for descending order: ", out var ch, true);
   var chars = letters.ToCharArray ();
   var order = ch is 'd' or 'D' ? Descending : Ascending;
   Sort (ref chars, refChar, order);
   Write ($"\nLetters:{letters}\nSorted:");
   foreach (char c in chars) Write (c);
   Write ("\n\nDo you want to continue? (y/n): ");
   if (ReadKey ().Key is not ConsoleKey.Y) break;
   WriteLine ("\n");
}

// Getting the valid character from the user for the given prompt.
static void GetChar (string prompt, out char ch, bool askingOrder = false) {
   var msg = askingOrder ? "either A or D" : "an alphabet";
   Write (prompt);
   while (true) {
      ch = (char)ReadKey ().Key;
      bool isValid = askingOrder ? ch is 'A' or 'a' or 'd' or 'D' : char.IsLetter (ch);
      if (isValid) break;
      ForegroundColor = ConsoleColor.Red;
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
static void Sort (ref char[] chars, char refChar, Order order = Ascending) {
   var (i, len, list) = (0, chars.Length, chars.ToList ());
   var (ptr, increment) = order == Ascending ? (0, 1) : (len - 1, -1);
   while (ptr >= 0 && ptr < len) {
      for (int j = 0; j < list.Count; j++) {
         var tmp = list[j];
         if (Math.Abs (refChar - tmp) == i) {
            list.RemoveAt (j--);
            chars[ptr] = tmp;
            ptr += increment;
         }
      }
      i++;
   }
}

public enum Order { Ascending, Descending }
