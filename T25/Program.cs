using static System.Console;
using static Order;

while (true) {
   GetString ("Enter the letters: ", out var letters);
   GetChar ("Enter the reference letter: ", out var refChar);
   GetChar ("Type \'A\' for ascending order or \'D\' for descending order: ", out var ch, true);
   var chars = letters.ToCharArray ();
   var order = ch is 'd' or 'D' ? Descending : Ascending;
   Sort (ref chars, refChar, order);
   Write ($"\n\tLetters:{letters}\n\tSorted:");
   foreach (char c in chars) Write (c);
   Write ("\n\nDo you want to continue? (y/n): ");
   if (ReadKey ().Key is not ConsoleKey.Y) break;
   WriteLine ("\n");
}

// Getting the valid character from the user for the given prompt.
static void GetChar (string prompt, out char ch, bool askingOrder = false) {
   bool isValid;
   var msg = askingOrder ? "either A or D" : "an alphabet";
   do {
      Write (prompt);
      ch = (char)ReadKey ().Key;
      isValid = askingOrder ? ch is 'A' or 'a' or 'd' or 'D' : char.IsLetter (ch);
      if (!isValid) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ($"\tInput should be {msg}.");
         ResetColor ();
      }
      WriteLine ();
   } while (!isValid);
}

// Getting the valid string from the user for the given prompt.
static void GetString (string prompt, out string response) {
   bool isValid;
   do {
      Write (prompt);
      response = ReadLine () ?? "";
      isValid = response.All (char.IsLetter);
      if (!isValid) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ("\tInput should contain only alphabets.");
         ResetColor ();
      }
   } while (!isValid);
}

// Sorts the given character array for the given order.
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
