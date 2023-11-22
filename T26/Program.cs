using T26;
using static System.Console;

var mList = new MyList<int> ();
Random r = new ();
for (int i = 1; i <= 50; i++) {
   var (tmp1, tmp2) = (r.NextDouble (), r.Next (0, i));
   if (tmp1 <= 0.25) {
      mList.Add (i);
      ForegroundColor = ConsoleColor.Green;
      CursorLeft = (WindowWidth / 5) - 5;
      WriteLine ($"\nAdded : {i}");
      ShowList ();
   } else if (tmp1 is > 0.25 and <= 0.5 && tmp2 < mList.Count) {
      mList.RemoveAt (tmp2);
      ForegroundColor = ConsoleColor.Red;
      WriteLine ($"\nRemoved at : {tmp2}");
      ShowList ();
   } else if (tmp2 > 0 && tmp2 < mList.Count) {
      tmp2 -= 1;
      mList.Insert (tmp2, i);
      ForegroundColor = ConsoleColor.Yellow;
      WriteLine ($"\nInserted {i} at : {tmp2}");
      ShowList ();
   }
}
ForegroundColor = ConsoleColor.White;
WriteLine ($"\nList count is {mList.Count} and capacity is {mList.Capacity}");

void ShowList () {
   ResetColor ();
   if (mList?.Count > 0) {
      Write ("List items : ");
      for (int j = 0, count = mList.Count; j < count; j++) Write ($" {mList[j]},");
      WriteLine ();
   } else Write ("\nList is Empty!!");
}