using Graph3D.Math;

namespace Graph3D.Framework.Cameras {
    public abstract class Camera3D {

        public Camera3D() {
            CoordinateSystem = new CoordinateSystem();
            Ratio = 4.0 / 3.0;
            FocusDistance = 0.035;
            FOV = 60 * System.Math.PI / 180;
        }

        public CoordinateSystem CoordinateSystem { get; set; }

        public double FOV { get; set; }

        public double FocusDistance { get; set; }

        public double Ratio { get; set; }

        public Vector3D Position { get; set; }

    }
}
