using System;
using NUnit.Framework;
using RobotCleaner;

namespace Tests
{
    [TestFixture]
    public class HelperExtensionsTests
    {
        [Test, MyAutoData]
        public void AsInt_returns_integer_in_string(int value)
        {
            var testString = $"{value}";

            var result = HelperExtensions.AsInt(testString);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test, MyAutoData]
        public void AsInt_throws_if_invalid_string(string testString)
        {
            Assert.Throws<FormatException>(() => HelperExtensions.AsInt(testString));
        }

        [Test, MyAutoData]
        public void IsBetween_returns_false_if_number_is_less_than_lower_boundary(int lowerBoundary, int delta)
        {
            var testValue = lowerBoundary - delta;

            var higherBoundary = lowerBoundary + delta;

            var result = HelperExtensions.IsBetween(testValue, lowerBoundary, higherBoundary);

            Assert.That(result, Is.False);
        }

        [Test, MyAutoData]
        public void IsBetween_returns_false_if_number_is_higher_than_higher_boundary(int higherBoundary, int delta)
        {
            var testValue = higherBoundary + delta;

            var lowerBoundary = higherBoundary - delta;

            var result = HelperExtensions.IsBetween(testValue, lowerBoundary, higherBoundary);

            Assert.That(result, Is.False);
        }

        [Test, MyAutoData]
        public void IsBetween_returns_true_if_number_is_between_boundaries(int testValue, int delta)
        {
            int lowerBoundary = testValue - delta;

            int higherBoundary = testValue + delta;

            var result = HelperExtensions.IsBetween(testValue, lowerBoundary, higherBoundary);

            Assert.That(result, Is.True);
        }

        [Test, MyAutoData]
        public void IsBetween_returns_true_if_number_equals_lower_boundary(int lowerBoundary, int delta)
        {
            var testValue = lowerBoundary;

            var higherBoundary = lowerBoundary + delta;

            var result = HelperExtensions.IsBetween(testValue, lowerBoundary, higherBoundary);

            Assert.That(result, Is.True);
        }

        [Test, MyAutoData]
        public void IsBetween_returns_true_if_number_equals_higher_boundary(int higherBoundary, int delta)
        {
            var testValue = higherBoundary;

            var lowerBoundary = higherBoundary - delta;

            var result = HelperExtensions.IsBetween(testValue, lowerBoundary, higherBoundary);

            Assert.That(result, Is.True);
        }
    }
}