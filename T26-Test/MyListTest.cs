using T26;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace T26_Test {
   #region Public Class-----------------------------------------------------------------------------
   /// <summary> A test class which performs sequence of tests for each functionalities of class "MyList"</summary>
   [TestClass]
   public class MyListTest {
      #region Public Methods------------------------------------------
      [TestMethod]
      /// <summary> Initiates the testing of each list functionalities. </summary>
      public void InitiateTest () {
         TestAdd ();
         TestInsert ();
         TestRemove ();
         TestRemoveAt ();
         TestClear ();
         TestExceptions ();
      }
      #endregion

      #region Private Methods-----------------------------------------
      /// <summary> Tests the add functionality using equality assertion.</summary>
      void TestAdd () {
         int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
         foreach (var val in values) {
            mList.Add (val);
            mMyList.Add (val);
         }
         TestProperties ();
         AreEqual (mMyList[^1], mList[^1]);
         mMyList[^1] = 99;
         AreNotEqual (mMyList[^1], mList[^1]);
      }

      /// <summary> Tests the clear functionality using equality assertion of list properties.</summary>
      void TestClear () {
         mList.Clear ();
         mMyList.Clear ();
         TestProperties ();
      }

      /// <summary> Tests whether the exceptions thrown for invalid operations.</summary>
      void TestExceptions () {
         var (index, value) = (5, 10);
         Action remove = () => mMyList.RemoveAt (index),
                insert = () => mMyList.Insert (index, value),
                assign = () => mMyList[index] = value,
                retrieve = () => value = mMyList[index];
         ThrowsException<ArgumentOutOfRangeException> (remove);
         ThrowsException<ArgumentOutOfRangeException> (insert);
         ThrowsException<IndexOutOfRangeException> (assign);
         ThrowsException<IndexOutOfRangeException> (retrieve);
      }

      /// <summary> Tests the insert functionality using equality assertion.</summary>
      void TestInsert () {
         var (index, value) = (3, 10);
         mList.Insert (index, value);
         mMyList.Insert (index, value);
         TestProperties ();
         AreEqual (mMyList[index], mList[index]);
      }

      /// <summary> Tests the properties using equality assertion.</summary>
      void TestProperties () {
         AreEqual (mMyList.Count, mList.Count);
         AreEqual (mMyList.Capacity, mList.Capacity);
      }

      /// <summary> Tests the remove functionality using boolean assertion.</summary>
      void TestRemove () {
         int val = 5;
         mList.Remove (val);
         mMyList.Remove (val);
         TestProperties ();
         IsFalse (mList.Remove (val));
         IsFalse (mMyList.Remove (val));
      }

      /// <summary> Tests the removeAt functionality using boolean assertion.</summary>
      void TestRemoveAt () {
         int index = 3;
         mList.RemoveAt (index);
         mMyList.RemoveAt (index);
         TestProperties ();
         IsTrue (mList[index] == mMyList[index]);
      }
      #endregion

      #region Private Fields------------------------------------------
      List<int> mList = new ();
      MyList<int> mMyList = new ();
      #endregion
   }
   #endregion
}