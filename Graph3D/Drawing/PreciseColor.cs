using System.Diagnostics;
using System.Drawing;

namespace Graph3D.Drawing {
    [DebuggerDisplay("Red: {Red}, Green: {Green}, Blue: {Blue}")]
    public struct PreciseColor {

        private readonly float _red;
        private readonly float _green;
        private readonly float _blue;

        [DebuggerStepThrough]
        public PreciseColor(float red, float green, float blue) {
            _red = red;
            _green = green;
            _blue = blue;
        }

        public PreciseColor(Color clr) {
            _red = clr.R / 255f;
            _green = clr.G / 255f;
            _blue = clr.B / 255f;
        }

        public float Red {
            [DebuggerStepThrough]
            get { return _red; }
        }

        public float Green {
            [DebuggerStepThrough]
            get { return _green; }
        }

        public float Blue {
            [DebuggerStepThrough]
            get { return _blue; }
        }

        [DebuggerStepThrough]
        public static PreciseColor operator +(PreciseColor first, PreciseColor second) {
            return new PreciseColor(first._red + second._red, first._green + second._green, first._blue + second._blue);
        }

        [DebuggerStepThrough]
        public static PreciseColor operator *(PreciseColor color, float multiplier) {
            return new PreciseColor(color._red * multiplier, color._green * multiplier, color._blue * multiplier);
        }

        public static bool operator ==(PreciseColor a, PreciseColor b) {
            return a._red == b._red && a._green == b._green && a._blue == b._blue;
        }

        public static bool operator !=(PreciseColor a, PreciseColor b) {
            return a._red != b._red || a._green != b._green || a._blue != b._blue;
        }

        public Color ToColor() {
            var r = _red;
            var g = _green;
            var b = _blue;

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
                var hashCode = _red.GetHashCode();
                hashCode = (hashCode * 397) ^ _green.GetHashCode();
                hashCode = (hashCode * 397) ^ _blue.GetHashCode();
                return hashCode;
            }
        }


        public override bool Equals(object obj) {
            var other = (PreciseColor)obj;
            return (_red == other._red) && (_green == other._green) && (_blue == other._blue);
        }

        public bool Equals(PreciseColor other) {
            return (_red == other._red) && (_green == other._green) && (_blue == other._blue);
        }

    }
}
