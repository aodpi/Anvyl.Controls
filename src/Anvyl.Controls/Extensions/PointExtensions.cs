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
        public static double Distance(this ref Point point, Point point2)
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
        public static double DistanceSquared(this ref Point point, Point point2)
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
        public static Point Normalize(this ref Point point)
        {
            var ls = Math.Max(Math.Abs(point.X), Math.Abs(point.Y));
            var x = point.X / ls;
            var y = point.Y / ls;
            var len = point.Length();

            return new Point(x / len, y / len);
        }

        /// <summary>
        /// Rotate a point by the specified angle (in degrees) clockwise
        /// relative to origin (0,0)
        /// </summary>
        /// <param name="point">Point to rotate</param>
        /// <param name="angle">Degrees to rotate</param>
        /// <returns></returns>
        public static Point Rotate(this ref Point point, double angle)
        {
            return Rotate(ref point, angle, new Point());
        }

        /// <summary>
        /// Rotate a point by the specified angle (in degrees) clockwise relative 
        /// to the provided origin.
        /// </summary>
        /// <param name="point">Point to rotate</param>
        /// <param name="angle">Degrees to rotate</param>
        /// <param name="origin">Origin of rotation</param>
        /// <returns></returns>
        public static Point Rotate(this ref Point point, double angle, Point origin)
        {
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
        public static double Length(this ref Point point)
        {
            return Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }

        /// <summary>
        /// Length squared of this point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double LengthSquared(this ref Point point)
        {
            return point.X * point.X + point.Y * point.Y;
        }

        /// <summary>
        /// Calculate the angle in degrees between current and specified point.
        /// </summary>
        /// <param name="point1">Curreent point</param>
        /// <param name="point2">Other point</param>
        /// <returns></returns>
        public static double AngleBetween(this ref Point point1, Point point2)
        {
            double sin = point1.X * point2.Y - point2.X * point1.Y;
            double cos = point1.X * point2.X + point2.Y * point1.X;
            double tan = Math.Atan2(sin, cos);

            return tan.ToDegrees();
        }
    }
}
