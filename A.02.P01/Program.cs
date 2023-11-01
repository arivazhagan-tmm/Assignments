using static System.Console;

internal class Program {
   // Allows the user to predict the system generated number between 1 and 100.
   static void Main () {
      int n = new Random ().Next (1, 100);
      bool isValid;
      string[] guesses = { "1st", "2nd", "3rd", "4th", "5th", "6th", "last" },
               responses = { "too low", "correct..!", "too high" };
      WriteLine ("The number is chosen between 1 and 100. Find the number in 7 guesses!");
      for (int i = 0, count = 7, guess; i < count; i++) {
         var prompt = $"Your {guesses[i]} guess: ";
         WriteLine ();
         do {
            Write (prompt);
            isValid = int.TryParse (ReadLine (), out guess) && guess > 0;
            if (!isValid) {
               ForegroundColor = ConsoleColor.Red;
               Write ("\tPlease enter a valid whole number!");
               ResetColor ();
               WriteLine ();
            }
         } while (!isValid);
         var tmp = guess.CompareTo (n);
         Write ($"\t\tYour guess is {responses[tmp + 1]}!");
         WriteLine ();
         if (tmp is 0) break;
      }
   }
}