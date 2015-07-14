using System;
using Graph3D.Drawing;
using Graph3D.Framework.Cameras;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Engines.RayEngines.Lights;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Math;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.RayTracing {
    public class RayTracingEngine : Ray3DEngine {

        [Flags]
        protected enum RayTracingOptions {
            Diffuse = 1,
            Speculate
        }

        private readonly Random _rnd = new Random(DateTime.Now.Millisecond);

        protected virtual PreciseColor ProcessRay(ColoredRay3D ray, REScene scene, int level) {
            NearestIntersection intersections = GetIntersections(ray, scene);
            var color = new PreciseColor();
            Vector3D dir = (ray.End - ray.Start).Normalize();
            Intersection intersection = intersections.Get();
            if (intersection != null) {
                if (intersection.Length > 0.05) {
                    Material material = intersection.Shape3D.Material;
                    color += material.DiffuseColor * material.AmbientIntensity;
                    RayTracingOptions options = GetRayTracingOptions(intersection.Shape3D);
                    if ((options & RayTracingOptions.Diffuse) > 0) {
                        color += GetDiffuseIllumination(intersection, scene);
                    }
                    if ((options & RayTracingOptions.Speculate) > 0) {

                        if (level < 5 && material.Shininess > 0.001) {
                            ColoredRay3D reflected = ray;
                            Vector3D reflectedStart = ray.Start + dir * intersection.Length;
                            Vector3D reflectedEnd = reflectedStart + Math3D.GetReflectedVector(dir, intersection.Normal);
                            reflected.Start = reflectedStart;
                            reflected.End = reflectedEnd;
                            color += ProcessRay(reflected, scene, level + 1) * material.Shininess;
                        }
                    }
                }
            }
            return color;
        }

        protected virtual RayTracingOptions GetRayTracingOptions(REBaseShape shape) {
            return RayTracingOptions.Diffuse | RayTracingOptions.Speculate;
        }


        protected override void Render(REScene scene, Camera3D camera, Canvas canvas) {
            foreach (REBaseLight lightSource in scene.Lights) {
                var start = lightSource.Origin.CoordinateSystem.Position;
                var dir = new Vector3D(
                    (float)(_rnd.NextDouble() * 2 - 1),
                    (float)(_rnd.NextDouble() * 2 - 1),
                    (float)(_rnd.NextDouble() * 2 - 1)
                ).Normalize();
                var ray = new ColoredRay3D {
                    Color = new PreciseColor(1, 1, 1),
                    Start = start,
                    End = start + dir
                };
                ProcessRay(ray, scene, 0);
            }
        }
    }
}
