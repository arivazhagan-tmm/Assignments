using static System.Console;

while (true) {
   GetResponse ("Enter the number: ", out int n);
   var res = IsArmstrong (n) ? "armstrong" : "not armstrong";
   WriteLine ($"{n} is {res} number.\n");
   Write ("Do you want to continue? (y/n): ");
   if (ReadKey ().Key is not ConsoleKey.Y) break;
   WriteLine ("\n");
}

// Getting the valid numeric response from the user for the given prompt.
static void GetResponse (string prompt, out int n) {
   Write (prompt);
   while (!int.TryParse (ReadLine (), out n) || n <= 0) {
      ForegroundColor = ConsoleColor.Red;
      WriteLine ("\tPlease enter any whole number that is greater than 0");
      ResetColor ();
      Write (prompt);
   }
}

// Checks whether the given number is armstrong or not.
static bool IsArmstrong (int n) {
   var (str, res) = (n.ToString (), 0.0);
   var len = str.Length;
   foreach (var ch in str) res += Math.Pow (double.Parse (ch.ToString ()), len);
   return res == n;
}

