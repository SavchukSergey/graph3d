using System.Diagnostics;
using Graph3D.Framework.Lights;
using Graph3D.Framework.Shapes;

namespace Graph3D.Framework {
    public class Scene3D {

        private readonly Shape3DComposite _shapes = new Shape3DComposite();
        public Shape3DComposite Shapes {
            [DebuggerStepThrough]
            get { return _shapes; }
        }

        private readonly Light3DComposite _lights = new Light3DComposite();
        public Light3DComposite Lights {
            [DebuggerStepThrough]
            get { return _lights; }
        }

    }
}
