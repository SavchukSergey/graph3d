using System.Drawing;
using Graph3D.Drawing;

namespace Graph3D.Framework.Drawing {
    public class BitmapBuilder {

        public Bitmap BuildBitmap(Canvas canvas, float brightness) {
            var bmp = new Bitmap(canvas.Width, canvas.Height);
            for (var y = 0; y < bmp.Height; y++) {
                for (var x = 0; x < bmp.Width; x++) {
                    var pColor = canvas[x, y] * brightness;
                    var color = pColor.ToColor();
                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp;
        }

    }
}
