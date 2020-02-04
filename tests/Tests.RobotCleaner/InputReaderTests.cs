using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using RobotCleaner;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Tests
{
    [TestFixture]
    public class InputReaderTests
    {
        [Test, MyAutoData]
        public void Constructor_is_guarded(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(InputReader).GetConstructors());
        }

        [Test, MyAutoData]
        public void InputReader_can_be_created([Frozen] IConsoleReader reader, InputReader sut)
        {
            Assert.That(sut, Is.Not.Null);
        }

        [Test, MyAutoData]
        public void TryReadInputs_can_process_sample([Frozen] IConsoleReader reader, InputReader sut)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("2")
                            .Returns("10 22")
                            .Returns("E 2")
                            .Returns("N 1");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.True);

            Assert.That(output!.InitialTile.X, Is.EqualTo(10));
            Assert.That(output.InitialTile.Y, Is.EqualTo(22));

            Assert.That(output.Instructions, Has.Exactly(2).InstanceOf<Instruction>());

            Assert.That(output.Instructions[0].Direction, Is.EqualTo(Direction.East));
            Assert.That(output.Instructions[0].Distance, Is.EqualTo(2));

            Assert.That(output.Instructions[1].Direction, Is.EqualTo(Direction.North));
            Assert.That(output.Instructions[1].Distance, Is.EqualTo(1));
        }

        [Test, MyAutoData]
        public void TryReadInputs_returns_false_if_tile_position_is_invalid([Frozen] IConsoleReader reader, InputReader sut, string invalidTilePosition)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("0")
                            .Returns(invalidTilePosition);

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.False);
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_returns_false_if_tile_position_is_out_of_west_boundary([Frozen] IConsoleReader reader, InputReader sut)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("0")
                            .Returns($"{InputReader.MinX - 1} {0}");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.False);
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_returns_false_if_tile_position_is_out_of_east_boundary([Frozen] IConsoleReader reader, InputReader sut)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("0")
                            .Returns($"{InputReader.MaxX + 1} {0}");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.False);
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_returns_false_if_tile_position_is_out_of_south_boundary([Frozen] IConsoleReader reader, InputReader sut)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("0")
                            .Returns($"{0} {InputReader.MinY - 1}");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.False);
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_returns_false_if_tile_position_is_out_of_north_boundary([Frozen] IConsoleReader reader, InputReader sut)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("0")
                            .Returns($"{0} {InputReader.MaxY + 1}");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.False);
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_ignores_invalid_instructions([Frozen] IConsoleReader reader, InputReader sut, [Range(-100, 100)] int initialX, [Range(-100, 100)] int initialY, string invalidInstruction)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns("1")
                            .Returns($"{initialX} {initialY}")
                            .Returns(invalidInstruction);

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.True);

            Assert.That(output!.Instructions, Has.None.InstanceOf<Instruction>());
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_ignores_instructions_with_distance_greater_than_max_allowed([Frozen] IConsoleReader reader, InputReader sut, [Range(-100, 100)] int initialX, [Range(-100, 100)] int initialY, Direction direction, int distance)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                .Returns("1")
                .Returns($"{initialX} {initialY}")
                .Returns($"{direction.ToString()[0]} {distance + InputReader.MaxDistance}");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.True);

            Assert.That(output!.Instructions, Has.None.InstanceOf<Instruction>());
        }

        [Test, MyInlineAutoData]
        public void TryReadInputs_returns_false_if_GTE_max_number_of_allowed_instructions_is_entered([Frozen] IConsoleReader reader, InputReader sut, [Range(-100, 100)] int initialX, [Range(-100, 100)] int initialY)
        {
            Mock.Get(reader).SetupSequence(p => p.ReadLine())
                            .Returns($"{InputReader.MaxInstructions}")
                            .Returns($"{initialX} {initialY}");

            var result = sut.TryReadInputs(out var output);

            Assert.That(result, Is.False);
        }
    }
}