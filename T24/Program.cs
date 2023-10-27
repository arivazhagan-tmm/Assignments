using static System.Console;

while (true) {
   GetResponse ("Enter the money\t: ", 0, out int money);
   GetResponse ("Enter the price\t: ", 0, out int price);
   GetResponse ("Enter number of exchange wrappers: ", 1, out int exchange);
   Compute (money, price, ref exchange, out int chocolates);
   WriteLine ();
   Write ($"\tNo of chocolates: {chocolates}\n\tRemaining money : {money % price}\n\tRemaining wrappers : {exchange}\n");
   WriteLine ();
   Write ("Do you want to continue? (Y/N): ");
   if (ReadLine ()?.ToLower () is not "y") break;
   WriteLine ();
}

// Computes number of chocolates and the remaining wrappers for the given money
static void Compute (int money, int price, ref int exchange, out int chocos) {
   chocos = money / price; // Number of chocolates on the first buy.
   int wraps = chocos; // Wrappers from bought chocolates.
   while (wraps >= exchange) {
      int newChocos = wraps / exchange; // Chocolates from exchange of wrappers.
      chocos += newChocos;
      wraps %= exchange;
      wraps += newChocos;
   }
   exchange = wraps; // Remaining wrappers which can't be exchanged.
}

// Getting valid response from the user for the given prompt.
static void GetResponse (string prompt, int limit, out int number) {
   string? response = "";
   number = 0;
   do {
      if (response != "" && number <= limit) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ($"\tThe number should be greater than {limit}.");
         ResetColor ();
      }
      Write (prompt);
      response = ReadLine ();
   } while (!int.TryParse (response, out number) || number <= limit);
}

