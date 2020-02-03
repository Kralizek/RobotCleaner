namespace RobotCleaner
{
    public struct Summary
    {
        public Summary(int cleanedTiles)
        {
            CleanedTiles = cleanedTiles;
        }

        public int CleanedTiles { get; }
    }
}
