using System.Diagnostics;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public abstract class REBaseShape {

        protected CoordinateSystem _inner;
        protected CoordinateSystem _outer;

        [DebuggerStepThrough]
        protected REBaseShape(Shape3D origin) {
            _material = origin.Material;
            _inner = origin.CoordinateSystem;
            _outer = origin.CoordinateSystem.ToReverse();
        }

        private readonly Material _material;
        public Material Material {
            [DebuggerStepThrough]
            get { return _material; }
        }

        //TODO: Hide ray from params. Use its parameters instead.
        public abstract void GetIntersections(ColoredRay3D ray, NearestIntersection intersections);

        public virtual void OnIntersection(Intersection intersection) {
        }

    }
}
