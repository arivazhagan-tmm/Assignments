namespace T26 {
   #region class MyList ---------------------------------------------------------------------------
   /// <summary>A generic class depicts the typical functionality of the conventional class "List"</summary>
   public class MyList<T> {
      #region Constructor -------------------------------------------
      /// <summary>Initializes the MyList class with default properties</summary>
      //Default capacity is set to 4, capacity doubles on further addition of elements.
      public MyList () {
         (mCount, mCapacity) = (0, 4);
         mElements = new T[mCapacity];
      }
      #endregion

      #region Properties --------------------------------------------
      /// <summary>Number of elements present in the list</summary>
      public int Count => mCount;
      /// <summary>Maximum number of elements that can be stored in the list</summary>
      public int Capacity => mCapacity;
      /// <summary>Returns the element present in the given valid index</summary>
      public T this[int index]{
         get{
            ValidateIndex (index);
            return mElements[index];
         }
         set{
            ValidateIndex (index);
            mElements[index] = value;
         }
      }
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Adds given element into the list.</summary>
      ///Doubles the list capacity when it is completly filled.
      public void Add (T a) {
         Update ();
         mElements[mCount++] = a;
      }

      /// <summary>Clears all the elements from list</summary>
      public void Clear () {
         mCount = 0;
         Array.Clear (mElements);
      }

      /// <summary>Inserts the given element at the given index.</summary>
      ///Throws an exception if the given index is not in the valid range.
      ///Doubles the list capacity if it is filled to current capacity.
      public void Insert (int index, T a) {
         ValidateArgument (index);
         Update ();
         for (int i = mCount; i > index; i--) mElements[i] = mElements[i - 1];
         mElements[index] = a;
         mCount++;
      }

      /// <summary>Removes given element from list.</summary>
      ///Throws an exception if the given element is not present in the list.
      public bool Remove (T a) {
         var index = Array.IndexOf (mElements, a);
         if (index == -1) return false;
         RemoveAt (index);
         return true;
      }

      /// <summary>Removes the element present in given index.</summary>
      ///Throws an exception if the given index is not in the valid range.
      public void RemoveAt (int index) {
         ValidateArgument (index);
         for (int i = index; i < mCount - 1; i++) mElements[i] = mElements[i + 1];
         mElements[mCount] = default!;
         mCount--;
      }
      #endregion

      #region Implementation ----------------------------------------
      //Returns whether the given value is present in the valid range or not
      bool IsInvalid (int val) => val < 0 || val >= mCount;

      //Throws an exception if the arguement is not in the valid range
      void ValidateArgument (int arg) {
         if (IsInvalid (arg)) throw new ArgumentOutOfRangeException ($"Given argument {arg} is not in the valid range.");
      }

      //Throws an exception if the index is not in the valid range
      void ValidateIndex (int index) {
         if (IsInvalid (index)) throw new IndexOutOfRangeException ($"Cannot access the element at the given index {index}.");
      }

      //Doubles the current list capacity
      void Update () {
         if (mCount == mCapacity) {
            mCapacity *= 2;
            var tmp = new T[mCapacity];
            for (int i = 0; i < mCount; i++) tmp[i] = mElements[i];
            mElements = tmp;
         }
      }
      #endregion

      #region Private data ------------------------------------------
      T[] mElements;
      int mCount;
      int mCapacity;
      #endregion
   }
   #endregion
}