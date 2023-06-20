namespace StageObject
{
    public enum Size
    {
        TooSmall,
        Small,
        Midium,
        Big,
        TooBig,
    }

    public static class SizeExtension
    {
        public static bool IsCatchable(this Size size)
        {
            switch(size)
            {
                case Size.TooSmall:
                    return false;
                case Size.Small:
                    return true;
                case Size.Midium:
                    return true;
                case Size.Big:
                    return true;
                case Size.TooBig:
                    return false;
                default:
                    return false;
            }
        }
    }
}