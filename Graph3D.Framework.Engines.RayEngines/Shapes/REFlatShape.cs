using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public abstract class REFlatShape : REBaseShape {
        
        protected REFlatShape(Shape3D origin, in Vector3D a, in Vector3D ba, in Vector3D ca)
            : base(origin) {
            A = a;
            U = ba;
            V = ca;
            B = ba + a;
            C = ca + a;
            W = Vector3D.Product(ba, ca);
            float det = Math3D.CalcDet(U.X, V.X, W.X,
                                       U.Y, V.Y, W.Y,
                                       U.Z, V.Z, W.Z);
            Tu = Vector3D.Product(V, W) / det;
            Tv = Vector3D.Product(W, U) / det;
            Tw = Vector3D.Product(U, V) / det;
            Normal = W.Normalize();
        }

        public Vector3D A { get; }
        public Vector3D B { get; }
        public Vector3D C { get; }
        public Vector3D U { get; }
        public Vector3D V { get; }
        public Vector3D W { get; }
        public Vector3D Tu { get; }
        public Vector3D Tv { get; }
        public Vector3D Tw { get; }
        public Vector3D Normal { get; }

        public abstract bool ValidateTexture(float u, float v);

    }
}
