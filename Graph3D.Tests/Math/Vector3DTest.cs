using Graph3D.Drawing;
using Graph3D.Math;
using NUnit.Framework;

namespace Graph3D.Tests.Math {
    [TestFixture]
    public class Vector3DTest {

        [Test]
        public void AddTest() {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(-2, -4, -6);
            var c = a + b;
            Assert.AreEqual(new Vector3D(-1, -2, -3), c);
        }
    }
}
