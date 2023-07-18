namespace A._10.P02 {
   public class TDeque<T> {
      T[] mData = new T[4];
      int mUsed;
      public bool IsEmpty => mUsed == 0;
      public int Length => mData.Length;
      public void HeadEnqueue (T t) {
         if (mUsed == Length)
            UpdateQueue (2 * mUsed);
         else if (!IsEmpty)
            UpdateQueue (Length);
         mData[0] = t;
         mUsed++;
      }
      public T HeadDequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         T t = mData[0];
         mUsed--;
         UpdateQueue (Length, 0, 1);
         return t;
      }
      public void TailEnqueue (T t) {
         if (mUsed == Length)
            UpdateQueue (2 * mUsed, 0, 0);
         mData[mUsed++] = t;
      }
      public T TailDequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         return mData[--mUsed];
      }
      /// <summary> Updates queue to the given size where x and y are set depends on enqueue and dequeue</summary>
      void UpdateQueue (int size, int x=1, int y=0) {
         var tmp = new T[size];
         for (int i = 0; i < mUsed; i++)
            tmp[i + x] = mData[i + y];
         mData = tmp;
      }
   }
}
