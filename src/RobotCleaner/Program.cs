using System;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var room = new OfficeRoom(-100_000, 100_000, -100_000, 100_000);

            var initialTile = new Tile(10, 22);

            var robot = room.CreateRobot(initialTile);

            robot.Move(Direction.East, 2);
            robot.Move(Direction.North, 1);

            var summary = robot.GetSummary();

            Console.WriteLine($"=> Cleaned: {summary.CleanedTiles}");
        }
    }
}
