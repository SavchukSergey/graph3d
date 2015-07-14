using System.Collections.Generic;
using System.Diagnostics;

namespace Graph3D.Framework.Shapes {
    public class Shape3DComposite : Shape3D, IEnumerable<Shape3D> {

        [DebuggerStepThrough]
        public Shape3DComposite() {
        }

        private readonly List<Shape3D> _children = new List<Shape3D>();

        [DebuggerStepThrough]
        public void Add(Shape3D obj) {
            _children.Add(obj);
        }

        [DebuggerStepThrough]
        public void Clear() {
            _children.Clear();
        }

        public Shape3D this[int index] {
            get { return _children[index]; }
        }

        public override void AcceptVisitor(IShape3DVisitor visitor) {
            visitor.Visit(this);
        }

        #region IEnumerable<Shape3D> Members

        IEnumerator<Shape3D> IEnumerable<Shape3D>.GetEnumerator() {
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
