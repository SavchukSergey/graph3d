using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class RERectangle : REFlatShape {

        public RERectangle(Rectangle3D rect)
            : base(rect, rect.A, rect.B - rect.A, rect.C - rect.A) {
        }


        public override void GetIntersections(ColoredRay3D ray, NearestIntersection intersections) {
            RayEngineMath.GetIntersections(ray, this, intersections);
        }

        public override bool ValidateTexture(float u, float v) {
            if (u < 0 || v < 0) return false;
            if (u > 1 || v > 1) return false;
            return true;
        }

    }
}
