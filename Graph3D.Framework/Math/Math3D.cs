using Graph3D.Math;

namespace Graph3D.Framework.Math {
    public static class Math3D {

        public static float CalcDet(
            float a11, float a12, float a13,
            float a21, float a22, float a23,
            float a31, float a32, float a33
        ) {
            return a11 * (a22 * a33 - a23 * a32) - a12 * (a21 * a33 - a23 * a31) + a13 * (a21 * a32 - a22 * a31);
        }

        public static Vector3D GetReflectedVector(Vector3D origin, Vector3D normal) {
            float cosa = -Vector3D.Scalar(origin.Normalize(), normal);
            Vector3D n = normal * (origin.Length * cosa);
            Vector3D a = origin + n;
            return n + a;
        }

    }
}
