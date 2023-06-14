using TStack;

public class Program {
   public static void Main (string[] args) {
      TQueue<int> q = new ();
      int n;
      for (n = 1; n < 5; n++)
         Enqueue (q, n);
      Console.WriteLine ();
      for (n = 0; n < 3; n++)
         Dequeue (q);
      Console.WriteLine ();
      for (n = 1; n <5; n++)
         Enqueue (q, n * 100);
      Console.WriteLine ($"\nCurrent queue size is : {q.Length}\n");
      for (n = 1; n <4; n++)
         Dequeue (q);
      Console.WriteLine ($"\nIs Queue empty : {q.IsEmpty}\n");
      for (n = 1; n < 4; n++)
         Enqueue (q, n * 10); 
      Console.WriteLine ($"\nIs Queue empty : {q.IsEmpty}\n");
      while (!q.IsEmpty)
         Dequeue (q);
      Console.WriteLine ($"\nIs Queue empty : {q.IsEmpty}\n");
      for (n = 1; n <= 4; n++)
         Enqueue (q, n * 10);
      Console.WriteLine ();
      Dequeue (q);
      Console.WriteLine ($"\nCurrent queue size is : {q.Length}");

      //Console.WriteLine (q.IsEmpty);
      //for (int n = 0; n < 4; n++) {
      //   q.Enqueue (n);
      //}
      //q.Dequeue ();
      //q.Dequeue ();
      //Console.WriteLine (q.IsEmpty);
      //for (int n = 0; n < 2; n++) {
      //   q.Enqueue (n * 10);
      //}
      //Console.WriteLine (q.IsEmpty);
      //for (int n = 0; n < q.Length; n++) {
      //   Console.WriteLine (q.Dequeue ());
      //}
      //Console.WriteLine (q.IsEmpty);
      //for (int n = 0; n < 4; n++) {
      //   q.Enqueue (n * 10);
      //}
   }

   static void Dequeue (TQueue<int> q) => Console.Write ($"Dequeued : {q.Dequeue ()}\n");
   static void Enqueue (TQueue<int> q, int n) {
      q.Enqueue (n);
      Console.Write ($"Enqueued : {n}\n");
   }
}