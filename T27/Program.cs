using T27;
using static System.Console;
using static System.ConsoleColor;

var stack = new TStack<int> ();
var r = new Random ();
for (int i = 1; i <= 100; i++) {
   var tmp = r.NextDouble ();
   if (tmp <= 0.33) {
      stack.Push (i);
      ForegroundColor = Green;
      WriteLine ($"Pushed: {i}");
   } else if (tmp is <= 0.66 and >= 0.33 && !stack.IsEmpty) {
      ForegroundColor = Red;
      WriteLine ($"Popped: {stack.Pop ()}");
   } else if (!stack.IsEmpty) {
      ForegroundColor = Yellow;
      WriteLine ($"Peeked: {stack.Peek ()}");
   }
}
ResetColor ();
WriteLine ($"\nStack length is {stack.Length}");