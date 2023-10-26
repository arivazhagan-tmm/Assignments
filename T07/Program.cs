using System.Text;
using static System.Console;

OutputEncoding = Encoding.Unicode;
WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
var blackCoins = " ♜ │ ♞ │ ♝ │ ♛ │ ♚ │ ♝ │ ♞ │ ♜ │";
var blackPawns = " ♟ │ ♟ │ ♟ │ ♟ │ ♟ │ ♟ │ ♟ │ ♟ │";
var whiteCoins = " ♖ │ ♘ │ ♗ │ ♕ │ ♔ │ ♗ │ ♘ │ ♖ │";
var whitePawns = " ♙ │ ♙ │ ♙ │ ♙ │ ♙ │ ♙ │ ♙ │ ♙ │";
var connector = "├───┼───┼───┼───┼───┼───┼───┼───┤";
var verticalBar = "│";
for (int i = 0, size = 8, padLength = 4; i < size; i++) {
   Write (verticalBar);
   Action todo = i switch {
      0 => () => Write (blackCoins),
      1 => () => Write (blackPawns),
      6 => () => Write (whitePawns),
      7 => () => Write (whiteCoins),
      _ => () => { for (int j = 0; j < size; j++) Write (verticalBar.PadLeft (padLength)); }
   };
   todo ();
   if (i == size - 1) break;
   WriteLine ();
   WriteLine (connector);
}
WriteLine ();
WriteLine ("└───┴───┴───┴───┴───┴───┴───┴───┘");
