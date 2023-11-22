using T28;
using static System.Console;
using static System.ConsoleColor;

TQueue<int> q = new ();
int n = 0;
Random r = new ();
for (int i = 0; i < 100; i++) {
   if (r.NextDouble () < 0.5 && !q.IsEmpty) {
      ForegroundColor = Red;
      Write ($"Dequeued: {q.Dequeue ()}\n");
   } else {
      q.Enqueue (n);
      ForegroundColor = Green;
      Write ($"Enqueued: {n++}\n");
   }
   ResetColor ();
}