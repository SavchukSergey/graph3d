using System.Drawing;
using Graph3D.Drawing;

namespace Graph3D.Framework.Drawing {
    public class BitmapBuilder {

        public Bitmap BuildBitmap(Canvas canvas, float brightness) {
            var bmp = new Bitmap(canvas.Width, canvas.Height);
            for (int y = 0; y < bmp.Height; y++) {
                for (int x = 0; x < bmp.Width; x++) {
                    PreciseColor pColor = canvas[x, y] * brightness;
                    int red = pColor.Red > 0 ? (pColor.Red < 1.0 ? (byte)(pColor.Red * 255) : 255) : 0;
                    int green = pColor.Green > 0 ? (pColor.Green < 1.0 ? (byte)(pColor.Green * 255) : 255) : 0;
                    int blue = pColor.Blue > 0 ? (pColor.Blue < 1.0 ? (byte)(pColor.Blue * 255) : 255) : 0;
                    Color color = Color.FromArgb(red, green, blue);
                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp;
        }

    }
}
