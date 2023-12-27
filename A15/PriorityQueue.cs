namespace A15;

#region class PriorityQueue -----------------------------------------------------------------------
/// <summary>
/// A generic class depicts the typical functionality of the conventional class "Queue".
/// Dequeue functionality returns the least object present in the queue. 
/// </summary>
public class PriorityQueue<T> where T : IComparable<T> {
   #region Constructor ----------------------------------------------
   public PriorityQueue () => mList = new () { default! };
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds the given element into the queue</summary>
   public void Enqueue (T t) {
      mList.Add (t);
      mCount++;
      var left = mCount;
      while (left > 0) {
         var parent = left / 2;
         var (pValue, lValue) = (mList[parent], mList[left]);
         if (lValue.CompareTo (pValue) >= 0) break;
         (mList[parent], mList[left]) = (lValue, pValue);
         left = parent;
      }
   }

   /// <summary>Returns the element which is least object among the all other elements in the queue</summary>
   public T Dequeue () {
      var (last, parent) = (mCount, 1);
      T element = mList[parent];
      mList[parent] = mList[last];
      mCount--;
      while (true) {
         // Left child and right child
         var (left, right) = (2 * parent, (2 * parent) + 1);
         if (left >= last) break;
         // value stored in parent, left child and right child
         var (pValue, lValue, rValue) = (mList[parent], mList[left], mList[right]);
         // Swapping child values
         if (right <= last && rValue.CompareTo (lValue) < 0) {
            lValue = mList[right];
            left = right;
         }
         // Aborting the sort if parent value in lesser than or equal to left child's value
         if (pValue.CompareTo (lValue) <= 0) break;
         // Swapping the values of left child and parent
         (mList[left], mList[parent]) = (pValue, lValue);
         parent = left;
      }
      mList.RemoveAt (last);
      return element;
   }
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Number of elements currently present in the queue</summary>
   public int Count => mCount;
   #endregion

   #region Private data ---------------------------------------------
   // List to store the queue elements
   List<T> mList;
   // Number of elements currently present in the queue
   int mCount;
   #endregion
}
#endregion