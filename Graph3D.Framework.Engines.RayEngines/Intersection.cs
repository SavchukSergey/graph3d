using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines {
    public class Intersection {

        public Intersection(float textureU, float textureV, float textureW) {
            TextureU = textureU;
            TextureV = textureV;
            TextureW = textureW;
        }

        //TODO: ray engine specifc
        public ColoredRay3D Ray { get; set; }

        public Vector3D Point { get; set; }

        public REBaseShape Shape3D { get; set; }

        public float Length { get; set; }

        public Vector3D Normal { get; set; }

        public float TextureU { get; }
        public float TextureV { get; }
        public float TextureW { get; }

    }
}
