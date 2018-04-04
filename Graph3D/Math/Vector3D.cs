using System.Diagnostics;

namespace Graph3D.Math {
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}")]
    public struct Vector3D {

        private bool _lengthValid;
        private float _length;

        [DebuggerStepThrough]
        public Vector3D(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
            _length = 0;
            _lengthValid = false;
        }

        public readonly float X;

        public readonly float Y;

        public readonly float Z;

        public float Length {
            get {
                if (!_lengthValid) {
                    _length = (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
                    _lengthValid = true;
                }
                return _length;
            }
        }

        [DebuggerStepThrough]
        public static Vector3D operator -(in Vector3D first, in Vector3D second) {
            return new Vector3D(first.X - second.X, first.Y - second.Y, first.Z - second.Z);
        }

        [DebuggerStepThrough]
        public static Vector3D operator +(in Vector3D first, in Vector3D second) {
            return new Vector3D(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
        }

        [DebuggerStepThrough]
        public static Vector3D operator /(in Vector3D vector, float divider) {
            return new Vector3D(vector.X / divider, vector.Y / divider, vector.Z / divider);
        }

        [DebuggerStepThrough]
        public static Vector3D operator *(in Vector3D vector, float multiplier) {
            return new Vector3D(vector.X * multiplier, vector.Y * multiplier, vector.Z * multiplier);
        }

        [DebuggerStepThrough]
        public static Vector3D operator *(float multiplier, in Vector3D vector) {
            return new Vector3D(vector.X * multiplier, vector.Y * multiplier, vector.Z * multiplier);
        }

        [DebuggerStepThrough]
        public static float Scalar(in Vector3D first, in Vector3D second) {
            return first.X * second.X + first.Y * second.Y + first.Z * second.Z;
        }

        [DebuggerStepThrough]
        public static Vector3D Product(in Vector3D first, in Vector3D second) {
            float x = first.Y * second.Z - first.Z * second.Y;
            float y = first.Z * second.X - first.X * second.Z;
            float z = first.X * second.Y - first.Y * second.X;
            return new Vector3D(x, y, z);
        }

        [DebuggerStepThrough]
        public Vector3D Normalize() {
            var len = Length;
            if (len > 0) return this / len;
            return this;
        }

    }
}
