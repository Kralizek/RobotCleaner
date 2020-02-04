namespace RobotCleaner
{
    public static class HelperExtensions
    {
        public static bool IsBetween(this int value, int min, int max) => value >= min && value <= max;

        public static int AsInt(this string str)
        {
            return int.Parse(str);
        }
    }
}
