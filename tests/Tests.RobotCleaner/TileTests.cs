using NUnit.Framework;
using RobotCleaner;

namespace Tests
{
    [TestFixture]
    public class TileTests
    {
        [Test, MyAutoData]
        public void Tiles_can_be_created(int x, int y)
        {
            var tile = new Tile(x, y);

            Assert.That(tile, Is.Not.Null);
            Assert.That(tile.X, Is.EqualTo(x));
            Assert.That(tile.Y, Is.EqualTo(y));
        }

        [Test, MyAutoData]
        public void Tiles_are_correctly_represented_as_string(Tile tile)
        {
            var str = tile.ToString();

            Assert.That(str, Contains.Substring($"({tile.X},{tile.Y})"));
        }
    }
}