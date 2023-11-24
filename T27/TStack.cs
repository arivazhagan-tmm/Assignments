namespace T27;

#region Public Class--------------------------------------------------------------------------------
/// <summary> A generic class depicts the typical functionality of the conventional class "Stack".</summary>
public class TStack<T> {

   #region Public Properties------------------------------------------
   /// <summary>State of stack either empty or filled.</summary>
   public bool IsEmpty => mPointer is 0;
   /// <summary> Current capacity of the stack to store elements.</summary>
   public int Length => mElements.Length;
   /// <summary> Number of elements currently present in the stack.</summary>
   public int Count => mPointer;
   #endregion

   #region Public Methods---------------------------------------------
   /// <summary> Pushes the given element into the stack.</summary>
   // Doubles the stack capacity when the stack is completely filled to the current capacity.
   public void Push (T a) {
      if (mPointer == Length) {
         var tmp = new T[Length * 2];
         for (int i = 0; i < Length; i++) tmp[i] = mElements[i];
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
      Array.Clear (mElements);
      mPointer = 0;
   }
   #endregion

   #region Private Fields---------------------------------------------
   T[] mElements = new T[4];
   int mPointer;
   #endregion
}
#endregion