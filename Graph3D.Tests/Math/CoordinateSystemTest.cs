using Graph3D.Math;
using NUnit.Framework;

namespace Graph3D.Tests.Math {

    [TestFixture]
    public class CoordinateSystemTest {

        [Test]
        public void ScaleTest() {
            var cs = new CoordinateSystem();
            cs.Scale(2, 5, 7);
            Assert.AreEqual(new Vector3D(2, 0, 0), cs.U);
            Assert.AreEqual(new Vector3D(0, 5, 0), cs.V);
            Assert.AreEqual(new Vector3D(0, 0, 7), cs.W);
        }

        [Test]
        public void Shift() {
            var cs = new CoordinateSystem();
            cs.Shift(2, 5, 7);
            Assert.AreEqual(new Vector3D(2, 5, 7), cs.Position);
        }

        [Test]
        public void DoubleConversionTest() {
            var cs = new CoordinateSystem {
                U = new Vector3D(1, 5, 2),
                V = new Vector3D(10, -2, 4),
                W = new Vector3D(3, 4, -2),
                Position = new Vector3D(100, 50, 20)
            };
            var localPoint = new Vector3D(-1, 5, 7);
            var globalPoint = cs.ToAbsolute(localPoint);

            var csReversed = cs.ToReverse();
            var local = csReversed.ToAbsolute(globalPoint);

            const double epsilon = 0.000001;
            Assert.AreEqual(localPoint.X, local.X, epsilon);
            Assert.AreEqual(localPoint.Y, local.Y, epsilon);
            Assert.AreEqual(localPoint.Z, local.Z, epsilon);
        }
    }
}
