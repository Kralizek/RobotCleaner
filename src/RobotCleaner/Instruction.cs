using System.Text.RegularExpressions;

namespace RobotCleaner
{
    public struct Instruction
    {
        public Instruction(Direction direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public Direction Direction { get; }
        public int Distance { get; }

        public static Regex Pattern = new Regex(@"^(?<direction>[EWNS]) (?<distance>\d+)$");

        public static bool TryParse(string? str, out Instruction instruction)
        {
            var match = Pattern.Match(str);

            if (match.Success)
            {
                Direction direction = match.Groups["direction"].Value switch
                {
                    "E" => Direction.East,
                    "W" => Direction.West,
                    "N" => Direction.North,
                    "S" => Direction.South,
                    _ => 0
                };

                int distance = match.Groups["distance"].Value.AsInt();

                instruction = new Instruction(direction, distance);
                return true;
            }

            instruction = default;
            return false;
        }
    }
}
