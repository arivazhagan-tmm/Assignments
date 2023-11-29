using T28;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace T28_Test {
   #region class QueueTest ------------------------------------------------------------------------
   /// <summary>A test class which performs sequence of tests for each functionalities of class "TQueue"</summary>
   [TestClass]
   public class QueueTest {
      #region Methods -----------------------------------------------
      /// <summary>Initializes test for all functionalities in TQueue</summary>
      [TestMethod]
      public void InitializeTest () {
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
               <= 100 => TestDequeue,
               <= 200 and > 100 => TestPeek,
               <= 300 and > 280 => TestClear,
               _ => () => TestEnqueue (tmp)
            };
            action ();
         }
      }
      #endregion

      #region Implementation ----------------------------------------
      /// <summary>Tests the clear functionality by performing count, peek and dequeue tests</summary>
      void TestClear () {
         mQueue.Clear ();
         mTQueue.Clear ();
         TestCount ();
         TestDequeue ();
         TestPeek ();
      }

      /// <summary>Tests the queue count using equality assertion</summary>
      void TestCount () => AreEqual (mQueue.Count, mTQueue.Count);

      /// <summary>Tests the enqueue functionality by performing count and peek tests</summary>
      // After Enqueue operation the queue count should be same for defualt and custom queue.
      void TestEnqueue (int val) {
         mQueue.Enqueue (val);
         mTQueue.Enqueue (val);
         TestCount ();
         TestPeek ();
      }

      /// <summary>Tests the arise of exceptions if any invalid operation is performed</summary>
      bool ThrowsExceptions () {
         bool throwsException = mTQueue.IsEmpty && mTQueue.IsEmpty;
         if (throwsException) {
            Action peek = () => mTQueue.Peek (),
                   deque = () => mTQueue.Dequeue ();
            ThrowsException<InvalidOperationException> (peek);
            ThrowsException<InvalidOperationException> (deque);
         }
         return throwsException;
      }

      /// <summary>Tests the dequeue functionality using equality assertion</summary>
      // After dequeue operation the queue count should be same for defualt and custom queue.
      void TestDequeue () {
         if (!ThrowsExceptions ()) AreEqual (mTQueue.Dequeue (), mQueue.Dequeue ());
         TestCount ();
      }

      /// <summary>Tests the peek functionality using equality assertion</summary>
      // After peek operation the queue count should be same for defualt and custom queue.
      void TestPeek () {
         if (!ThrowsExceptions ()) AreEqual (mTQueue.Peek (), mQueue.Peek ());
         TestCount ();
      }
      #endregion

      #region Private data ------------------------------------------
      Queue<int> mQueue = new ();
      TQueue<int> mTQueue = new ();
      #endregion
   }
   #endregion
}