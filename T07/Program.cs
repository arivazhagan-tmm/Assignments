using System.Text;
using static System.Console;

OutputEncoding = Encoding.Unicode;
WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
var blackCoins = " ♜ │ ♞ │ ♝ │ ♛ │ ♚ │ ♝ │ ♞ │ ♜ │";
var blackPawns = " ♟ │ ♟ │ ♟ │ ♟ │ ♟ │ ♟ │ ♟ │ ♟ │";
var whiteCoins = " ♖ │ ♘ │ ♗ │ ♕ │ ♔ │ ♗ │ ♘ │ ♖ │";
var whitePawns = " ♙ │ ♙ │ ♙ │ ♙ │ ♙ │ ♙ │ ♙ │ ♙ │";
var connector = "\n├───┼───┼───┼───┼───┼───┼───┼───┤";
var verticalBar = "│";
for (int i = 0, size = 8; i < size; i++) {
   Write (verticalBar);
   if (i is 0) Write (blackCoins);
   else if (i is 1) Write (blackPawns);
   else if (i == size - 2) Write (whitePawns);
   else if (i == size - 1) Write (whiteCoins);
   else
      for (int j = 0; j < size; j++) Write (verticalBar.PadLeft (4));
   if (i == size - 1) break;
   WriteLine (connector);
}
WriteLine ("\n└───┴───┴───┴───┴───┴───┴───┴───┘\n\n");