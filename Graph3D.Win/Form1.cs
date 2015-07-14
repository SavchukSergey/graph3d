using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using Graph3D.Drawing;
using Graph3D.Framework;
using Graph3D.Framework.Cameras;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Engines;
using Graph3D.Framework.Engines.RayEngines.RayCasting;
using Graph3D.Framework.Lights;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Win {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private Scene3D _scene = new Scene3D();
        private const int QUALITY = 150;
        private readonly Canvas _canvas = new Canvas(4 * QUALITY, 3 * QUALITY);
        private readonly Graph3DEngine _engine = new RayCastingEngine();
        readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        private void Form1_Load(object sender, EventArgs e) {
            _scene = new Scene3D();

            //PreciseColor color = new PreciseColor(0.6f, 0.3f, 0.3f) * 0.8f;
            //const float shininess = 0;
            //var b = new Box3D {
            //    Material = { DiffuseColor = color, Shininess = shininess },
            //    Width = 40,
            //    Height = 40,
            //    Depth = 120
            //};
            //_scene.Shapes.Add(b);

            _scene.Shapes.Add(new Sphere3D {
                CoordinateSystem = { Position = new Vector3D(0, 14, -2) },
                Radius = 6.0f,
                Material = {
                    DiffuseColor = new PreciseColor(0.0f, 0.1f, 0.0f) * 2.99f,
                    AmbientIntensity = 0.9f,
                    Shininess = 0.1f
                }
            });

            //_scene.Shapes.Add(new Box3D {
            //    CoordinateSystem = { Position = new Vector3D(3, 14, -6) },
            //    Width = 4,
            //    Height = 4,
            //    Depth = 4,
            //    Material = {
            //        DiffuseColor = new PreciseColor(0.0f, 0.0f, 1.0f) * 0.50f,
            //        AmbientIntensity = 0.2f,
            //        Shininess = 0.1f
            //    }
            //});

            const int omniCount = 5;
            for (int i = 0; i < omniCount; i++) {
                float angle = 2 * (float)System.Math.PI * i / omniCount;
                const float radius = 16;
                float x = 0 + radius * (float)System.Math.Cos(angle);
                float y = -19.5f;
                float z = -7 + radius * (float)System.Math.Sin(angle);
                _scene.Lights.Add(new OmniLight3D {
                    Position = new Vector3D(x, y, z),
                    Color = new PreciseColor(1.0f, 1.0f, 1.0f),
                    Power = 1500.0f / omniCount
                });
            }

        }

        void timer_Tick(object sender, EventArgs e) {
            var builder = new BitmapBuilder();
            Bitmap bmp = builder.BuildBitmap(_canvas, 1);
            pictureBox1.Image = bmp;

            pictureBox1.Refresh();
        }

        private void btnRender_Click(object sender, EventArgs e) {
            _timer.Interval = 1000;
            _timer.Tick += timer_Tick;
            _timer.Start();
            btnRender.Enabled = false;

            ThreadStart start = delegate {
                var camera = new GeneralCamera3D {
                    Position = new Vector3D(0, 7, -50),
                    FOV = 60 * System.Math.PI / 180
                };

                _canvas.Clear();
                _engine.Render(_scene, camera, _canvas);
                var builder = new BitmapBuilder();
                Bitmap bmp = builder.BuildBitmap(_canvas, 1);
                bmp.Save(string.Format(@"d:\temp\result.png"), ImageFormat.Png);
            };
            start.BeginInvoke(null, null);

        }

    }
}
