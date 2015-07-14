using Graph3D.Drawing;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Shapes;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class SensitiveMatrix : REBaseShape {

        private RERectangle rectangle;

        public SensitiveMatrix(Canvas canvas)
            : base(null) {
            this.canvas = canvas;
            Rectangle3D origin = new Rectangle3D();

            // TODO: set rectangle's params

            //rectangle = new RERectangle(origin);

        }

        private readonly Canvas canvas;
        public Canvas Canvas {
            get { return canvas; }
        }

        public override void GetIntersections(ColoredRay3D ray, NearestIntersection intersections) {
            rectangle.GetIntersections(ray, intersections);
        }

        public override void OnIntersection(Intersection intersection) {
            int x = (int)(canvas.Width * intersection.TextureU);
            int y = (int)(canvas.Height * intersection.TextureV);
            if (x < 0) x = 0; else if (x >= canvas.Width) x = canvas.Width - 1;
            if (y < 0) y = 0; else if (y >= canvas.Height) y = canvas.Height - 1;
            canvas[x, y] = intersection.Ray.Color;
            base.OnIntersection(intersection);
        }

    }
}
