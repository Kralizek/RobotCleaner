using NUnit.Framework;
using AutoFixture.Idioms;
using RobotCleaner;
using AutoFixture.NUnit3;
using Moq;

namespace Tests
{
    [TestFixture]
    public class RobotTests
    {
        [Test, MyAutoData]
        public void Constructor_is_guarded(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Robot).GetConstructors());
        }

        [Test, MyAutoData]
        public void Move_forwards_call_to_room([Frozen] ICleanableRoom room, Robot sut, Direction direction, int distance)
        {
            sut.Move(direction, distance);

            Mock.Get(room).Verify(p => p.MoveAndClean(It.IsAny<Tile>(), direction), Times.Exactly(distance));
        }

        [Test, MyAutoData]
        public void Move_can_handle_zero_distance([Frozen] ICleanableRoom room, Robot sut, Direction direction)
        {
            sut.Move(direction, 0);

            Mock.Get(room).Verify(p => p.MoveAndClean(It.IsAny<Tile>(), direction), Times.Never());
        }

        [Test, MyAutoData]
        public void GetSummary_uses_rooms_CleanedTiles([Frozen] ICleanableRoom room, Robot sut)
        {
            var summary = sut.GetSummary();

            Assert.That(summary.CleanedTiles, Is.EqualTo(room.CleanedTiles));
        }
    }
}