using System.Diagnostics;
using Graph3D.Drawing;
using Graph3D.Framework.Drawing;
using Graph3D.Math;

namespace Graph3D.Framework.Lights {
    public class OmniLight3D : Light3D {

        private Vector3D _position;
        public Vector3D Position {
            [DebuggerStepThrough]
            get { return _position; }
            [DebuggerStepThrough]
            set { _position = value; }
        }

        private float _power;
        public float Power {
            get { return _power; }
            set { _power = value; }
        }


        private PreciseColor _color;
        public virtual PreciseColor Color {
            get { return _color; }
            set { _color = value; }
        }

        public override void AcceptVisitor(ILight3DVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
