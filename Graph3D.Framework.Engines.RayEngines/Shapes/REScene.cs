using System.Diagnostics;
using Graph3D.Framework.Engines.RayEngines.Lights;
using Graph3D.Framework.Lights;
using Graph3D.Framework.Shapes;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class REScene {

        [DebuggerStepThrough]
        public REScene() {
            lights = new RELightComposite(new Light3DComposite(), this);
        }

        private readonly REShapeComposite objects = new REShapeComposite(new Shape3DComposite());
        public REShapeComposite Objects {
            [DebuggerStepThrough]
            get { return objects; }
        }

        private readonly RELightComposite lights;
        public RELightComposite Lights {
            [DebuggerStepThrough]
            get { return lights; }
        }

    }
}
