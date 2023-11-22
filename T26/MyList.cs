namespace T26 {
   public class MyList<T> {
      #region Constructor
      public MyList () {
         (mCount, mCapacity) = (0, 4);
         mElements = new T[mCapacity];
         mDefault = default;
      }
      #endregion

      #region Public Properties
      public int Count { get => mCount; }
      public int Capacity { get => mCapacity; }
      public T this[int index]
      {
         get
         {
            ValidateIndex (index);
            return mElements[index];
         }
         set
         {
            ValidateIndex (index);
            mElements[index] = value;
         }
      }
      #endregion

      #region Public Methods
      /// <summary> Adds given element into the list. </summary>
      // Doubles the list capacity when it is completly filled.
      public void Add (T a) {
         Update ();
         mElements[mCount++] = a;
      }

      /// <summary> Clears all the elements from list by setting default values to them. </summary>
      public void Clear () {
         if (mDefault != null)
            for (int i = 0; i < mCount;) mElements[i++] = mDefault;
         mCount = 0;
      }

      /// <summary> Inserts the given element at the given index. </summary>
      // Throws an exception if the given index is not in the valid range.
      // Doubles the list capacity if it is filled to current capacity.
      public void Insert (int index, T a) {
         ValidateArgument (index);
         Update ();
         var tmp = new T[mCapacity];
         for (int i = 0, j = 0; i < mCount; i++) {
            if (i == index) (tmp[i], j) = (a, 1);
            tmp[i + j] = mElements[i];
         }
         mElements = tmp;
         mCount++;
      }

      /// <summary> Removes given element from list. </summary>
      // Throws an exception if the given element is not present in the list.
      public bool Remove (T a) {
         for (int i = 0; i < mCount; i++) {
            T tmp = mElements[i];
            if (tmp != null && tmp.Equals (a)) {
               RemoveAt (i);
               return true;
            }
         }
         return false;
      }

      /// <summary> Removes the element present in given index. </summary>
      // Throws an exception if the given index is not in the valid range.
      public void RemoveAt (int index) {
         ValidateArgument (index);
         bool found = false;
         for (int i = 0; i < mCount; i++) {
            if (i == index) found = true;
            if (found && i + 1 < mCount) mElements[i] = mElements[i + 1];
         }
         if (mDefault != null) mElements[--mCount] = mDefault;
      }
      #endregion

      #region Private Methods
      bool IsInvalid (int val) => val < 0 || val >= mCount;

      void ValidateArgument (int arg) {
         if (IsInvalid (arg)) throw new ArgumentOutOfRangeException ($"Given argument {arg} is not in the valid range.");
      }

      /// <summary> Validates the given index for non negativity and max limit condition. </summary>
      void ValidateIndex (int index) {
         if (IsInvalid (index)) throw new IndexOutOfRangeException ($"Cannot access the element at the given index {index}.");
      }

      /// <summary> Doubles the current list capacity. </summary>
      void Update () {
         if (mCount == mCapacity) {
            mCapacity *= 2;
            var tmp = new T[mCapacity];
            for (int i = 0; i < mCount; i++) tmp[i] = mElements[i];
            mElements = tmp;
         }
      }
      #endregion

      #region Private Fields
      T[] mElements;
      readonly T? mDefault;
      int mCount;
      int mCapacity;
      #endregion
   }
}