using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class RETriangle : REFlatShape {

        public RETriangle(Triangle3D triangle)
            : base(triangle, triangle.A, triangle.B - triangle.A, triangle.C - triangle.A) {
        }


        public override void GetIntersections(ColoredRay3D ray, NearestIntersection intersections) {
            RayEngineMath.GetIntersections(ray, this, intersections);
        }

        public override bool ValidateTexture(float u, float v) {
            if (u < 0 || v < 0) return false;
            if (u > 1 || v > 1) return false; 
            if (u + v > 1) return false;
            return true;
        }

    }
}
