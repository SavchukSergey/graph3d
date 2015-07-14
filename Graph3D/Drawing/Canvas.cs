using System.Drawing;

namespace Graph3D.Drawing {
    public class Canvas {

        private readonly int _width;
        public int Width {
            get { return _width; }
        }

        private readonly int _height;
        public int Height {
            get { return _height; }
        }

        public Canvas(int width, int height) {
            _width = width;
            _height = height;
            Clear();
        }

        private PreciseColor[,] _points;
        public PreciseColor this[int x, int y] {
            get { return _points[y, x]; }
            set { _points[y, x] = value; }
        }


        public void Clear() {
            _points = new PreciseColor[_height, _width];
            for (int y = 0; y < _height; y++) {
                for (int x = 0; x < _width; x++) {
                    _points[y, x] = new PreciseColor();
                }
            }
        }

        public static Canvas FromImage(Bitmap img) {
            var res = new Canvas(img.Width, img.Height);
            for (var y = 0; y < res.Height; y++) {
                for (var x = 0; x < res.Width; x++) {
                    var clr = img.GetPixel(x, y);
                    res[x, y] = new PreciseColor(clr);
                }
            }
            return res;
        }
    }
}
