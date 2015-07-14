using System.Collections.Generic;

namespace Graph3D.Framework.Engines.RayEngines {
    public class IntersectionList : IEnumerable<Intersection> {

        private readonly List<Intersection> list = new List<Intersection>();

        public void Add(Intersection intersection) {
            int index = 0;
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Length >= intersection.Length) {
                    break;
                }
                index = i + 1;
            }
            list.Insert(index, intersection);
        }

        public int Count {
            get { return list.Count; }
        }

        public Intersection this[int index] {
            get { return list[index]; }
        }

        #region IEnumerable<Intersection> Members

        IEnumerator<Intersection> IEnumerable<Intersection>.GetEnumerator() {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return list.GetEnumerator();
        }

        #endregion
    }
}
