using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Math;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines {
    public static class RayEngineMath {

        public static void GetIntersections(ColoredRay3D ray, REFlatShape flatShape, NearestIntersection intersections) {

            /* Math for float shapes like triangle or rectangle
             * __  ___              ___
             * Ro, Dir  - the ray, |Dir| = 1
               _  _  _
             * A, B, C - the triangle
             * 
             * Equations
             *      _            ___
             * Ray: P = Ro + t * Dir
             *           _   _       _       _       _
             * Triangle: P = U * u + V * v + W * w + A, _    _   _
             *                                          U = (B - A)
             *                                          _    _   _
             *                                          V = (C - A)
             *                                          _   _   _
             *                                          W = U x V
             *                                          
             *                                          w = 0
             * _   _   _
             * D = P - A
             * D.x = U.x * u + V.x * v + W.x * w
             * D.y = U.y * u + V.y * v + W.y * w
             * D.z = U.z * u + V.z * v + W.z * w 
             * 
             *        | U.x V.x W.x |
             * det =  | U.y V.y W.y |
             *        | U.z V.z W.z |
             *        
             *        | D.x V.x W.x |   _    _   _
             * detU = | D.y V.y W.y | = D * (V x W)
             *        | D.z V.z W.z |
             *        
             *        | U.x D.x W.x |   _    _   _
             * detV = | U.y D.y W.y | = D * (W x U)
             *        | U.z D.z W.z |
             *        
             *        | U.x V.x D.x |   _    _   _    _   _
             * detW = | U.y V.y D.y | = D * (U x V) = D * W
             *        | U.z V.z D.z |
             *            _    _   _         _   _
             *     detU   D * (V x W)   _    V x W    _   __
             * u = ---- = ----------- = D * (-----) = D * Tu
             *     det        det             det
             *            _    _   _         _   _
             *     detV   D * (W x U)   _    W x U    _   __
             * v = ---- = ----------- = D * (-----) = D * Tv
             *     det        det             det
             *            _    _   _         _   _
             *     detW   D * (U x V)   _    U x V    _   __
             * w = ---- = ----------- = D * (-----) = D * Tw
             *     det        det             det
             *     
             * w = 0
             * _   __
             * D * Tw = 0
             *  _   _    __
             * (P - A) * Tw = 0
             * _   __   _   __
             * P * Tw = A * Tw
             *  __       ___    __   _   __
             * (Ro + t * Dir) * Tw = A * Tw
             *     ___   __    _   __    __
             * t * Dir * Tw = (A - Ro) * Tw
             *      _   __    __
             *     (A - Ro) * Tw
             * t = -===---==----
             *      Dir * Tw
             * */

            //Vector3D ro = ray.CoordinateSystem.TransformVector(ray.Start, flatShape.Origin.CoordinateSystem);
            //Vector3D re = ray.CoordinateSystem.TransformVector(ray.End, flatShape.Origin.CoordinateSystem);
            Vector3D ro = ray.Start;
            Vector3D re = ray.End;
            Vector3D dir = re - ro;

            float k = Vector3D.Scalar(dir, flatShape.Tw);
            if (k == 0) return;

            float t = Vector3D.Scalar(flatShape.A - ro, flatShape.Tw) / k;
            if (t < 0) return;

            var point = ro + dir*t;
            Vector3D d = point - flatShape.A;

            float u = Vector3D.Scalar(d, flatShape.Tu);
            float v = Vector3D.Scalar(d, flatShape.Tv);

            if (!flatShape.ValidateTexture(u, v)) return;

            var intersection = new Intersection(u, v, 0) {
                Length = t,
                Normal = k < 0 ? flatShape.Normal : flatShape.Normal * -1,
                Shape3D = flatShape,
                Point = point,
                Ray = ray
            };

            intersections.Set(intersection);
        }

        public static void GetIntersections(ColoredRay3D ray, RESphere sphere, NearestIntersection intersections) {
            /*
             * __  ___              ___
             * Ro, Dir  - the ray, |Dir| = 1
             * __  
             * So, R - Sphere
             * 
             * Equations
             *      _            ___
             * Ray: P = Ro + t * Dir
             *          __   _   __   _
             * Sphere: (So - P)*(So - P) = R*R
             *  __    __     ___    __    __       ___
             * (So - (Ro + t*Dir))*(So - (Ro + t*Dir)) = R*R
             *   __   __      ___    __   __      ___
             * ((So - Ro) - t*Dir)*((So - Ro) - t*Dir) = R*R
             * _ _     _ ___     ___ ___                          _    __   __
             * C*C - 2*C*Dir*t + Dir*Dir*t*t = R*R,               C = (So - Ro)
             *               _ _                                  ___           _ ___
             * t*t - 2*k*t + C*C - R*R = 0,                      |Dir| = 1, k = C*Dir
             *                   _ _
             * det' = 4*k*k - 4*(C*C - R*R)
             *              _ _
             * det = k*k - (C*C - R*R)
             *           
             *        2*k +/- sqrt(det')
             * t1,2 = -------------------------
             *                 2
             *                                                             
             * t1,2 = k +/- sqrt(det)
             * 
             * */
            //Vector3D ro = ray.CoordinateSystem.TransformVector(ray.Start, flatShape.Origin.CoordinateSystem);
            //Vector3D re = ray.CoordinateSystem.TransformVector(ray.End, flatShape.Origin.CoordinateSystem);
            Vector3D ro = ray.Start;
            Vector3D re = ray.End;
            Vector3D c = sphere.Position - ro;
            Vector3D dir = re - ro;
            float k = Vector3D.Scalar(dir, c);
            float det = k * k - Vector3D.Scalar(c, c) + sphere.Radius2;
            if (det > 0) {
                det = (float)System.Math.Sqrt(det);
                float t1 = k - det;
                float t2 = k + det;
                if (t1 > 0) {
                    var first = new Intersection(0, 0, 0) {
                        Shape3D = sphere,
                        Ray = ray,
                        Length = t1,
                        Point = ro + dir * t1
                    };
                    first.Normal = (first.Point - sphere.Position) / sphere.Radius;
                    intersections.Set(first);
                }
                if (t2 > 0) {
                    var second = new Intersection(0, 0, 0) {
                        Shape3D = sphere,
                        Ray = ray,
                        Length = t2,
                        Point = ro + dir * t2
                    };
                    second.Normal = (second.Point - sphere.Position) / sphere.Radius;
                    intersections.Set(second);
                }
            }
        }
    }
}
