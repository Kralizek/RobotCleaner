using System;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new ConsoleReader();

            var inputReader = new InputReader(reader);

            int result = 0;

            if (inputReader.TryReadInputs(out var inputs))
            {
                result = CleanRoom(inputs);
            }

            Console.WriteLine($"=> Cleaned: {result}");
        }

        private static int CleanRoom(Inputs? inputs)
        {
            if (inputs != null)
            {
                var room = new OfficeRoom();

                var robot = new Robot(room, inputs.InitialTile);

                foreach (var instruction in inputs.Instructions)
                {
                    robot.Move(instruction.Direction, instruction.Distance);
                }

                var summary = robot.GetSummary();

                return summary.CleanedTiles;
            }

            return 0;
        }
    }
}
