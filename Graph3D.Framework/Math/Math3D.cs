﻿using Graph3D.Math;

namespace Graph3D.Framework.Math {
    public static class Math3D {

        public static float CalcDet(
            float a11, float a12, float a13,
            float a21, float a22, float a23,
            float a31, float a32, float a33
        ) {
            return a11 * (a22 * a33 - a23 * a32) - a12 * (a21 * a33 - a23 * a31) + a13 * (a21 * a32 - a22 * a31);
        }

        public static Vector3D GetReflectedVector(in Vector3D origin, in Vector3D normal) {
            var cosa = -Vector3D.Scalar(origin.Normalize(), normal);
            var n = normal * (origin.Length * cosa);
            var a = origin + n;
            return n + a;
        }

    }
}
