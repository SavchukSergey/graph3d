using Graph3D.Drawing;
using Graph3D.Framework.Cameras;

namespace Graph3D.Framework.Engines {
    public abstract class Graph3DEngine {

        public abstract void Render(Scene3D scene, Camera3D camera, Canvas canvas);

    }
}
