using System.Collections.Generic;

namespace RobotCleaner
{
    public interface ICleanableRoom
    {
        Tile MoveAndClean(Tile currentPosition, Direction direction);

        IReadOnlyCollection<Tile> CleanedTiles { get; }
    }

    public class OfficeRoom : ICleanableRoom
    {
        private readonly HashSet<Tile> _cleanedTiles = new HashSet<Tile>();

        public Tile MoveAndClean(Tile currentPosition, Direction direction)
        {
            _cleanedTiles.Add(currentPosition);

            var newPosition = GetNewPosition(currentPosition, direction);

            _cleanedTiles.Add(newPosition);

            return newPosition;
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

            return new Tile(newPosition.x, newPosition.y);
        }

        public IReadOnlyCollection<Tile> CleanedTiles => _cleanedTiles;
    }
}
