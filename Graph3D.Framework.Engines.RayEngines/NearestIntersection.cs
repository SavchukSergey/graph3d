namespace Graph3D.Framework.Engines.RayEngines {
    public class NearestIntersection {

        private Intersection _nearest = null;

        public Intersection Get() {
            return _nearest;
        }

        public void Set(Intersection intersection) {
            if (intersection.Length < 0.05) return;
            if (_nearest == null) {
                _nearest = intersection;
                return;
            }
            if (intersection.Length < _nearest.Length) {
                _nearest = intersection;
            }
        }

    }
}
