using System.Text;
using static System.Console;
using static System.String;

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
   var s = i switch {
      0 => blackCoins,
      1 => blackPawns,
      6 => whitePawns,
      7 => whiteCoins,
      < 6 => Concat (Enumerable.Repeat (verticalBar.PadLeft (padLength), size)),
      _ => Empty
   };
   Write (s);
   WriteLine ();
   if (i == size - 1) break;
   WriteLine (connector);
}
WriteLine ("└───┴───┴───┴───┴───┴───┴───┴───┘");
