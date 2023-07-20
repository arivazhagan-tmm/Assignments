namespace Eval;
class Program {
   static readonly Evaluator mEval = new ();
   static void Main (string[] args) {
      Console.WriteLine ("_____Test Cases______\n");
      string[] testCases = { "-10+25", "-1*3++4","a=1*+1", "a=1/*2", "a=1/-2", "b=-a/2", "a=-1+2*4--10",
                             "a=-1+2--3++4*-1*--2", "a=-1+2--3++4*-1*-2", "a=-3*5", "a=3*-8",
                             "a=-4*(3+5)","a=(3+5)*-4","a=-4*(-3+5)","a=(3-5)*-4","a=(-3*5)*(5*-2)+-6"};
      foreach (var expr in testCases) {
         Console.Write ($"{expr} : ");
         PrintResult (expr);
         Console.WriteLine ();
      }
      Dictionary<string, double> mTestData = new () {
         // Add tests for each operator. Start with the binary operators
         ["2+3"] = 5, ["3+5+6"] = 14, ["(10+5)+8"] = 23, ["5-3"] = 2, ["3-5"] = -2,
         ["8-5-2"] = 1, ["(12-4)-8"] = 0, ["3+7-1"] = 9, ["22+5-30"] = -3, ["(6-3)+4"] = 7,
         ["8-(2-6)"] = 12, ["(12-8)-(6-10)"] = 8, ["(30+40)-(75-40)"] = 35, ["3*5"] = 15, ["0*7"] = 0,
         ["(5+4)*10"] = 90, ["10*(5+4)"] = 90, ["2+4*6"] = 26, ["5*6+10"] = 40, ["10*(20-5)"] = 150,
         ["(20-10)*10"] = 100, ["5*(5-10)"] = -25, ["10*2-10"] = 10, ["20-10*2"] = 0, ["10/2"] = 5,
         ["10+10/2"] = 15, ["10-10/2"] = 5, ["-10/2"] = -5, ["5+10+10/2+5*2"] = 30, ["20-10-4/2"] = 8,
         ["30-10-5*2-10/5"] = 8, ["(10-5)*2+(2+4)/2"] = 13, ["10^2"] = 100, ["10^-2"] = .01,
         ["10+10^2"] = 110, ["-10+10^2"] = 90, ["5*2+10/2+10^-2"] = 15.01, ["10^(1+2)"] = 1000,
         // Unary operators
         ["---5"] = -5, ["-5+10"] = 5, ["-2-4"] = -6, ["---4+5--2+3"] = 6, ["(2+3)*-4"] = -20,
         ["(2+3)*+5"] = 25, ["-4*(3+5)"] = -32, ["-4+5--8"] = 9, ["-4+5-(-8)"] = 9, ["10^(-4+2)"] = .01,
         ["-10-10^2"] = -110, ["---4+5--6-2"] = 5,
         // Functional operator
         ["sin45"] = 0.70710678, ["sin-45"] = -0.70710678, ["-sin45"] = -0.70710678, ["cos45"] = 0.70710678, ["cos-45"] = 0.70710678,
         ["tan45"] = 1.0, ["sin(45+45)"] = 1.0, ["sin90*2"] = 2, ["sin--90"] = 1.0, ["log10"] = 2.302585,
         ["sqrt100"] = 10, ["sqrt-100"] = double.NaN, ["sqrt(10^2)"] = 10, ["sqrt100-10"] = 0,
         ["tan45+10-20"] = -9, ["asin1"] = 90, ["acos0"] = 90, ["log10+5"] = 7.302585, ["log(10+5)"] = 2.708050,
         ["log(-10+5)"] = double.NaN, ["sin90--1"] = 2, ["sin-90--1"] = 0, ["sqrt(90+10)"] = 10, ["sqrt(110-10)"] = 10,
         ["atan1"] = 45, ["asin-1"] = -90, ["atan-1"] = -45, ["atan-1+45"] = 0, ["exp1"] = 2.718281, ["exp1-2"] = .718281,
         ["exp(2-1)"] = 2.718281, ["exp-1"] = 0.367879,
         // Assignment operator
         ["a=5"] = 5, ["a+10"] = 15
      };
      foreach (var test in mTestData) {
         try {
            double result = mEval.Evaluate (test.Key);
            Console.ForegroundColor = (Math.Abs (result - test.Value) > 1e-6) ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine ($"'{test.Key}'={result}  ");
         } catch (Exception) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ($"'{test.Key}'=NAN  ");
         }
      }
      Console.ResetColor ();
      Console.WriteLine ("______Write your expression below_____\n");
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
