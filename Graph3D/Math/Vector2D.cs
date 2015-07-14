using System.Diagnostics;

namespace Graph3D.Math {
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public struct Vector2D {

        private bool _lengthValid;

        [DebuggerStepThrough]
        public Vector2D(float x, float y) {
            _x = x;
            _y = y;
            _length = 0;
            _lengthValid = false;
        }

        private float _x;
        public float X {
            [DebuggerStepThrough]
            get { return _x; }
            [DebuggerStepThrough]
            set {
                _x = value;
                _lengthValid = false;
            }
        }

        private float _y;
        public float Y {
            [DebuggerStepThrough]
            get { return _y; }
            [DebuggerStepThrough]
            set {
                _y = value;
                _lengthValid = false;
            }
        }

        private float _length;
        public float Length {
            get {
                if (!_lengthValid) {
                    _length = (float)System.Math.Sqrt(_x * _x + _y * _y);
                    _lengthValid = true;
                }
                return _length;
            }
        }

        [DebuggerStepThrough]
        public static Vector2D operator -(Vector2D first, Vector2D second) {
            return new Vector2D(first._x - second._x, first._y - second._y);
        }

        [DebuggerStepThrough]
        public static Vector2D operator +(Vector2D first, Vector2D second) {
            return new Vector2D(first._x + second._x, first._y + second._y);
        }

        [DebuggerStepThrough]
        public static Vector2D operator /(Vector2D vector, float divider) {
            return new Vector2D(vector._x / divider, vector._y / divider);
        }

        [DebuggerStepThrough]
        public static Vector2D operator *(Vector2D vector, float multiplier) {
            return new Vector2D(vector._x * multiplier, vector._y * multiplier);
        }

        [DebuggerStepThrough]
        public static Vector2D operator *(float multiplier, Vector2D vector) {
            return new Vector2D(vector._x * multiplier, vector._y * multiplier);
        }

        [DebuggerStepThrough]
        public static float Scalar(Vector2D first, Vector2D second) {
            return first._x * second.X + first.Y * second.Y;
        }

        [DebuggerStepThrough]
        public Vector2D Normalize() {
            if (Length > 0) return this / Length;
            return this;
        }

    }
}
