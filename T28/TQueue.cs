namespace T28 {
   #region class TQueue ---------------------------------------------------------------------------
   /// <summary>A generic class depicts the typical functionality of the conventional class "Queue"</summary>
   public class TQueue<T> {
      #region Properties --------------------------------------------
      /// <summary>State of queue either empty or filled</summary>
      public bool IsEmpty => mUsed is 0;
      /// <summary>Current capacity of queue to store elements</summary>
      public int Length => mData.Length;
      /// <summary>Number of elements currently present in the queue</summary>
      public int Count => mUsed;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Clears the queue and sets the properties to defaults</summary>
      public void Clear () {
         Array.Clear (mData);
         (mRead, mUsed, mWrite) = (0, 0, 0);
      }

      /// <summary>Returns and removes the first element from the queue</summary>
      public T Dequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Queue Empty");
         T t = mData[mRead++];
         mRead %= Length;
         mUsed--;
         return t;
      }

      /// <summary>Adds given element at the end of queue</summary>
      //Doubles the queue capacity when the queue is filled to the current capacity.
      public void Enqueue (T t) {
         if (mUsed == Length) {
            var tmp = new T[2 * mUsed];
            for (int i = 0; i < mUsed; i++)
               tmp[i] = mData[(mRead + i) % Length];
            (mData, mRead, mWrite) = (tmp, 0, mUsed);
         }
         mWrite %= Length;
         mData[mWrite++] = t;
         mUsed++;
      }

      /// <summary>Returns the first element from the queue without removing</summary>
      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Queue Empty");
         return mData[mRead];
      }
      #endregion

      #region Private data ------------------------------------------
      T[] mData = new T[4];
      int mUsed, mRead, mWrite;
      #endregion
   }
   #endregion
}
