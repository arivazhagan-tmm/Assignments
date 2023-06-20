namespace Eval;
class Program {
   static readonly Evaluator mEval = new ();
   static void Main (string[] args) {
        Console.WriteLine("_____Test Cases______\n");
        string[] testCases = { "-10+25", "-1*3++4","a=1*+1", "a=1/*2", "a=1/-2", "b=-a/2", "a=-1+2*4--10",
                             "a=-1+2--3++4*-1*--2", "a=-1+2--3++4*-1*-2", "a=-3*5", "a=3*-8" };
      foreach (var expr in testCases) {
         Console.Write ($"{expr} : ");
         PrintResult (expr);
         Console.WriteLine ();
      }
        Console.WriteLine("______Write your expression below_____\n");
        for (; ; ) { 
         Console.Write ("> ");
         string text = Console.ReadLine () ?? "";
         if (text == "exit") break;
         PrintResult (text);
      }
   }

   static void PrintResult (string str) {
      try {
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine (mEval.Evaluate (str));
      } catch (Exception e) {
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine (e.Message);
      }
      Console.ResetColor ();
   }
}
