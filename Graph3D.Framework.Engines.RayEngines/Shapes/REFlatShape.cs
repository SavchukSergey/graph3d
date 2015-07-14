using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public abstract class REFlatShape : REBaseShape {
        
        protected REFlatShape(Shape3D origin, Vector3D a, Vector3D ba, Vector3D ca)
            : base(origin) {
            _a = a;
            _ba = ba;
            _ca = ca;
            _b = ba + a;
            _c = ca + a;
            _w = Vector3D.Product(ba, ca);
            float det = Math3D.CalcDet(U.X, V.X, W.X,
                                       U.Y, V.Y, W.Y,
                                       U.Z, V.Z, W.Z);
            _tu = Vector3D.Product(V, W) / det;
            _tv = Vector3D.Product(W, U) / det;
            _tw = Vector3D.Product(U, V) / det;
            _normal = _w.Normalize();

        }

        private readonly Vector3D _a;
        public Vector3D A {
            get { return _a; }
        }

        private readonly Vector3D _b;
        public Vector3D B {
            get { return _b; }
        }

        private readonly Vector3D _c;
        public Vector3D C {
            get { return _c; }
        }

        private readonly Vector3D _ba;
        public Vector3D U {
            get { return _ba; }
        }

        private readonly Vector3D _ca;
        public Vector3D V {
            get { return _ca; }
        }

        private readonly Vector3D _w;
        public Vector3D W {
            get { return _w; }
        }

        private readonly Vector3D _tu;
        public Vector3D Tu {
            get { return _tu; }
        }

        private readonly Vector3D _tv;
        public Vector3D Tv {
            get { return _tv; }
        }

        private readonly Vector3D _tw;
        public Vector3D Tw {
            get { return _tw; }
        }

        private readonly Vector3D _normal;
        public Vector3D Normal {
            get { return _normal; }
        }

        public abstract bool ValidateTexture(float u, float v);

    }
}
