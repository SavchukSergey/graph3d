using System;
using System.Collections.Generic;
using System.Diagnostics;
using Graph3D.Drawing;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Lights;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Lights {
    public class RELightComposite : REBaseLight, IEnumerable<REBaseLight> {

        [DebuggerStepThrough]
        public RELightComposite(Light3DComposite composite, REScene scene)
            : base(composite, scene) {
        }

        private readonly List<REBaseLight> _children = new List<REBaseLight>();

        [DebuggerStepThrough]
        public void Add(REBaseLight obj) {
            _children.Add(obj);
        }

        [DebuggerStepThrough]
        public void Clear() {
            _children.Clear();
        }

        #region IEnumerable<ShapeWrapper> Members

        IEnumerator<REBaseLight> IEnumerable<REBaseLight>.GetEnumerator() {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

        #endregion


        public override PreciseColor GetSpecularIllumination(Intersection intersection) {
            PreciseColor illumination = new PreciseColor();
            foreach (REBaseLight light in _children) {
                illumination += light.GetSpecularIllumination(intersection);
            }
            return illumination;
        }

        public override PreciseColor GetDiffuseIllumination(Intersection intersection) {
            PreciseColor illumination = new PreciseColor();
            foreach (REBaseLight light in _children) {
                illumination += light.GetDiffuseIllumination(intersection);
            }
            return illumination;
        }

        public override ColoredRay3D IssueRandomRay() {
            if (_children.Count == 0) return null;
            var light = _children[Rnd.Next() % _children.Count];
            return light.IssueRandomRay();
        }
    }
}
