using static System.Console;

while (true) {
   GetResponse ("Enter the money: ", 0, out int money);
   GetResponse ("Enter the price: ", 0, out int price);
   GetResponse ("Enter number of exchange wrappers: ", 1, out int exchange);
   var (chocos, wraps) = GetChocolates (money, price, exchange);
   Write ($"\nNo of chocolates: {chocos}\nRemaining money: {money % price}\nRemaining wrappers: {wraps}\n\n");
   Write ("Do you want to continue? (y/n): ");
   if (ReadKey ().Key is not ConsoleKey.Y) break;
   WriteLine ("\n");
}

// Computes and returns number of chocolates and remaining wrappers for given money.
static (int chocos, int wraps) GetChocolates (int money, int price, int exchange) {
   int chocos = money / price; // Number of chocolates on the first buy.
   int wraps = chocos; // Wrappers from bought chocolates.
   while (wraps >= exchange) {
      int newChocos = wraps / exchange; // Chocolates from exchange of wrappers.
      chocos += newChocos;
      wraps %= exchange;
      wraps += newChocos;
   }
   return (chocos, wraps);
}

// Getting the valid response from the user for the given prompt.
static void GetResponse (string prompt, int min, out int number) {
   Write (prompt);
   while (true) {
      if (int.TryParse (ReadLine (), out number) && number > min) break;
      ForegroundColor = ConsoleColor.Red;
      WriteLine ($"The value should be greater than {min}.\n");
      ResetColor ();
      Write (prompt);
   }
}

