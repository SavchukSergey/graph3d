using System.Diagnostics;

namespace Graph3D.Math {
    public class CoordinateSystem {

        public CoordinateSystem() {
            Position = new Vector3D(0, 0, 0);
            U = new Vector3D(1.0f, 0.0f, 0.0f);
            V = new Vector3D(0.0f, 1.0f, 0.0f);
            W = new Vector3D(0.0f, 0.0f, 1.0f);
        }

        [DebuggerStepThrough]
        public Vector3D ToAbsolute(in Vector3D vector) {
            return vector.X * U + vector.Y * V + vector.Z * W + Position;
        }

        public CoordinateSystem ToReverse() {
            var det = CalcDet();

            var u = U;
            var v = V;
            var w = W;

            var c11 = (v.Y * w.Z - w.Y * v.Z) / det;
            var c12 = (w.Y * u.Z - u.Y * w.Z) / det;
            var c13 = (u.Y * v.Z - v.Y * u.Z) / det;

            var c21 = (w.X * v.Z - v.X * w.Z) / det;
            var c22 = (u.X * w.Z - w.X * u.Z) / det;
            var c23 = (v.X * u.Z - u.X * v.Z) / det;

            var c31 = (v.X * w.Y - w.X * v.Y) / det;
            var c32 = (w.X * u.Y - u.X * w.Y) / det;
            var c33 = (u.X * v.Y - v.X * u.Y) / det;

            var cs = new CoordinateSystem {
                U = new Vector3D(c11, c12, c13),
                V = new Vector3D(c21, c22, c23),
                W = new Vector3D(c31, c32, c33),
                Position = new Vector3D(
                    -(Position.X * c11 + Position.Y * c21 + Position.Z * c31),
                    -(Position.X * c12 + Position.Y * c22 + Position.Z * c32),
                    -(Position.X * c13 + Position.Y * c23 + Position.Z * c33)
                )
            };
            return cs;
        }

        public Vector3D ToRelative(in Vector3D vector) {
            var rel = vector - Position;
            var det = CalcDet();
            var detX = CalcDet(rel.X, V.X, W.X,
                                        rel.Y, V.Y, W.Y,
                                        rel.Z, V.Z, W.Z);
            var detY = CalcDet(U.X, rel.X, W.X,
                                        U.Y, rel.Y, W.Y,
                                        U.Z, rel.Z, W.Z);
            var detZ = CalcDet(U.X, V.X, rel.X,
                                        U.Y, V.Y, rel.Y,
                                        U.Z, V.Z, rel.Z);
            return new Vector3D(detX / det, detY / det, detZ / det);
        }

        public CoordinateSystem ToAbsolute(CoordinateSystem cs) {
            var res = new CoordinateSystem {
                U = ToAbsolute(cs.U),
                V = ToAbsolute(cs.V),
                W = ToAbsolute(cs.W),
                Position = cs.Position + Position
            };
            return res;
        }

        public CoordinateSystem Translate(in Vector3D delta) {
            Position += delta;
            return this;
        }

        public void RotateW(float angle) {
            var sa = (float)System.Math.Sin(angle);
            var ca = (float)System.Math.Cos(angle);
            var nu = U * ca + V * sa;
            var nv = V * ca - U * sa;
            U = nu;
            V = nv;
        }

        public CoordinateSystem RotateV(float angle) {
            var sa = (float)System.Math.Sin(angle);
            var ca = (float)System.Math.Cos(angle);
            var nw = W * ca + U * sa;
            var nu = U * ca - W * sa;
            W = nw;
            U = nu;
            return this;
        }

        public CoordinateSystem RotateU(float angle) {
            var sa = (float)System.Math.Sin(angle);
            var ca = (float)System.Math.Cos(angle);
            var nv = V * ca + W * sa;
            var nw = W * ca - V * sa;
            V = nv;
            W = nw;
            return this;
        }

        protected float CalcDet() {
            return CalcDet(U.X, V.X, W.X,
                           U.Y, V.Y, W.Y,
                           U.Z, V.Z, W.Z);
        }

        public Vector3D Position;

        public Vector3D U;

        public Vector3D V;

        public Vector3D W;

        public CoordinateSystem Scale(float x, float y, float z) {
            U = U * x;
            V = V * y;
            W = W * z;
            return this;
        }

        public void Shift(float x, float y, float z) {
            Position += new Vector3D(x, y, z);
        }

        private static float CalcDet(
            float a11, float a12, float a13,
            float a21, float a22, float a23,
            float a31, float a32, float a33
        ) {
            return a11 * (a22 * a33 - a23 * a32) - a12 * (a21 * a33 - a23 * a31) + a13 * (a21 * a32 - a22 * a31);
        }
    }
}
