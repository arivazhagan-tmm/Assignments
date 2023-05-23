using System.Text;

internal class Program {
   const int size = 8;
   private static void Main (string[] args) {
      List<List<(int i, int j)>> soln = new ();
      List<(int i, int j)> val = new (), inval = new ();
      int i = 0, j = 0, k = 0;
      bool foundSoln = false;
      while (!foundSoln) {
         while (val.Count < size) {
            for (; i < size; i++) {
               if (val.Any() && i != val.Last ().i + 1)
                  break;
               for (; j < size; j++) {
                  if (!inval.Contains ((i, j))) {
                     inval.AddRange (GetInvalLoc ((i, j)));
                     val.Add ((i, j));
                     break;
                  }
               }
               j = 0;
            }
            if (!val.Any())
               break;
            else if (val.Count < size) {
               i = val.Last ().i; j = val.Last ().j + 1;
               val.Remove (val.Last ());
               inval.Clear ();
               val.ForEach (x => inval.AddRange (GetInvalLoc (x)));
            }
         }
         inval.Clear ();
         if (val.Count == size && !soln.Any (x => val.All (x.Contains))) {
            var cVal = Clone (val);
            //Checking for unique solution
            if (IsUnique (cVal, soln))
               soln.Add (cVal);
            val.Remove (val.Last ());
            val.ForEach (x => inval.AddRange (GetInvalLoc (x)));
            //Back tracking the locations
            i = val.Last ().i;
            j = val.Last ().j + 1;
         } else {
            val.Clear ();
            k += 1;
            i = 0;
            j = k;
         }
         foundSoln = i == 0 && j > 7;
      }
      Print (soln);
   }

   static List<(int i, int j)> GetInvalLoc ((int i, int j) loc) {
      List<(int i, int j)> l = new ();
      for (int i = 1; i < size; i++) {
         if (loc.i + i < size && loc.j + i < size)
            l.Add ((loc.i + i, loc.j + i));
         if (loc.i + i < size && loc.j - i >= 0)
            l.Add ((loc.i + i, loc.j - i));
         if (loc.i + i < size)
            l.Add ((loc.i + i, loc.j));
      }
      return l;
   }

   static bool IsUnique (List<(int i, int j)> loc, List<List<(int i, int j)>> soln) {
      for (int i = 0; i < 4; i++) {
         loc = loc.Select (i => (i.j, size - 1 - i.i)).ToList ();
         if (soln.Any (r => loc.All (r.Contains))) return false;
         if (soln.Any (r => loc.Select (i => (size - 1 - i.i, i.j)).ToList ().All (r.Contains))) return false;
      }
      return true;
   }

   static void Print (List<List<(int i, int j)>> soln) {
      soln = soln.OrderBy (x => x.First ().j).ToList ();
      int count = 1;
      Console.OutputEncoding = Encoding.UTF8;
      foreach (var item1 in soln) {
         var cols = item1.Select (x => x.j).ToList ();
         Console.WriteLine ($" {count} ");
         Console.WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
         for (int i = 0; i < size; i++) {
            Console.Write ("│");
            for (int j = 0; j < size; j++)
               Console.Write (cols[i] == j ? " ♕ │" : "   │");
            if (i == size - 1)
               break;
            Console.WriteLine ("\n├───┼───┼───┼───┼───┼───┼───┼───┤");
         }
         Console.WriteLine ("\n└───┴───┴───┴───┴───┴───┴───┴───┘\n\n");
         count++;
      }
   }

   static List<(int i, int j)> Clone (List<(int i, int j)> loc) {
      List<(int i, int j)> cln = new ();
      loc.ForEach (cln.Add);
      return cln;
   }
}