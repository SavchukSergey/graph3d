using System;
using System.Collections.Generic;
using System.Diagnostics;
using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;

namespace Graph3D.Framework.Engines.RayEngines.Shapes {
    public class REShapeComposite : REBaseShape, IEnumerable<REBaseShape> {

        [DebuggerStepThrough]
        public REShapeComposite(Shape3DComposite composite)
            : base(composite) {
        }

        private REBaseShape boundingShape;
        public REBaseShape BoundingShape {
            get { return boundingShape; }
            set { boundingShape = value; }
        }

        private readonly List<REBaseShape> children = new List<REBaseShape>();

        [DebuggerStepThrough]
        public void Add(REBaseShape obj) {
            children.Add(obj);
        }

        [DebuggerStepThrough]
        public void Clear() {
            children.Clear();
        }
        
        #region IEnumerable<ShapeWrapper> Members

        IEnumerator<REBaseShape> IEnumerable<REBaseShape>.GetEnumerator() {
            return children.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

        #endregion

        public override void GetIntersections(ColoredRay3D ray, NearestIntersection intersections) {
            if (boundingShape != null) {
                NearestIntersection holderIntersection = new NearestIntersection();
                boundingShape.GetIntersections(ray, holderIntersection);
                if (holderIntersection.Get() == null) return;
            }

            foreach (REBaseShape obj in children) {
                obj.GetIntersections(ray, intersections);
            }
        }
    }
}
