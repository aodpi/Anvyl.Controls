using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Anvyl.Controls.Extensions
{
    public static class PointExtensions
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

        public static double DistanceSquared(this Point point, Point point2)
        {
            var dx = point.X - point2.X;
            var dy = point.Y - point2.Y;
            return dx * dx + dy * dy;
        }

        public static Point Normalize(this Point point)
        {
            var ls = Math.Max(Math.Abs(point.X), Math.Abs(point.Y));
            var x = point.X / ls;
            var y = point.Y / ls;
            var len = point.Length();

            return new Point(x / len, y / len);
        }

        public static Point Rotate(this Point point, double degrees)
        {
            return Rotate(point, degrees, new Point());
        }

        public static Point Rotate(this Point point, double angle, Point center)
        {
            var angleInRadians = angle.ToRadians();

            float cos = (float)Math.Cos(angleInRadians);
            float sin = (float)Math.Sin(angleInRadians);
            
            var x = (point.X - center.X) * cos - (point.Y - center.Y) * sin + center.X;
            var y = (point.X - center.X) * sin + (point.Y - center.Y) * cos + center.Y;
            
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

            return Math.Atan2(sin, cos).ToDegrees();
        }
    }
}
