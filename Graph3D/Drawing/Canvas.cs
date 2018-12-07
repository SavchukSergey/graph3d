namespace Graph3D.Drawing {
	public class Canvas {

        public int Width { get; }

        public int Height { get; }

        public Canvas(int width, int height) {
            Width = width;
            Height = height;
            Clear();
        }

        private PreciseColor[,] _points;
        public PreciseColor this[int x, int y] {
            get { return _points[y, x]; }
            set { _points[y, x] = value; }
        }


        public void Clear() {
            _points = new PreciseColor[Height, Width];
            for (var y = 0; y < Height; y++) {
                for (var x = 0; x < Width; x++) {
                    _points[y, x] = new PreciseColor();
                }
            }
        }

    }
}
