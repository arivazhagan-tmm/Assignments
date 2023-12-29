using System.Text;
using static State;
using static System.Console;
using static System.ConsoleColor;
using static System.String;

var testCases = new string[] { @"D:\ETC\\NEW\IP.IO",
                               @"D:\\\\TEST.DLL",
                               @"\\D:FILES\TEST.DLL",
                               @"D\:FILES\TEST.DLL",
                               @"D:FILES\TEST.DLL",
                               @":FILES\TEST.DLL",
                               @"D:\FILES\TEST.DLL.DXF",
                               @"D\FILES\TEST.DLL",
                               @"D:\TEST.DLL",
                               @"D:\FILES\\\TEST.DLL",
                               @"D:\D:\FILES\TEST.DLL.",
                               @"D:/FILES/TEST.DLL.",
                               @"CD:\FILES\TEST.DLL",
                               @"D:\FILES\.DLL",
                               @"C:\ETC\DOCUMENTS\WORDS\" ,
                               @"D:\FILES\TEST.DLL\",
                               @"D:\FILES\TEST.\DLL",
                               @"C:\USERS\CLEMENT\SOLUTIONFILES\EVALUATOR\\BIN\DEBUG\EVAL.DLL",
                               @"C:\ETC\DOCUMENTS\WORDS\GUIDELINES.TXT" ,
                               @"C:\USERS\CLEMENT\SOLUTIONFILES\EVALUATOR\BIN\DEBUG\EVAL.DLL",
                               @"E:\USERS\CLEMENT\DESKTOP\PRACTICEDRAWINGS\CBLOCKPART.PDG"};
foreach (var testCase in testCases) {
   var (dir, path, file, extn) = ParseFileName (testCase);
   if (IsNullOrEmpty (dir)) Print ($" {testCase} - Invalid directory.", Red);
   else {
      Print (testCase, Green);
      Print ($" Directory: {dir}\n File path: {path}\n File Name: {file}\n Extension: {extn}\n");
   }
}

// Prints the given string to console with given color
void Print (string str, ConsoleColor color = White) {
   ForegroundColor = color;
   WriteLine (str + "\n");
   ResetColor ();
}

// Returns the four substring of the given string
// Refer the railway diagram for this state machine in "A.10.P01\data\FileNameParser_RailwayDiagram"
(string dir, string path, string file, string extn) ParseFileName (string str) {
   State state = A;
   Action none = () => { }, todo;
   StringBuilder filePath = new (), extn = new ();
   var (directory, fileName) = (Empty, Empty);
   foreach (var ch in str.Trim () + "~") {
      if (state is Z) break;
      (state, todo) = (state, ch) switch {
         (A, >= 'A' and <= 'Z') => (B, () => directory = ch.ToString ()),
         (B, ':') => (C, none),
         (C, '\\') => (D, none),
         (D or E, >= 'A' and <= 'Z') => (E, () => filePath.Append (ch)),
         (E, '\\') => (F, () => filePath.Append (ch)),
         (F or G, >= 'A' and <= 'Z') => (G, () => filePath.Append (ch)),
         (G, '\\') => (F, () => filePath.Append (ch)),
         (G, '.') => (H, none),
         (H, >= 'A' and <= 'Z') => (H, () => extn.Append (ch)),
         (H, '~') => (I, none),
         _ => (Z, none)
      };
      todo ();
   }
   if (state is not I) directory = Empty;
   else {
      var tmp = filePath.ToString ();
      var index = tmp.LastIndexOf ('\\');
      fileName = new string (tmp.TakeLast (tmp.Length - (index + 1)).ToArray ());
      filePath.Remove (index, fileName.Length + 1);
   }
   return (directory, filePath.ToString (), fileName, extn.ToString ());
}

public enum State { A, B, C, D, E, F, G, H, I, Z }
