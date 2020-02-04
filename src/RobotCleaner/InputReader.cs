using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace RobotCleaner
{
    public class InputReader
    {
        private readonly IConsoleReader _reader;

        public InputReader(IConsoleReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public bool TryReadInputs([NotNullWhen(true)] out Inputs? inputs)
        {
            inputs = null;

            int expectedInstructions = _reader.ReadLine().AsInt();

            var initialTileCoordinates = _reader.ReadLine();

            var instructions = new List<Instruction>();

            for (int i = 1; i <= expectedInstructions; i++)
            {
                var str = _reader.ReadLine();

                if (Instruction.TryParse(str, out var instruction))
                {
                    instructions.Add(instruction);
                }
            }

            if (Tile.TryParse(initialTileCoordinates, out var initialTile) && initialTile.X.IsBetween(MinX, MaxX) && initialTile.Y.IsBetween(MinY, MaxY))
            {
                inputs = new Inputs(initialTile, instructions);
                return true;
            }

            return false;
        }

        public static int MinX = -100_000;
        public static int MinY = -100_000;
        public static int MaxX = 100_000;
        public static int MaxY = 100_000;
    }

    public interface IConsoleReader
    {
        string ReadLine();
    }

    public class ConsoleReader : IConsoleReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
