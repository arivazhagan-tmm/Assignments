namespace A._10.P02 {
   public class TDeque<T> {
      #region Public Properties
      public bool IsEmpty => mUsed == 0;
      public bool IsFull => mUsed == Length;
      public int Length => mData.Length;
      #endregion

      #region Public Methods
      public void HeadEnqueue (T t) {
         if (IsFull) UpdateQueue ();
         if (IsEmpty) ResetPointers ();
         else if (mHead == 0) mHead = Length - 1;
         else mHead--;
         mData[mHead] = t;
         mUsed++;
      }
      public T HeadDequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         T t = mData[mHead];
         if (mUsed == 1 && mHead == mTail) ResetPointers ();
         else if (mHead == (Length - 1)) mHead = 0;
         else mHead++;
         mUsed--;
         return t;
      }
      public void TailEnqueue (T t) {
         if (IsFull) UpdateQueue ();
         if (IsEmpty) ResetPointers ();
         else if (mTail == (Length - 1)) mTail = 0;
         else mTail++;
         mData[mTail] = t;
         mUsed++;
      }
      public T TailDequeue () {
         T t = mData[mTail];
         if (mUsed == 1 && mHead == mTail) ResetPointers ();
         else if (mTail == 0) mTail = Length - 1;
         else mTail--;
         mUsed--;
         return t;
      }
      #endregion

      #region Private Methods
      void ResetPointers () => (mHead, mTail) = (0, 0);
      void UpdateQueue () {
         int size = Length * 2;
         var tmp = new T[size];
         for (int i = mHead; i < Length; i++)
            tmp[size - (Length - i)] = mData[i];
         for (int i = 0; i <= mTail; i++)
            tmp[i] = mData[i];
         mHead = size - (Length - mHead);
         mData = tmp;
         return;
      }
      #endregion

      #region Private Data Members
      T[] mData = new T[4];
      int mUsed, mHead, mTail;
      #endregion
   }
}