using System.Diagnostics;
using Graph3D.Drawing;
using Graph3D.Math;

namespace Graph3D.Framework.Lights {
    public class OmniLight3D : Light3D {

        public Vector3D Position {
            [DebuggerStepThrough]
            get;
            [DebuggerStepThrough]
            set;
        }

        public float Power { get; set; }


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
