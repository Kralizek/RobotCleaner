using System;
using System.Text.RegularExpressions;

namespace RobotCleaner
{
    public struct Tile
    {
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public override string ToString() => $"({X},{Y})";

        private static Regex Pattern = new Regex(@"^(?<x>-?\d+) (?<y>-?\d+)$", RegexOptions.Compiled);

        public static bool TryParse(string? str, out Tile tile)
        {
            var match = Pattern.Match(str);

            if (match.Success)
            {
                var x = match.Groups["x"].Value.AsInt();
                var y = match.Groups["y"].Value.AsInt();

                tile = new Tile(x, y);
                return true;
            }

            tile = default;
            return false;
        }
    }
}
