using static System.Console;

while (true) {
   GetResponse ("Enter the number: ", out int n);
   var result = IsArmstrong (n) ? "Armstrong" : "Not Armstrong";
   WriteLine ($"{n} is {result} number.");
   WriteLine ();
   Write ("Do you want to continue? (Y/N): ");
   if (ReadLine ()?.ToLower () is not "y") break;
   WriteLine ();
}

// Getting the valid numeric response from the user for the given prompt.
static void GetResponse (string prompt, out int n) {
   Write (prompt);
   while (!int.TryParse (ReadLine (), out n) || n <= 0) {
      ForegroundColor = ConsoleColor.Red;
      WriteLine ("\tPlease enter any whole number which is greater than 0.");
      ResetColor ();
      Write (prompt);
   }
}

// Checks whether the given number is armstrong or not.
static bool IsArmstrong (int number) {
   var (str, res) = (number.ToString (), 0.0);
   var len = str.Length;
   foreach (var ch in str) res += Math.Pow (double.Parse (ch.ToString ()), len);
   return res == number;
}

