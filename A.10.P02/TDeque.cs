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
         mHead = mHead is 0 ? Length - 1 : mHead - 1;
         mData[mHead] = t;
         mUsed++;
      }
      public T HeadDequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         T t = mData[mHead];
         mHead = mHead == (Length - 1) ? 0 : mHead + 1;
         mUsed--;
         return t;
      }
      public void TailEnqueue (T t) {
         if (IsFull) UpdateQueue ();
         mData[mTail] = t;
         mTail = mTail == (Length - 1) ? 0 : mTail + 1;
         mUsed++;
      }
      public T TailDequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         mTail = mTail is 0 ? Length - 1 : mTail - 1;
         mUsed--;
         return mData[mTail];
      }
      #endregion

      #region Private Methods
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