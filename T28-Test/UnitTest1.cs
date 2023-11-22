using T28;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace T28_Test {
   [TestClass]
   public class UnitTest1 {
      #region Public Methods
      /// <summary>
      /// Initializes test for all functionalities in TQueue.</summary>
      [TestMethod]
      public void InitializeTest () {
         TestEnqueue ();
         TestDequeue ();
         TestPeek ();
         TestExceptions ();
      }
      #endregion

      #region Private Methods
      /// <summary>Tests the queue count using equality assertion</summary>
      void TestCount () => AreEqual (mQueue.Count, mTQueue.Count);

      /// <summary> Tests the enqueue functionality by performing count and peek tests. </summary>
      // After Enqueue operation the queue count should be same for defualt and custome queue.
      void TestEnqueue () {
         int[] ints = { 1, 2, 3, 4, 5 };
         foreach (var item in ints) {
            mQueue.Enqueue (item);
            mTQueue.Enqueue (item);
            TestCount ();
            TestPeek ();
         }
      }

      /// <summary> Tests the arise of exceptions if any invalid operation is performed. </summary>
      void TestExceptions () {
         mTQueue.Clear ();
         Action peek = () => mTQueue.Peek (),
                deque = () => mTQueue.Dequeue ();
         ThrowsException<InvalidOperationException> (peek);
         ThrowsException<InvalidOperationException> (deque);
      }

      /// <summary> Tests the dequeue functionality using equality assertion.</summary>
      // After dequeue operation the queue count should be same for defualt and custome queue.
      void TestDequeue () {
         AreEqual (mTQueue.Dequeue (), mQueue.Dequeue ());
         TestCount ();
      }

      /// <summary> Tests the peek functionality using equality assertion.</summary>
      // After peek operation the queue count should be same for defualt and custome queue.
      void TestPeek () {
         AreEqual (mTQueue.Peek (), mQueue.Peek ());
         TestCount ();
      }
      #endregion

      #region Private Data Members
      Queue<int> mQueue = new ();
      TQueue<int> mTQueue = new ();
      #endregion
   }
}