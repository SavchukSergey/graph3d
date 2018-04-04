using System.Diagnostics;

namespace Graph3D.Math {
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public struct Vector2D {

        private bool _lengthValid;

        [DebuggerStepThrough]
        public Vector2D(float x, float y) {
            X = x;
            Y = y;
            _length = 0;
            _lengthValid = false;
        }

        public readonly float X;

        public readonly float Y;

        private float _length;
        public float Length {
            get {
                if (!_lengthValid) {
                    _length = (float)System.Math.Sqrt(X * X + Y * Y);
                    _lengthValid = true;
                }
                return _length;
            }
        }

        [DebuggerStepThrough]
        public static Vector2D operator -(in Vector2D first, in Vector2D second) {
            return new Vector2D(first.X - second.X, first.Y - second.Y);
        }

        [DebuggerStepThrough]
        public static Vector2D operator +(in Vector2D first, in Vector2D second) {
            return new Vector2D(first.X + second.X, first.Y + second.Y);
        }

        [DebuggerStepThrough]
        public static Vector2D operator /(in Vector2D vector, float divider) {
            return new Vector2D(vector.X / divider, vector.Y / divider);
        }

        [DebuggerStepThrough]
        public static Vector2D operator *(in Vector2D vector, float multiplier) {
            return new Vector2D(vector.X * multiplier, vector.Y * multiplier);
        }

        [DebuggerStepThrough]
        public static Vector2D operator *(float multiplier, in Vector2D vector) {
            return new Vector2D(vector.X * multiplier, vector.Y * multiplier);
        }

        [DebuggerStepThrough]
        public static float Scalar(in Vector2D first, in Vector2D second) {
            return first.X * second.X + first.Y * second.Y;
        }

        [DebuggerStepThrough]
        public Vector2D Normalize() {
            if (Length > 0) return this / Length;
            return this;
        }

    }
}
