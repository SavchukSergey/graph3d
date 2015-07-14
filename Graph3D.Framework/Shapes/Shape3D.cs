using System.Diagnostics;
using Graph3D.Framework.Drawing;
using Graph3D.Math;

namespace Graph3D.Framework.Shapes {
    public abstract class Shape3D : Object3D {

        [DebuggerStepThrough]
        protected Shape3D()
            : this(new Vector3D(0, 0, 0)) {
        }

        [DebuggerStepThrough]
        protected Shape3D(Vector3D position)
            : base(position) {
        }

        private Material _material = new Material();
        public Material Material {
            [DebuggerStepThrough]
            get { return _material; }
            set { _material = value; }
        }

        public abstract void AcceptVisitor(IShape3DVisitor visitor);

    }
}
