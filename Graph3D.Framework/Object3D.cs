using System.Diagnostics;
using Graph3D.Math;

namespace Graph3D.Framework {
    public abstract class Object3D {

        [DebuggerStepThrough]
        protected Object3D()
            : this(new Vector3D(0, 0, 0)) {
        }

        [DebuggerStepThrough]
        protected Object3D(Vector3D position) {
            CoordinateSystem = new CoordinateSystem {
                Position = position
            };
        }

        public CoordinateSystem CoordinateSystem { get; set; }
    }
}
