using A._10.P02;
internal class Program {
   private static void Main (string[] args) {
      TDeque<int> q = new ();
      Random r = new ();
      for (int i = 1; i <= 100; i++) {
         var tmp = r.NextDouble ();
         if (tmp <= 0.25) {
            q.HeadEnqueue (i);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ($"HE : {i}");
         } else if (tmp is <= 0.5 and >= 0.25) {
            q.TailEnqueue (i);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ($"TE : {i}");
         } else if (tmp is <= 0.75 and >= 0.5 && !q.IsEmpty) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ($"HD : {q.HeadDequeue ()}");
         } else if (!q.IsEmpty) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ($"TD : {q.TailDequeue ()}");
         }
         Console.ResetColor ();
         if (q.IsEmpty)
            Console.WriteLine ("Queue Empty!");
      }
   }
}