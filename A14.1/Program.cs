using System.Reflection;

using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"A14_1.data.words.txt");
using var reader = stream != null ? new StreamReader (stream) : null;
var words = new List<string> ();
while (reader != null && !reader.EndOfStream) words.Add (reader?.ReadLine () ?? "");
var dict = new Dictionary<List<string>, int> ();
while (words.Count > 0) {
   var str = Sort (words[0]);
   var anagrams = words.Where (w => w.Length == str.Length && str == Sort (w)).ToList ();
   if (anagrams.Count is 0) anagrams.Add (str);
   dict.Add (anagrams, anagrams.Count);
   anagrams.ForEach (a => words.Remove (a));
}
var sorted = dict.OrderByDescending (a => a.Value);
// File is generated in the output folder
using var sw = new StreamWriter ("anagrams.txt");
foreach (var item in sorted) {
   var str = $"{item.Value} ";
   item.Key.ForEach (w => str += $"{w} ");
   sw.WriteLine(str);
}

//Sorts and returns the given string
static string Sort (string str) {
   var arr = str.ToCharArray ();
   Array.Sort (arr);
   return new string (arr);
}
