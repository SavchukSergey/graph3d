using System.Diagnostics;

namespace Graph3D.Math {
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}")]
    public struct Vector3D {

        private bool _lengthValid;

        [DebuggerStepThrough]
        public Vector3D(float x, float y, float z) {
            _x = x;
            _y = y;
            _z = z;
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

        private float _z;
        public float Z {
            [DebuggerStepThrough]
            get { return _z; }
            [DebuggerStepThrough]
            set {
                _z = value;
                _lengthValid = false;
            }
        }

        private float _length;
        public float Length {
            get {
                if (!_lengthValid) {
                    _length = (float)System.Math.Sqrt(_x * _x + _y * _y + _z * _z);
                    _lengthValid = true;
                }
                return _length;
            }
        }

        [DebuggerStepThrough]
        public static Vector3D operator -(Vector3D first, Vector3D second) {
            return new Vector3D(first._x - second._x, first._y - second._y, first._z - second._z);
        }

        [DebuggerStepThrough]
        public static Vector3D operator +(Vector3D first, Vector3D second) {
            return new Vector3D(first._x + second._x, first._y + second._y, first._z + second._z);
        }

        [DebuggerStepThrough]
        public static Vector3D operator /(Vector3D vector, float divider) {
            return new Vector3D(vector._x / divider, vector._y / divider, vector._z / divider);
        }

        [DebuggerStepThrough]
        public static Vector3D operator *(Vector3D vector, float multiplier) {
            return new Vector3D(vector._x * multiplier, vector._y * multiplier, vector._z * multiplier);
        }

        [DebuggerStepThrough]
        public static Vector3D operator *(float multiplier, Vector3D vector) {
            return new Vector3D(vector._x * multiplier, vector._y * multiplier, vector._z * multiplier);
        }

        [DebuggerStepThrough]
        public static float Scalar(Vector3D first, Vector3D second) {
            return first._x * second.X + first.Y * second.Y + first.Z * second.Z;
        }

        [DebuggerStepThrough]
        public static Vector3D Product(Vector3D first, Vector3D second) {
            float x = first._y * second._z - first._z * second._y;
            float y = first._z * second._x - first._x * second._z;
            float z = first._x * second._y - first._y * second._x;
            return new Vector3D(x, y, z);
        }

        [DebuggerStepThrough]
        public Vector3D Normalize() {
            if (Length > 0) return this / Length;
            return this;
        }

    }
}
