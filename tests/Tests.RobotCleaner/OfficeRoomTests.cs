using System;
using System.Collections.Generic;
using AutoFixture.NUnit3;
using NUnit.Framework;
using RobotCleaner;

namespace Tests
{
    [TestFixture]
    public class OfficeRoomTests
    {
        [Test, MyAutoData]
        public void Room_is_created_with_no_cleaned_tiles(OfficeRoom sut)
        {
            Assert.That(sut.CleanedTiles, Is.Empty);
        }

        [Test]
        [MyInlineAutoData(Direction.East, 1, 0)]
        [MyInlineAutoData(Direction.West, -1, 0)]
        [MyInlineAutoData(Direction.North, 0, 1)]
        [MyInlineAutoData(Direction.South, 0, -1)]
        public void MoveAndClean_returns_next_tile(Direction direction, int expectedXdelta, int expectedYdelta, OfficeRoom sut)
        {
            var currentPosition = new Tile(0, 0);

            var newPosition = sut.MoveAndClean(currentPosition, direction);

            Assert.That(newPosition.X, Is.EqualTo(currentPosition.X + expectedXdelta));
            Assert.That(newPosition.Y, Is.EqualTo(currentPosition.Y + expectedYdelta));
        }

        [Test]
        [MyInlineAutoData(Direction.East)]
        [MyInlineAutoData(Direction.West)]
        [MyInlineAutoData(Direction.North)]
        [MyInlineAutoData(Direction.South)]
        public void MoveAndClean_adds_current_tile_to_cleaned_tiles(Direction direction, [Frozen] int size, OfficeRoom sut)
        {
            var currentPosition = GetPositionOnBoundary(direction, size);

            Assume.That(sut.CleanedTiles, Is.Empty);

            sut.MoveAndClean(currentPosition, direction);

            Assert.That(sut.CleanedTiles, Contains.Item(currentPosition));
        }

        [Test]
        [MyInlineAutoData(Direction.East)]
        [MyInlineAutoData(Direction.West)]
        [MyInlineAutoData(Direction.North)]
        [MyInlineAutoData(Direction.South)]
        public void MoveAndClean_adds_new_tile_to_cleaned_tiles(Direction direction, OfficeRoom sut)
        {
            var currentPosition = new Tile(0,0);

            Assume.That(sut.CleanedTiles, Is.Empty);

            var newPosition = sut.MoveAndClean(currentPosition, direction);

            Assert.That(sut.CleanedTiles, Contains.Item(newPosition));
        }

        Tile GetPositionOnBoundary(Direction direction, int size) => direction switch
        {
            Direction.East => new Tile(size, 0),
            Direction.West => new Tile(-size, 0),
            Direction.North => new Tile(0, size),
            Direction.South => new Tile(0, -size),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };
    }
}