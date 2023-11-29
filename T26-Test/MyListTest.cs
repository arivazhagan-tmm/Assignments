using T26;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace T26_Test {
   #region class MyListTest -----------------------------------------------------------------------
   /// <summary>A test class which performs sequence of tests for each functionalities of class "MyList"</summary>
   [TestClass]
   public class MyListTest {
      #region Methods -----------------------------------------------
      [TestMethod]
      /// <summary>Initiates the testing of each list functionalities</summary>
      public void InitiateTest () {
         TestAdd ();
         TestInsert ();
         TestRemove ();
         TestRemoveAt ();
         TestClear ();
         TestExceptions ();
      }
      #endregion

      #region Implementation ----------------------------------------
      //Tests the add functionality using equality assertion
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

      //Tests the clear functionality using equality assertion of list properties
      void TestClear () {
         mList.Clear ();
         mMyList.Clear ();
         TestProperties ();
      }

      //Tests whether the exceptions thrown for invalid operations
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

      //Tests the insert functionality using equality assertion
      void TestInsert () {
         var (index, value) = (3, 10);
         mList.Insert (index, value);
         mMyList.Insert (index, value);
         TestProperties ();
         AreEqual (mMyList[index], mList[index]);
      }

      //Tests the properties using equality assertion
      void TestProperties () {
         AreEqual (mMyList.Count, mList.Count);
         AreEqual (mMyList.Capacity, mList.Capacity);
      }

      //Tests the remove functionality using boolean assertion
      void TestRemove () {
         int val = 5;
         mList.Remove (val);
         mMyList.Remove (val);
         TestProperties ();
         IsFalse (mList.Remove (val));
         IsFalse (mMyList.Remove (val));
      }

      //Tests the removeAt functionality using boolean assertion
      void TestRemoveAt () {
         int index = 3;
         mList.RemoveAt (index);
         mMyList.RemoveAt (index);
         TestProperties ();
         IsTrue (mList[index] == mMyList[index]);
      }
      #endregion

      #region Private data ------------------------------------------
      List<int> mList = new ();
      MyList<int> mMyList = new ();
      #endregion
   }
   #endregion
}