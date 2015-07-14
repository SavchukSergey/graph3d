﻿using System.Diagnostics;
using System.Drawing;

namespace Graph3D.Drawing {
    [DebuggerDisplay("Red: {Red}, Green: {Green}, Blue: {Blue}")]
    public struct PreciseColor {

        [DebuggerStepThrough]
        public PreciseColor(float red, float green, float blue) {
            _red = red > 0 ? red : 0;
            _green = green > 0 ? green : 0;
            _blue = blue > 0 ? blue : 0;
        }

        private float _red;
        public float Red {
            [DebuggerStepThrough]
            get { return _red; }
            [DebuggerStepThrough]
            set { _red = value > 0 ? value : 0; }
        }

        private float _green;
        public float Green {
            [DebuggerStepThrough]
            get { return _green; }
            [DebuggerStepThrough]
            set { _green = value > 0 ? value : 0; }
        }

        private float _blue;
        public float Blue {
            [DebuggerStepThrough]
            get { return _blue; }
            [DebuggerStepThrough]
            set { _blue = value > 0 ? value : 0; }
        }

        [DebuggerStepThrough]
        public static PreciseColor operator +(PreciseColor first, PreciseColor second) {
            return new PreciseColor(first._red + second._red, first._green + second._green, first._blue + second._blue);
        }

        [DebuggerStepThrough]
        public static PreciseColor operator *(PreciseColor color, float multiplier) {
            return new PreciseColor(color._red * multiplier, color._green * multiplier, color._blue * multiplier);
        }

        public Color ToColor() {
            var red = _red > 0 ? (_red < 1.0 ? (byte)(_red * 255) : 255) : 0;
            var green = _green > 0 ? (_green < 1.0 ? (byte)(_green * 255) : 255) : 0;
            var blue = _blue > 0 ? (_blue < 1.0 ? (byte)(_blue * 255) : 255) : 0;
            return Color.FromArgb(red, green, blue);
        }

    }
}
