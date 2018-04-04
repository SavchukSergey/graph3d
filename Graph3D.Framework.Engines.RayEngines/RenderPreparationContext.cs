using System.Collections.Generic;
using System.Diagnostics;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Math;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines {
    public class RenderPreparationContext {

        [DebuggerStepThrough]
        public RenderPreparationContext(REScene scene) {
            Scene = scene;
        }

        public REScene Scene { get; }

        private readonly Stack<CoordinateSystem> csystems = new Stack<CoordinateSystem>();

        public void PushCoordinateSystem(CoordinateSystem csystem) {
            if (csystems.Count == 0) {
                csystems.Push(csystem);
            } else {
                csystems.Push(csystems.Peek().ToAbsolute(csystem));
            }
        }

        public void PopCoordinateSystem() {
            csystems.Pop();
        }

        public Vector3D ToAbsolute(in Vector3D vector) {
            if (csystems.Count == 0) {
                return vector;
            } else {
                return csystems.Peek().ToAbsolute(vector);
            }
        }

    }
}
