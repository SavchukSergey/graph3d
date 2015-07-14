using System.Diagnostics;
using Graph3D.Math;

namespace Graph3D.Framework.Shapes {
    [DebuggerDisplay("Sphere3D - X: {Position.X}, Y: {Position.Y}, Z: {Position.Z}, Radius: {Radius}")]
    public class Sphere3D : Shape3D {

        public Sphere3D()
            : this(new Vector3D(0, 0, 0)) {
        }

        public Sphere3D(Vector3D position)
            : this(position, 50) {
        }

        public Sphere3D(Vector3D position, float radius)
            : base(position) {
            Radius = radius;
        }

        public float Radius { get; set; }

        public override void AcceptVisitor(IShape3DVisitor visitor) {
            visitor.Visit(this);
        }

    }
}
