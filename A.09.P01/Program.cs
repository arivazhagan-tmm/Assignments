using A_09_P01;
internal class Program {
   private static void Main (string[] args) {
      TQueue<int> q = new ();
      int n = 0;
      Random r = new ();
      for (int i = 0; i < 100; i++) {
         if (r.NextDouble () < 0.5 && !q.IsEmpty)
            Dequeue (q);
         else
            Enqueue (q, ++n);
      }
   }
   static void Dequeue (TQueue<int> q) => Console.Write ($"Dequeued : {q.Dequeue ()}\n");
   static void Enqueue (TQueue<int> q, int n) {
      q.Enqueue (n);
      Console.Write ($"Enqueued : {n}\n");
   }
}