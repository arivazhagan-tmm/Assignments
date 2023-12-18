using System.Text;
using static State;
using static System.Console;
using static System.ConsoleColor;
using static System.String;

var testCases = new string[] { @"D:\\\\TEST.DLL",
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
   bool parseFailed = false;
   foreach (var ch in str.Trim () + "~") {
      if (parseFailed) break;
      (state, todo) = (state, ch) switch {
         (A, >= 'A' and <= 'Z') => (B, () => directory = ch.ToString ()),
         (B, ':') => (C, none),
         (C, '\\') => (D, none),
         (D, >= 'A' and <= 'Z') => (D, () => filePath.Append (ch)),
         (D, '\\') => (D, () => {
            parseFailed = filePath.Length > 0 && filePath[^1] is '\\';
            filePath.Append (ch);
         }),
         (D, '.') => (E, () => {
            var tmp = filePath.ToString ();
            parseFailed = !tmp.Contains ('\\');
            if (!parseFailed) {
               var index = tmp.LastIndexOf ('\\');
               fileName = new string (tmp.TakeLast (tmp.Length - (index + 1)).ToArray ());
               filePath.Remove (index, fileName.Length + 1);
               parseFailed = fileName.Length is 0;
            }
         }),
         (E, >= 'A' and <= 'Z') => (E, () => extn.Append (ch)),
         (E, '~') => (F, none),
         _ => (Z, () => parseFailed = true)
      };
      todo ();
   }
   if (state is not F) directory = Empty;
   return (directory, filePath.ToString (), fileName, extn.ToString ());
}

public enum State { A, B, C, D, E, F, Z }
