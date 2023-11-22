namespace T28 {
   public class TQueue<T> {
      #region Public Properties
      public bool IsEmpty => mUsed is 0;
      public int Length => mData.Length;
      public int Count => mUsed;
      #endregion

      #region Public Methods

      /// <summary> Clears the queue and sets the properties to defaults.</summary>
      public void Clear () {
         mData = new T[Length];
         (mRead, mUsed, mWrite) = (0, 0, 0);
      }

      /// <summary>Returns and removes the first element from the queue.</summary>
      public T Dequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Queue Empty");
         T t = mData[mRead % Length];
         mRead++;
         mUsed--;
         return t;
      }

      /// <summary>Adds the element at the queue end.</summary>
      // Doubles the queue capacity when the queue is filled to the current capacity.
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

      /// <summary> Returns the first element from the queue without removing.</summary>
      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Queue Empty");
         return mData[mRead % Length];
      }
      #endregion

      #region Private Data Members
      T[] mData = new T[4];
      int mUsed, mRead, mWrite;
      #endregion
   }
}
