using System.Collections.Generic;

namespace Graph3D.Framework {
    public class Object3DComposite : Object3D, IEnumerable<Object3D> {
        
        private readonly List<Object3D> _children = new List<Object3D>();

        public void Add(Object3D obj) {
            _children.Add(obj);
        }

        public void Clear() {
            _children.Clear();
        }

        #region IEnumerable<Object3D> Members

        IEnumerator<Object3D> IEnumerable<Object3D>.GetEnumerator() {
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
