using A._10.P02;
using static System.Console;
internal class Program {
   private static void Main (string[] args) {
      TDeque<int> q = new ();
      Random r = new ();
      for (int i = 1; i <= 100; i++) {
         var tmp = r.NextDouble ();
         ForegroundColor = ConsoleColor.Red;
         if (tmp <= 0.25) {
            q.HeadEnqueue (i);
            ForegroundColor = ConsoleColor.Green;
            WriteLine ($"Head Enqueue : {i}");
         } else if (tmp is <= 0.5 and >= 0.25) {
            q.TailEnqueue (i);
            ForegroundColor = ConsoleColor.Green;
            WriteLine ($"Tail Enqueue : {i}");
         } else if (tmp is <= 0.75 and >= 0.5 && !q.IsEmpty) {
            WriteLine ($"Head Dequeue : {q.HeadDequeue ()}");
            if (q.IsEmpty) {
               ResetColor ();
               WriteLine ("\n____Queue Empty!____\n");
            }
         } else if (!q.IsEmpty) {
            WriteLine ($"Tail Dequeue : {q.TailDequeue ()}");
            if (q.IsEmpty) {
               ResetColor ();
               WriteLine ("\n____Queue Empty!____\n");
            }
         }
         ResetColor ();
      }
      WriteLine ($"\nQueue size is {q.Length}");
   }
}