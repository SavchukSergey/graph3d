using System.Collections.Generic;

namespace Graph3D.Framework.Lights {
    public class Light3DComposite : Light3D, IEnumerable<Light3D> {

        private readonly List<Light3D> _children = new List<Light3D>();

        public void Add(Light3D obj) {
            _children.Add(obj);
        }

        public void Clear() {
            _children.Clear();
        }

        public override void AcceptVisitor(ILight3DVisitor visitor) {
            visitor.Visit(this);
        }

        #region IEnumerable<Light3D> Members

        IEnumerator<Light3D> IEnumerable<Light3D>.GetEnumerator() {
            return _children.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return _children.GetEnumerator();
        }

        #endregion
    }
}
