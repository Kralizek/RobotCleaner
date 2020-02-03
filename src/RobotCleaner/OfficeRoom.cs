using System;
using System.Collections.Generic;

namespace RobotCleaner
{
    public interface ICleanableRoom
    {
        Tile MoveAndClean(Tile currentPosition, Direction direction);

        int CleanedTiles { get; }
    }

    public class OfficeRoom : ICleanableRoom
    {
        private readonly (int min, int max) _x;
        private readonly (int min, int max) _y;

        private readonly HashSet<Tile> _cleanedTiles = new HashSet<Tile>();

        public OfficeRoom(int minX, int maxX, int minY, int maxY)
        {
            if (minX >= maxX)
            {
                throw new ArgumentException("MinX must be less than MaxX", nameof(minX));
            }

            if (minY >= maxY)
            {
                throw new ArgumentException("MinY must be less than MaxY", nameof(maxY));
            }

            _x = (minX, maxX);
            _y = (minY, maxY);
        }

        public Robot CreateRobot(Tile initialTile)
        {
            if (IsPositionValid(initialTile.X, initialTile.Y))
            {
                return new Robot(this, initialTile);
            }

            throw new ArgumentOutOfRangeException(nameof(initialTile), "The given tile isn't valid for this room");
        }

        public Tile MoveAndClean(Tile currentPosition, Direction direction)
        {
            if (IsPositionValid(currentPosition.X, currentPosition.Y))
            {
                _cleanedTiles.Add(currentPosition);

                var newPosition = GetNewPosition(currentPosition, direction);

                _cleanedTiles.Add(newPosition);

                return newPosition;
            }

            return currentPosition;
        }

        private Tile GetNewPosition(Tile currentPosition, Direction direction)
        {
            (int x, int y) newPosition = direction switch
            {
                Direction.East => (currentPosition.X + 1, currentPosition.Y),
                Direction.West => (currentPosition.X - 1, currentPosition.Y),
                Direction.North => (currentPosition.X, currentPosition.Y + 1),
                Direction.South => (currentPosition.X, currentPosition.Y - 1),
                _ => (currentPosition.X, currentPosition.Y)
            };

            if (!IsPositionValid(newPosition.x, newPosition.y))
            {
                return currentPosition;
            }

            return new Tile(newPosition.x, newPosition.y);
        }

        private bool IsPositionValid(int x, int y) => y >= _y.min && y <= _y.max && x >= _x.min && x <= _x.max;

        public int CleanedTiles => _cleanedTiles.Count;
    }
}
