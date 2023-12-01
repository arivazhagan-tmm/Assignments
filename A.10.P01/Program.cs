using System.Text;
using static State;

var testCases = new string[] { @"C:\ETC\DOCUMENTS\WORDS\GUIDELINES.TXT" ,
                               @"C:\USERS\CLEMENT\SOLUTIONFILES\EVALUATOR\BIN\DEBUG\EVAL.DLL",
                               @"E:\USERS\CLEMENT\DESKTOP\PRACTICEDRAWINGS\CBLOCKPART.PDG"};
foreach (var testCase in testCases) {
   var (dir, path, file, extn) = ParseFileName (testCase);
   Console.WriteLine ($" Directory: {dir}\n File path: {path}\n File Name: {file}\n Extension: {extn}\n");
}

//Returns the four substring of the given string
//Refer the railway diagram for this state machine in "A.10.P01\data\FileNameParser_RailwayDiagram"
(string dir, string path, string file, string extn) ParseFileName (string str) {
   State state = A;
   Action none = () => { }, todo;
   string directory = "", fileName = "";
   StringBuilder filePath = new (), extn = new ();
   int index = 0;
   foreach (var ch in str.Trim () + "~") {
      (state, todo) = (state, ch) switch {
         (A, >= 'A' and <= 'Z') => (B, () => directory = ch.ToString ()),
         (B, ':') => (C, none),
         (C, '\\') => (D, none),
         (D, >= 'A' and <= 'Z' or '\\') => (D, () => filePath.Append (ch)),
         (D, '.') => (E, () => {
            index = filePath.ToString ().LastIndexOf ('\\');
            fileName = new string (filePath.ToString ().TakeLast (filePath.Length - (index + 1)).ToArray ());
            filePath = filePath.Remove (index, fileName.Length + 1);
         }
         ),
         (E, >= 'A' and <= 'Z') => (E, () => extn.Append (ch)),
         (E, '~') => (G, none),
         _ => (G, none)
      };
      todo ();
   }
   return (directory, filePath.ToString (), fileName, extn.ToString ());
}

public enum State { A, B, C, D, E, F, G }
