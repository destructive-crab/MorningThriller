namespace destructive_code.LevelGeneration
{
    public static class DirectionHelper
    {
        public static Direction GetOpposite(Direction direction)
        {
            if (direction == Direction.Bottom)
                return Direction.Top;
            if (direction == Direction.Top)
                return Direction.Bottom;
            if (direction == Direction.Right)
                return Direction.Left;
            if (direction == Direction.Left)
                return Direction.Right;
            
            return Direction.Bottom;
        }
    }
}