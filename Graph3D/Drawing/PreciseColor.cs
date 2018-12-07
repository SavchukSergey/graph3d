using System.Diagnostics;

namespace Graph3D.Drawing {
	[DebuggerDisplay("Red: {Red}, Green: {Green}, Blue: {Blue}")]
    public readonly struct PreciseColor {

        [DebuggerStepThrough]
        public PreciseColor(float red, float green, float blue) {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public readonly float Red;

        public readonly float Green;

        public readonly float Blue;

        [DebuggerStepThrough]
        public static PreciseColor operator +(in PreciseColor first, in PreciseColor second) {
            return new PreciseColor(first.Red + second.Red, first.Green + second.Green, first.Blue + second.Blue);
        }

        [DebuggerStepThrough]
        public static PreciseColor operator *(in PreciseColor color, float multiplier) {
            return new PreciseColor(color.Red * multiplier, color.Green * multiplier, color.Blue * multiplier);
        }

        public static bool operator ==(in PreciseColor a, in PreciseColor b) {
            return a.Red == b.Red && a.Green == b.Green && a.Blue == b.Blue;
        }

        public static bool operator !=(in PreciseColor a, in PreciseColor b) {
            return a.Red != b.Red || a.Green != b.Green || a.Blue != b.Blue;
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Red.GetHashCode();
                hashCode = (hashCode * 397) ^ Green.GetHashCode();
                hashCode = (hashCode * 397) ^ Blue.GetHashCode();
                return hashCode;
            }
        }


        public override bool Equals(object obj) {
            var other = (PreciseColor)obj;
            return (Red == other.Red) && (Green == other.Green) && (Blue == other.Blue);
        }

        public bool Equals(in PreciseColor other) {
            return (Red == other.Red) && (Green == other.Green) && (Blue == other.Blue);
        }

    }
}
