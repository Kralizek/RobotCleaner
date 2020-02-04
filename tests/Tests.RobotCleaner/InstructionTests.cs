using NUnit.Framework;
using RobotCleaner;

namespace Tests
{
    [TestFixture]
    public class InstructionTests
    {
        [Test, MyAutoData]
        public void TryParse_can_parse_well_structured_strings(Direction direction, int distance)
        {
            var testString = $"{direction.ToString()[0]} {distance}";

            Assume.That(testString, Does.Match(Instruction.Pattern.ToString()));

            var result = Instruction.TryParse(testString, out var output);

            Assert.That(result, Is.True);
            Assert.That(output.Direction, Is.EqualTo(direction));
            Assert.That(output.Distance, Is.EqualTo(distance));
        }

        [Test, MyAutoData]
        public void TryParse_refuses_negative_numbers(Direction direction, int distance)
        {
            var testString = $"{direction.ToString()[0]} -{distance}";

            var result = Instruction.TryParse(testString, out var output);

            Assert.That(result, Is.False);
        }

        [Test, MyAutoData]
        public void TryParse_refuses_strings_not_matching_the_pattern(string testString)
        {
            Assume.That(testString, Does.Not.Match(Instruction.Pattern.ToString()));

            var result = Instruction.TryParse(testString, out var output);

            Assert.That(result, Is.False);
        }
    }
}