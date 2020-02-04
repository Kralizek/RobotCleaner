using System;
using System.Collections.Generic;

namespace RobotCleaner
{
    public class Inputs
    {
        public Inputs(Tile initialTile, IReadOnlyList<Instruction> instructions)
        {
            InitialTile = initialTile;
            Instructions = instructions ?? Array.Empty<Instruction>();
        }

        public Tile InitialTile { get; }
        public IReadOnlyList<Instruction> Instructions { get; }
    }
}
