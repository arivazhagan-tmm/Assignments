namespace A15;
using static System.Console;

#region class Program -----------------------------------------------------------------------------
public class Program {
   #region Methods --------------------------------------------------
   /// <summary>Generates and returns the unique random values for the given count</summary>
   public static int[] GenerateRandomNumbers (int count) {
      int[] numbers = new int[count];
      var r = new Random ();
      var (len, ptr) = (200, 0);
      while (ptr < len) {
         var tmp = r.Next (1, len + 1);
         if (!numbers.Contains (tmp)) numbers[ptr++] = tmp;
      }
      return numbers;
   }
   #endregion

   #region Implementation -------------------------------------------
   // Displays the functionality of class PriorityQueue
   static void Main () {
      var numbers = GenerateRandomNumbers (200);
      var list = new List<int> ();
      var queue = new PriorityQueue<int> ();
      foreach (var num in numbers) {
         if (num <= 100) {
            queue.Enqueue (num);
            list.Add (num);
            ForegroundColor = ConsoleColor.Green;
            Write ($"Enqueued: {num}");
            WriteLine ();
         } else if (queue.Count > 0) {
            ForegroundColor = ConsoleColor.Red;
            var tmp = queue.Dequeue ();
            Write ($"Dequeued: {tmp}");
            WriteLine ();
            list.Remove (tmp);
         }
         ResetColor ();
      }
   }
   #endregion
}
#endregion