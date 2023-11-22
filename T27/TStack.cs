namespace T27;

public class TStack<T> {

   #region Public Properties
   public bool IsEmpty => mPointer is 0;
   public int Length => mElements.Length;
   public int Count { get => mPointer; }
   #endregion

   #region Public Methods
   /// <summary> Pushes the given element into the stack.</summary>
   // Doubles the stack capacity when the stack is completely filled to the current capacity.
   public void Push (T a) {
      if (mPointer == Length) {
         var tmp = new T[Length * 2];
         for (int i = 0; i < Length; i++)
            tmp[i] = mElements[i];
         mElements = tmp;
      }
      mElements[mPointer++] = a;
   }

   /// <summary> Returns and removes the top most element from the stack.</summary>
   // Throws an exception if the stack is empty.
   public T Pop () {
      if (IsEmpty) throw new InvalidOperationException ("Stack is empty!");
      return mElements[--mPointer];
   }

   /// <summary> Returns the top most element from the stack without removing. </summary>
   // Throws an exception if the stack is empty.
   public T Peek () {
      if (IsEmpty) throw new InvalidOperationException ("Stack is empty!");
      return mElements[mPointer - 1];
   }

   /// <summary> Clears the stack and sets properties to defaults. </summary>
   public void Clear () {
      mElements = new T[Length];
      mPointer = 0;
   }
   #endregion

   #region Private Data Members
   T[] mElements = new T[4];
   int mPointer;
   #endregion
}