using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using RobotCleaner;

namespace Tests
{
    public static class FixtureHelper
    {
        public static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true,
                GenerateDelegates = true 
            });

            return fixture;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MyAutoData : AutoDataAttribute
    {
        public MyAutoData() : base (FixtureHelper.CreateFixture) {}
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class MyInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public MyInlineAutoDataAttribute(params object[] arguments) : base(FixtureHelper.CreateFixture, arguments) { }
    }
}