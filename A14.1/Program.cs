using System.Diagnostics;
using System.Reflection;

using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"A14_1.data.words.txt");
using var reader = stream != null ? new StreamReader (stream) : null;
var words = new List<string> ();
if (reader != null) while (!reader.EndOfStream) words.Add (reader.ReadLine () ?? "");
var dict = new Dictionary<string, List<string>> ();
words.ForEach (word => {
   var tmp = new string (word.Order ().ToArray ());
   if (!dict.TryGetValue (tmp, out var anagrams)) dict.Add (tmp, anagrams = new ());
   anagrams.Add (word.ToUpper ());
});
// File is generated in the data folder
var path = "../../../data/anagrams.txt";
using var sw = new StreamWriter (path);
var result = dict.Where (a => a.Value.Count >= 2).OrderByDescending (a => a.Value.Count);
foreach (var item in result)
   sw.WriteLine ($"{item.Value.Count} {string.Join (' ', item.Value)}");
Process.Start ("notepad.exe", path);