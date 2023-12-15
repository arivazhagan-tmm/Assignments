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
      var index = mCount - 1;
      while (index > 0) {
         var tmp = index / 2;
         if (mElements[index].CompareTo (mElements[tmp]) >= 0) break;
         (mElements[tmp], mElements[index]) = (mElements[index], mElements[tmp]);
         index = tmp;
      }
   }

   /// <summary>Returns the element which is least object among the all other elements in the queue</summary>
   public T Dequeue () {
      var (index, n) = (--mCount, 0);
      T element = mElements[n];
      mElements[n] = mElements[index];
      mElements[index--] = default!;
      while (true) {
         int tmp1 = n * 2;
         if (tmp1 > index) break;
         int tmp2 = tmp1 + 1;
         if (tmp2 <= index && mElements[tmp2].CompareTo (mElements[tmp1]) < 0) tmp1 = tmp2;
         if (mElements[n].CompareTo (mElements[tmp1]) <= 0) break;
         (mElements[tmp1], mElements[n]) = (mElements[n], mElements[tmp1]);
         n = tmp1;
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