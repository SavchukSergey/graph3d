using System.Diagnostics;
using Graph3D.Drawing;

namespace Graph3D.Framework.Drawing {
    public class Material {

        [DebuggerStepThrough]
        public Material() {
        }

        private PreciseColor _diffuseColor = new PreciseColor(0.8f, 0.8f, 0.8f );
        public PreciseColor DiffuseColor {
            get { return _diffuseColor; }
            set { _diffuseColor = value; }
        }

        private PreciseColor _emmisiveColor = new PreciseColor(0.0f, 0.0f, 0.0f);
        public PreciseColor EmmisiveColor {
            get { return _emmisiveColor; }
            set { _emmisiveColor = value; }
        }

        private PreciseColor _specularColor = new PreciseColor(0.8f, 0.8f, 0.8f);
        public PreciseColor SpecularColor {
            get { return _specularColor; }
            set { _specularColor = value; }
        }

        private float _ambientIntensity = 0.2f;
        public float AmbientIntensity {
            get { return _ambientIntensity; }
            set { _ambientIntensity = value; }
        }

        private float _shininess = 0.2f;
        public float Shininess {
            get { return _shininess; }
            set { _shininess = value; }
        }

    }
}
