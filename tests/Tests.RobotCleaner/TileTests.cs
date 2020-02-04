using NUnit.Framework;
using RobotCleaner;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

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

        [Test, MyAutoData]
        public void Tiles_can_be_parsed_from_string([Range(-1_000_000, 1_000_000)] int x, [Range(-1_000_000, 1_000_000)] int y)
        {
            var testString = $"{x} {y}";

            var result = Tile.TryParse(testString, out var outputTile);

            Assert.That(result, Is.True);
            Assert.That(outputTile.X, Is.EqualTo(x));
            Assert.That(outputTile.Y, Is.EqualTo(y));
        }
        
        [Test, MyAutoData]
        public void TryParse_return_false_if_invalid_string(string testString)
        {
            var result = Tile.TryParse(testString, out var outputTile);

            Assert.That(result, Is.False);
        }
    }
}