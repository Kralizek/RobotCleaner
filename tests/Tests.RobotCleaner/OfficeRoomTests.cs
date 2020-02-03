using System;
using AutoFixture.NUnit3;
using NUnit.Framework;
using RobotCleaner;

namespace Tests
{
    [TestFixture]
    public class OfficeRoomTests
    {
        [Test, MyAutoData]
        public void Room_can_be_created_according_to_sample()
        {
            var room = new OfficeRoom(-100_000, 100_000, -100_000, 100_000);

            Assert.That(room, Is.Not.Null);
        }

        [Test, MyAutoData]
        public void Room_is_created_with_no_cleaned_tiles(OfficeRoom sut)
        {
            Assert.That(sut.CleanedTiles, Is.EqualTo(0));
        }

        [Test, MyAutoData]
        public void MinX_cant_be_equal_to_MaxX(int maxX, int minY, int maxY)
        {
            var minX = maxX;

            Assert.Throws<ArgumentException>(() => new OfficeRoom(maxX, maxX, minY, maxY));
        }

        [Test, MyAutoData]
        public void MinX_cant_be_greater_than_MaxX(int maxX, int minY, int maxY)
        {
            var minX = maxX + 1;

            Assert.Throws<ArgumentException>(() => new OfficeRoom(minX, maxX, minY, maxY));
        }

        [Test, MyAutoData]
        public void MinY_cant_be_equal_to_MaxY(int minX, int maxX, int maxY)
        {
            var minY = maxY;

            Assert.Throws<ArgumentException>(() => new OfficeRoom(minX, maxX, minY, maxY));
        }

        [Test, MyAutoData]
        public void MinY_cant_be_greater_than_MaxY(int minX, int maxX, int maxY)
        {
            var minY = maxY + 1;

            Assert.Throws<ArgumentException>(() => new OfficeRoom(minX, maxX, minY, maxY));
        }

        [Test, MyAutoData]
        public void CreateRobot_returns_new_robot_if_tile_is_valid([Frozen] int size, OfficeRoom sut)
        {
            var robot = sut.CreateRobot(new Tile(size - 1, size - 1));

            Assert.That(robot, Is.Not.Null);
        }

        [Test, MyAutoData]
        public void A_robot_can_be_created_on_the_boundary([Frozen] int size, OfficeRoom sut)
        {
            var robot = sut.CreateRobot(new Tile(size, size - 1));

            Assert.That(robot, Is.Not.Null);
        }

        [Test, MyAutoData]
        public void CreateRobot_throws_if_starting_tile_is_invalid([Frozen] int size, OfficeRoom sut)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.CreateRobot(new Tile(size + 1, size)));
        }

        [Test]
        [MyInlineAutoData(Direction.East, 1, 0)]
        [MyInlineAutoData(Direction.West, -1, 0)]
        [MyInlineAutoData(Direction.North, 0, 1)]
        [MyInlineAutoData(Direction.South, 0, -1)]
        public void MoveAndClean_returns_next_tile_if_valid(Direction direction, int expectedXdelta, int expectedYdelta, OfficeRoom sut)
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
        public void MoveAndClean_returns_same_tile_if_movement_not_valid(Direction direction, [Frozen] int size, OfficeRoom sut)
        {
            var currentPosition = GetPositionOnBoundary(direction, size);

            var newPosition = sut.MoveAndClean(currentPosition, direction);

            Assert.That(newPosition, Is.EqualTo(currentPosition));
        }

        [Test]
        [MyInlineAutoData(Direction.East)]
        [MyInlineAutoData(Direction.West)]
        [MyInlineAutoData(Direction.North)]
        [MyInlineAutoData(Direction.South)]
        public void MoveAndClean_adds_current_tile_to_cleaned_tiles(Direction direction, [Frozen] int size, OfficeRoom sut)
        {
            var currentPosition = GetPositionOnBoundary(direction, size);

            Assume.That(sut.CleanedTiles, Is.EqualTo(0));

            sut.MoveAndClean(currentPosition, direction);

            Assert.That(sut.CleanedTiles, Is.EqualTo(1));
        }

        [Test]
        [MyInlineAutoData(Direction.East)]
        [MyInlineAutoData(Direction.West)]
        [MyInlineAutoData(Direction.North)]
        [MyInlineAutoData(Direction.South)]
        public void MoveAndClean_adds_new_tile_to_cleaned_tiles(Direction direction, OfficeRoom sut)
        {
            var currentPosition = new Tile(0,0);

            Assume.That(sut.CleanedTiles, Is.EqualTo(0));

            sut.MoveAndClean(currentPosition, direction);

            Assert.That(sut.CleanedTiles, Is.EqualTo(2));
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