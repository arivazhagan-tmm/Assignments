using System.Text;
using static System.Console;

internal class Program {
   const int size = 8;
   private static void Main (string[] args) {
      List<List<(int row, int col)>> soln = new ();
      List<(int row, int col)> valid = new (), invalid = new ();
      int row = 0, col = 0, temp = 0;
      bool foundSoln = false;
      // while loop runs till it finds all 92 solutions or 12 unique solutions
      while (!foundSoln) {
         // while loop runs till it finds all 8 valid places
         while (valid.Count < size) {
            for (; row < size; row++) {
               /* Break the loop if its not searching valid place at next immediate row. 
                * After break, search will start from ( 0 , col+1 ) th place */
               if (valid.Any () && row != valid.Last ().row + 1)
                  break;
               for (; col < size; col++) {
                  /* if valid place is found, then loop breaks and search starts from (row + 1, 0)th place
                   * and the corresponding invalid places are added for this valid place. */
                  if (!invalid.Contains ((row, col))) {
                     invalid.AddRange (GetInvalidLocations ((row, col)));
                     valid.Add ((row, col));
                     break;
                  }
               }
               col = 0;
            }
            /* if no valid places are found after iterating through all rows and columns then
            stop searching and break inside while the loop. */
            if (!valid.Any ())
               break;
            /*if valid places are less than size after iterating through all rows and columns then start back tracking
            by incrementing last added place's column and continue searching.*/
            else if (valid.Count < size) {
               row = valid.Last ().row; col = valid.Last ().col + 1;
               valid.Remove (valid.Last ());
               invalid.Clear ();
               valid.ForEach (x => invalid.AddRange (GetInvalidLocations (x)));
            }
         }
         invalid.Clear ();
         if (valid.Count == size && !soln.Any (x => valid.All (x.Contains))) {
            var cVal = Clone (valid);
            // Checking for unique solution
            if (IsUnique (cVal, soln))
               soln.Add (cVal);
            //searching for new valid places by back tracking found valid place
            valid.Remove (valid.Last ());
            valid.ForEach (x => invalid.AddRange (GetInvalidLocations (x)));
            row = valid.Last ().row;
            col = valid.Last ().col + 1;
         }
         // Placing queen at (0, col + 1)th place and continue searching the valid places
         else {
            valid.Clear ();
            temp += 1;
            row = 0;
            col = temp;
         }
         // All valid solutions are found if searching completes after placing queen at (0, 7)
         foundSoln = row == 0 && col > 7;
      }
      Print (soln);
   }

   /// <summary> returns all invalid locations based on given location </summary>
   static List<(int row, int col)> GetInvalidLocations ((int row, int col) loc) {
      List<(int row, int col)> invalidLoc = new ();
      for (int i = 1; i < size; i++) {
         if (loc.row + i < size && loc.col + i < size)
            invalidLoc.Add ((loc.row + i, loc.col + i));
         if (loc.row + i < size && loc.col - i >= 0)
            invalidLoc.Add ((loc.row + i, loc.col - i));
         if (loc.row + i < size)
            invalidLoc.Add ((loc.row + i, loc.col));
      }
      // if loc is (0, 0) then invalid locations are (1,0), (2,0).. (7,0) and its diagonals (1,1),(2,2)..(7,7)
      return invalidLoc;
   }

   /// <summary> returns if ref_ is duplicate or not after its rotations and mirrors </summary>
   static bool IsUnique (List<(int row, int col)> _ref, List<List<(int row, int col)>> soln) {
      for (int i = 0; i < 4; i++) {
         _ref = _ref.Select (loc => (loc.col, size - 1 - loc.row)).ToList ();
         if (soln.Any (s => _ref.All (s.Contains))) return false;
         if (soln.Any (s => _ref.Select (loc => (size - 1 - loc.row, loc.col)).ToList ().All (s.Contains))) return false;
      }
      return true;
   }

   /// <summary> Print solutions by forming 8x8 matrix and placing queen symbols </summary>
   static void Print (List<List<(int row, int col)>> soln) {
      soln = soln.OrderBy (x => x.First ().col).ToList ();
      int count = 1;
      OutputEncoding = Encoding.UTF8;
      foreach (var item in soln) {
         var cols = item.Select (x => x.col).ToList ();
         WriteLine ($" {count} ");
         WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
         for (int i = 0; i < size; i++) {
            Write ("│");
            for (int j = 0; j < size; j++)
               Write (cols[i] == j ? " ♕ │" : "   │");
            if (i == size - 1)
               break;
            WriteLine ("\n├───┼───┼───┼───┼───┼───┼───┼───┤");
         }
         WriteLine ("\n└───┴───┴───┴───┴───┴───┴───┴───┘\n\n");
         count++;
      }
   }

   /// <summary> Creating deep copy </summary>
   static List<(int row, int col)> Clone (List<(int row, int col)> loc) {
      List<(int row, int col)> cln = new ();
      loc.ForEach (cln.Add);
      return cln;
   }
}