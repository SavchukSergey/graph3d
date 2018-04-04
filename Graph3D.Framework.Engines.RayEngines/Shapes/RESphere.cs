using System.Diagnostics;
using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class RESphere : REBaseShape {

        public RESphere(Sphere3D sphere)
            : base(sphere) {
            Radius = sphere.Radius;
            Radius2 = sphere.Radius * sphere.Radius;
            Position = sphere.CoordinateSystem.Position;
        }


        public override void GetIntersections(ColoredRay3D ray, NearestIntersection intersections) {
            RayEngineMath.GetIntersections(ray, this, intersections);
        }

        public float Radius2 { get; }

        public float Radius { get; }

        public Vector3D Position { get; }

    }
}
