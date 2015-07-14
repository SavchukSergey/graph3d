using System.Diagnostics;
using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class RESphere : REBaseShape {

        public RESphere(Sphere3D sphere)
            : base(sphere) {
            _radius = sphere.Radius;
            _radius2 = sphere.Radius * sphere.Radius;
            _position = sphere.CoordinateSystem.Position;
        }


        public override void GetIntersections(ColoredRay3D ray, NearestIntersection intersections) {
            RayEngineMath.GetIntersections(ray, this, intersections);
        }

        private readonly float _radius2;
        public float Radius2 {
            [DebuggerStepThrough]
            get { return _radius2; }
        }
        
        private readonly float _radius;
        public float Radius {
            [DebuggerStepThrough]
            get { return _radius; }
        }

        private readonly Vector3D _position;
        public Vector3D Position {
            get { return _position; }
        }

    }
}
