using System.Reflection;

internal class Program {
   private static void Main (string[] args) {
      var reader = new StreamReader (Assembly.GetExecutingAssembly ()?.GetManifestResourceStream ("Program1.data.input.txt")!);
      int count = 0;
      while (!reader.EndOfStream) {
         count++;
         var str = new string (reader.ReadLine ()?.Replace ($"Game {count}:", ""));
         var splits = str.Split (';');
         var game = new Game (id: count);
         game.Sets = new List<Set> ();
         int blue = 0, green = 0, red = 0;
         foreach (var item in splits) {
            var tmp = item.Split (',');
            foreach (var item2 in tmp) {
               if (item2.Contains ("blue")) blue += int.Parse (item2[..(item2.IndexOf ("blue"))]);
               if (item2.Contains ("green")) green += int.Parse (item2[..(item2.IndexOf ("green"))]);
               if (item2.Contains ("red")) red += int.Parse (item2[..(item2.IndexOf ("red"))]);
            }
            if (blue <= 14 && green <= 13 && red <= 12) game.Sets.Add (new (blue, green, red));
         }
      }
   }
}
public struct Set {
   public Set (int blue, int green, int red) => (Blue, Green, Red) = (blue, green, red);
   public int Blue { get; }
   public int Green { get; }
   public int Red { get; }
}
public struct Game {
   public Game (int id) => ID = id;
   public int ID { get; }
   public List<Set> Sets { get; set; }
}
