namespace A_09_P01 {
   internal class TQueue<T> {
      T[] mData = new T[4];
      int mUsed, mRead, mWrite;
      public bool IsEmpty => mUsed == 0;
      public int Length => mData.Length;
      public void Enqueue (T t) {
         if (mUsed == Length) {
            var tmp = new T[2 * mUsed];
            for (int i = 0; i < mUsed; i++)
               tmp[i] = mData[(mRead + i) % Length];
            mData = tmp; mWrite = mUsed; mRead = 0;
         }
         mWrite %= Length;
         mData[mWrite++] = t;
         mUsed++;
      }
      public T Dequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         T t = mData[mRead % Length];
         mRead++;
         mUsed--;
         return t;
      }
   }
}
