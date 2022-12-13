namespace Anvyl.Controls.Extensions
{
    public static class DoubleExtensions
    {
        private const double degreeToRadians = Math.PI / 180;
        private const double radiansToDegrees = 180 / Math.PI;

        public static double ToRadians(this double value)
        {
            return value * degreeToRadians;
        }

        public static double ToDegrees(this double value)
        {
            return value * radiansToDegrees;
        }
    }
}
