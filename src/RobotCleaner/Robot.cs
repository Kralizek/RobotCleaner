using System;
using System.Diagnostics;

namespace RobotCleaner
{
    public class Robot
    {
        private readonly ICleanableRoom _room;
        private Tile _currentTile;

        public Robot(ICleanableRoom room, Tile initialTile)
        {
            _room = room ?? throw new ArgumentNullException(nameof(room));
            _currentTile = initialTile;
        }

        public void Move(Direction direction, int distance)
        {
            Tile newTile;

            for (int i = 0; i < distance; i++)
            {
                newTile = _room.MoveAndClean(_currentTile, direction);
#if DEBUG
                Debug.WriteLine($"Moved {direction} from {_currentTile} to {newTile}");
#endif
                _currentTile = newTile;
            }
        }

        public Summary GetSummary()
        {
            return new Summary(_room.CleanedTiles.Count);
        }
    }
}
