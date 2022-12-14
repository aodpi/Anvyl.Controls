using System.Diagnostics;

namespace Anvyl.Controls.Models.Loader
{
    [DebuggerDisplay("{DebugDisplay,nq}")]
    internal struct LoaderTick : IEquatable<LoaderTick>
    {
        public LoaderTick(Point start, Point end, double angle)
        {
            Start = start;
            End = end;
            Angle = angle;
        }

        /// <summary>
        /// Start point of this tick
        /// </summary>
        public Point Start { get; private set; }

        /// <summary>
        /// End point of this tick
        /// </summary>
        public Point End { get; private set; }

        public double Angle { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is LoaderTick tick && Equals(tick);
        }

        public bool Equals(LoaderTick other)
        {
            return EqualityComparer<Point>.Default.Equals(Start, other.Start) &&
                   EqualityComparer<Point>.Default.Equals(End, other.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public static bool operator ==(LoaderTick left, LoaderTick right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LoaderTick left, LoaderTick right)
        {
            return !(left == right);
        }

        private string DebugDisplay
        {
            get
            {
                return $"({Start.X:#.###}, {Start.Y:#.###}) ---> ({End.X:#.###}, {End.Y:#.###})";
            }
        }
    }
}
