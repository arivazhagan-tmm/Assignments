using A15;

namespace A15_Test {
   #region class PriorityQueueTest ----------------------------------------------------------------
   /// <summary>A test class which performs sequence of tests for each functionalities of class "PriorityQueue"</summary>
   [TestClass]
   public class PriorityQueueTest {
      #region Methods -----------------------------------------------
      /// <summary>Initiates the functionality tests of class PriorityQueue</summary>
      [TestMethod]
      public void InitiateTest () {
         var queue = new PriorityQueue<int> ();
         var list = new List<int> ();
         var numbers = Program.GenerateRandomNumbers (200);
         foreach (var num in numbers) {
            if (num <= 100) {
               queue.Enqueue (num);
               list.Add (num);
            } else if (queue.Count > 0) {
               list.Sort ();
               Assert.AreEqual (queue.Dequeue (), list.First ());
               list.RemoveAt (0);
            }
         }
      }
      #endregion
   }
   #endregion
}