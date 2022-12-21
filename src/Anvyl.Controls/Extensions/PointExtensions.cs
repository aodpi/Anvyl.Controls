using System.Diagnostics;
using System.Numerics;

namespace Anvyl.Controls.Extensions
{
    internal static class PointExtensions
    {
        /// <summary>
        /// Calculate the distance between current and specified point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double Distance(this Point point, Point point2)
        {
            var dx = point.X - point2.X;
            var dy = point.Y - point2.Y;
            var ls = dx * dx + dy * dy;

            return Math.Sqrt(ls);
        }

        /// <summary>
        /// Distance squared between two points.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double DistanceSquared(this Point point, Point point2)
        {
            var dx = point.X - point2.X;
            var dy = point.Y - point2.Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Normalize a point so it hase length of 1.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point Normalize(this Point point)
        {
            var length = point.Length();
            var x = point.X / length;
            var y = point.Y / length;

            return new Point(x, y);
        }

        /// <summary>
        /// Rotate a point by the specified angle (in degrees) clockwise
        /// relative to origin (0,0)
        /// </summary>
        /// <param name="point">Point to rotate</param>
        /// <param name="angle">Degrees to rotate</param>
        /// <returns></returns>
        public static Point Rotate(this Point point, double angle)
        {
            return Rotate(point, angle, new Point());
        }


        /// <summary>
        /// Find the reflected point based on another point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="other">Reference point</param>
        /// <returns></returns>
        public static Point Reflect(this Point point, Point other)
        {
            var normal = other.Normalize();

            var dot = point.X * normal.X + point.Y * normal.Y;
            var x = 2 * dot * normal.X - point.X;
            var y = 2 * dot * normal.Y - point.Y;

            return new Point(x, y);
        }

        /// <summary>
        /// Rotate a point by the specified angle (in degrees) clockwise relative 
        /// to the provided origin.
        /// </summary>
        /// <param name="point">Point to rotate</param>
        /// <param name="angle">Degrees to rotate</param>
        /// <param name="origin">Origin of rotation</param>
        /// <returns></returns>
        public static Point Rotate(this Point point, double angle, Point origin)
        {
            Debug.Assert(angle != double.NaN);

            var angleInRadians = angle.ToRadians();

            float cos = (float)Math.Cos(angleInRadians);
            float sin = (float)Math.Sin(angleInRadians);

            var x = (point.X - origin.X) * cos - (point.Y - origin.Y) * sin + origin.X;
            var y = (point.X - origin.X) * sin + (point.Y - origin.Y) * cos + origin.Y;

            return new Point(x, y);
        }

        /// <summary>
        /// Lenght of this point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double Length(this Point point)
        {
            return Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }

        /// <summary>
        /// Length squared of this point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double LengthSquared(this Point point)
        {
            return point.X * point.X + point.Y * point.Y;
        }

        /// <summary>
        /// Calculate the angle in degrees between current and specified point.
        /// </summary>
        /// <param name="point1">Curreent point</param>
        /// <param name="point2">Other point</param>
        /// <returns></returns>
        public static double AngleBetween(this Point point1, Point point2)
        {
            double sin = point1.X * point2.Y - point2.X * point1.Y;
            double cos = point1.X * point2.X + point2.Y * point1.X;
            double tan = Math.Atan2(sin, cos);

            return tan.ToDegrees();
        }

        public static Point Project(this Point point, Point point2)
        {
            var dotProduct = point.X * point2.X + point.Y * point2.Y;
            var dotB = point2.X * point2.X + (point2.Y * point2.Y);

            var x = (dotProduct / dotB) * point2.X;
            var y = (dotProduct / dotB) * point2.Y;

            return new Point(x, y);
        }

        public static Point Lerp(this Point point, Point other, double amount)
        {
            return new Point(
                point.X + (other.X - point.X) * amount,
                point.Y + (other.Y - point.Y) * amount);
        }

        public static Point MidPoint(this Point point, Point other)
        {
            return new Point(
                (point.X + other.X) / 2.0,
                (point.Y + other.Y) / 2.0);
        }
    }
}
