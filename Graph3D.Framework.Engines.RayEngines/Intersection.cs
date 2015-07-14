using System.Diagnostics;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Math;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines {
    public class Intersection {

        public Intersection(float textureU, float textureV, float textureW) {
            this.textureU = textureU;
            this.textureV = textureV;
            this.textureW = textureW;
        }

        private ColoredRay3D ray;
        //TODO: ray engine specifc
        public ColoredRay3D Ray {
            get { return ray; }
            set { ray = value; }
        }

        private Vector3D point;
        public Vector3D Point {
            get { return point; }
            set { point = value; }
        }

        private REBaseShape shape3D;
        public REBaseShape Shape3D {
            [DebuggerStepThrough]
            get { return shape3D; }
            [DebuggerStepThrough]
            set { shape3D = value; }
        }

        private float length;
        public float Length {
            get { return length; }
            set { length = value; }
        }

        private Vector3D normal;
        public Vector3D Normal {
            get { return normal; }
            set { normal = value; }
        }

        private readonly float textureU;
        public float TextureU {
            get { return textureU; }
        }

        private readonly float textureV;
        public float TextureV {
            get { return textureV; }
        }

        private readonly float textureW;
        public float TextureW {
            get { return textureW; }
        }

    }
}
