using System.Drawing;
using Graph3D.Drawing;

namespace Graph3D.Win {
	public static class CanvasExt {

		public static Canvas FromImage(Bitmap img) {
			var res = new Canvas(img.Width, img.Height);
			for (var y = 0; y < res.Height; y++) {
				for (var x = 0; x < res.Width; x++) {
					var clr = img.GetPixel(x, y);
					res[x, y] = new PreciseColor(clr.R / 255f, clr.G / 255f, clr.B / 255f);
				}
			}
			return res;
		}

		public static Color ToColor(this PreciseColor color) {
			var r = color.Red;
			var g = color.Green;
			var b = color.Blue;

			if (r > 1) {
				r = 1;
			} else if (r < 0) {
				r = 0;
			}

			if (g > 1) {
				g = 1;
			} else if (g < 0) {
				g = 0;
			}

			if (b > 1) {
				b = 1;
			} else if (b < 0) {
				b = 0;
			}

			return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
		}

		/*
        public PreciseColor(in Color clr) {
            Red = clr.R / 255f;
            Green = clr.G / 255f;
            Blue = clr.B / 255f;
        }
		*/

	}
}
