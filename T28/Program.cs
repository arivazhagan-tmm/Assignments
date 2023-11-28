using T28;
using static System.Console;

TQueue<int> q = new ();
var r = new Random ();
var (len, ptr) = (400, 0);
var numbers = new int[len];
while (ptr < len) {
   var tmp = r.Next (1, len + 1);
   if (!numbers.Contains (tmp)) numbers[ptr++] = tmp;
}
Action action;
for (int i = 0; i < 300; i++) {
   var tmp = numbers[i];
   action = tmp switch {
      <= 100 => () => { if (!q.IsEmpty) Write ($"Dequeued: {q.Dequeue ()}\n"); }
      ,
      <= 200 and > 100 => () => { if (!q.IsEmpty) Write ($"Peeked: {q.Peek ()}\n"); }
      ,
      <= 300 and > 280 => q.Clear,
      _ => () => { q.Enqueue (tmp); Write ($"Enqueued: {tmp}\n"); }
   };
   action ();
}