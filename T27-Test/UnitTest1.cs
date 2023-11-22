using T27;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace T27_Test {
   [TestClass]
   public class UnitTest1 {

      #region Public Methods
      /// <summary> Initiates the testing for stack functionalities.</summary>
      [TestMethod]
      public void InitiateTest () {
         TestPush ();
         TestPop ();
         TestPeek ();
         TestExceptions ();
      }
      #endregion

      #region Private Methods
      /// <summary> Tests the current count elements in the stack using equality assertion.</summary>
      void TestCount () => AreEqual (mTStack.Count, mStack.Count);

      /// <summary> Tests the arise of exceptions when invalid operation is performed.</summary>
      void TestExceptions () {
         mStack.Clear ();
         mTStack.Clear ();
         Action mStackPeek = () => mStack.Peek (),
                mStackPop = () => mStack.Pop (),
                mTStackPeek = () => mTStack.Peek (),
                mTStackPop = () => mTStack.Pop ();
         ThrowsException<InvalidOperationException> (mStackPeek);
         ThrowsException<InvalidOperationException> (mStackPop);
         ThrowsException<InvalidOperationException> (mTStackPeek);
         ThrowsException<InvalidOperationException> (mTStackPop);
      }

      /// <summary> Tests the peek functionality using equality assertion.</summary>
      void TestPeek () => AreEqual (mTStack.Peek (), mStack.Peek ());

      /// <summary> Tests the pop functionality using equality assertion.</summary>
      void TestPop () => AreEqual (mTStack.Pop (), mStack.Pop ());

      /// <summary> Tests the push functionality using equality assertion.</summary>
      // Tests the count of the elements in the stack and TStack.
      // Tests the equality of recently pushed elements.
      void TestPush () {
         int[] ints = { 1, 2, 3, 4, 5, };
         foreach (var i in ints) {
            mStack.Push (i);
            mTStack.Push (i);
            TestCount ();
            TestPeek ();
         }
      }
      #endregion

      #region Private Data Members
      Stack<int> mStack = new ();
      TStack<int> mTStack = new ();
      #endregion

   }
}
