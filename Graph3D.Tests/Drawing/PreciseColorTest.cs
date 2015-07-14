using Graph3D.Drawing;
using NUnit.Framework;

namespace Graph3D.Tests.Drawing {
    [TestFixture]
    public class PreciseColorTest {

        [Test]
        public void AddTest() {
            var a = new PreciseColor(1, 2, 3);
            var b = new PreciseColor(-2, -4, -6);
            var c = a + b;
            Assert.AreEqual(new PreciseColor(-1, -2, -3), c);
        }
    }
}
