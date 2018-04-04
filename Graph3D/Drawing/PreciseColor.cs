using System.Diagnostics;
using System.Drawing;

namespace Graph3D.Drawing {
    [DebuggerDisplay("Red: {Red}, Green: {Green}, Blue: {Blue}")]
    public readonly struct PreciseColor {

        [DebuggerStepThrough]
        public PreciseColor(float red, float green, float blue) {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public PreciseColor(in Color clr) {
            Red = clr.R / 255f;
            Green = clr.G / 255f;
            Blue = clr.B / 255f;
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

        public Color ToColor() {
            var r = Red;
            var g = Green;
            var b = Blue;

            if (r > 1) r = 1;
            else if (r < 0) r = 0;

            if (g > 1) g = 1;
            else if (g < 0) g = 0;

            if (b > 1) b = 1;
            else if (b < 0) b = 0;

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
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
