using Graph3D.Math;

namespace Graph3D.Framework.Cameras {
    public class GeneralCamera3D : Camera3D {

        public GeneralCamera3D()
            : this(new Vector3D(0, 0, 0)) {
        }

        public GeneralCamera3D(in Vector3D position) {
            Position = position;
        }

    }
}
