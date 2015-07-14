using System.Diagnostics;
using Graph3D.Math;

namespace Graph3D.Framework.Math {
    public class Ray3D {

        private bool _lengthValid;

        private Vector3D _start;
        public Vector3D Start {
            [DebuggerStepThrough]
            get { return _start; }
            [DebuggerStepThrough]
            set {
                _start = value;
                _lengthValid = false;
            }
        }

        private Vector3D _end;
        public Vector3D End {
            [DebuggerStepThrough]
            get { return _end; }
            [DebuggerStepThrough]
            set {
                _end = value;
                _lengthValid = false;
            }
        }

        private float _length;
        public float Length {
            [DebuggerStepThrough]
            get {
                if (!_lengthValid) {
                    _length = (_end - _start).Length;
                    _lengthValid = true;
                }
                return _length;
            }
        }

    }
}
