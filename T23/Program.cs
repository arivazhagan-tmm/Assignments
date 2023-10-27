using static System.Console;

Write ("\tSample test case");
WriteLine ();
var polls = "AabBBcd";
var (winner, vote) = GetWinner (polls);
PrintResult (polls, winner, vote);

while (true) {
   WriteLine ();
   var prompt = "Poll your vote to find the winner: ";
   bool validPoll;
   do {
      Write (prompt);
      polls = ReadLine () ?? "";
      validPoll = polls.Length != 0 && polls.All (char.IsLetter);
      if (!validPoll) {
         ForegroundColor = ConsoleColor.Red;
         Write ("\tVotes are alphabets and minimum 2 votes to be polled.");
         WriteLine ();
         ResetColor ();
      }
   } while (!validPoll);
   (winner, vote) = GetWinner (polls);
   PrintResult (polls, winner, vote);
   Write ("Do you want to continue? (Y/N): ");
   if (ReadLine ()?.ToLower () is not "y") break;
   WriteLine ();
}

// Prints the polled votes, winner and max vote.
static void PrintResult (string polls, char winner, int vote) {
   WriteLine ();
   WriteLine ($"\tPolls\t: {polls}\n\tWinner\t: {char.ToUpper (winner)}\n\tVotes\t: {vote}");
   WriteLine ();
}

// Finds and returns the winner along with their votes.
static (char, int) GetWinner (string polls) {
   var freqPairs = new Dictionary<char, int> ();
   foreach (char ch in polls.ToLower ())
      freqPairs[ch] = freqPairs.TryGetValue (ch, out int value) ? ++value : 1;
   var frequencies = freqPairs.Values.ToList ();
   var winner = freqPairs.ElementAt (frequencies.IndexOf (frequencies.Max ()));
   return (winner.Key, winner.Value);
}