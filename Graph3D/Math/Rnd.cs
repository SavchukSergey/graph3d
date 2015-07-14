using System;

namespace Graph3D.Math {
    public static class Rnd {

        private static readonly Random _rnd = new Random(DateTime.Now.Millisecond);

        public static float NextFloat() {
            return (float)_rnd.NextDouble();
        }

        public static float NextSignedFloat() {
            return (float)(2 * _rnd.NextDouble() - 1);
        }

        public static Vector3D Vector() {
            return new Vector3D(NextSignedFloat(), NextSignedFloat(), NextSignedFloat()).Normalize();
        }

        public static int Next() {
            return _rnd.Next();
        }
    }
}
