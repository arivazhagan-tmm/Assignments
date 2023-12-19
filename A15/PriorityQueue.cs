namespace A15;

#region class PriorityQueue -----------------------------------------------------------------------
/// <summary>
/// A generic class depicts the typical functionality of the conventional class "Queue".
/// Dequeue functionality returns the least object present in the queue. 
/// </summary>
public class PriorityQueue<T> where T : IComparable<T> {
   #region Constructor ----------------------------------------------
   public PriorityQueue () {
      (mCount, mCapacity) = (0, 4);
      mElements = new T[mCapacity];
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds the given element into the queue</summary>
   public void Enqueue (T t) {
      if (mCount == mCapacity) {
         mCapacity *= 2;
         var tmp = new T[mCapacity];
         for (int i = 0; i < mCount; i++) tmp[i] = mElements[i];
         mElements = tmp;
      }
      mElements[mCount++] = t;
      var rChild = mCount - 1;
      while (rChild > 0) {
         var lChild = rChild / 2; // Left child
         var (lValue, rValue) = (mElements[lChild], mElements[rChild]);
         if (rValue.CompareTo (lValue) >= 0) break;
         (mElements[lChild], mElements[rChild]) = (rValue, lValue);
         rChild = lChild;
      }
   }

   /// <summary>Returns the element which is least object among the all other elements in the queue</summary>
   public T Dequeue () {
      var (index, parent) = (--mCount, 0);
      T element = mElements[parent];
      mElements[parent] = mElements[index];
      while (true) {
         // Left child and right child
         var (lChild, rChild) = (2 * parent, (2 * parent) + 1);
         if (lChild > index) break;
         // value stored in parent, left child and right child
         var (pValue, lValue, rValue) = (mElements[parent], mElements[lChild], mElements[rChild]);
         // Swapping child values
         if (rChild <= index && rValue.CompareTo (lValue) < 0) {
            lValue = mElements[rChild];
            lChild = rChild;
         }
         // Aborting the sort if parent value in lesser than or equal to left child's value
         if (pValue.CompareTo (lValue) <= 0) break;
         // Swapping the values of left child and parent
         (mElements[lChild], mElements[parent]) = (pValue, lValue);
         parent = lChild;
      }
      return element;
   }
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Number of elements currently present in the queue</summary>
   public int Count => mCount;
   #endregion

   #region Private data ---------------------------------------------
   T[] mElements;
   // Number of elements currently present in the queue
   int mCount;
   // Current capacity of the queue
   int mCapacity;
   #endregion
}
#endregion