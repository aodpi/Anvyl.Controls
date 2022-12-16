namespace Anvyl.Controls.Extensions
{
    internal static class DoubleExtensions
    {
        private const double degreeToRadians = Math.PI / 180;
        private const double radiansToDegrees = 180 / Math.PI;

        public static double ToRadians(this ref double value)
        {
            return value * degreeToRadians;
        }

        public static double ToDegrees(this ref double value)
        {
            return value * radiansToDegrees;
        }
    }
}
