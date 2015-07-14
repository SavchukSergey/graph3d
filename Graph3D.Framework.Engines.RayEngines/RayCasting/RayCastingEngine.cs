using System;
using Graph3D.Drawing;
using Graph3D.Framework.Cameras;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Math;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.RayCasting {
    public class RayCastingEngine : Ray3DEngine {

        [Flags]
        protected enum RayTracingOptions {
            Diffuse = 1,
            Speculate
        }

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
                            var reflected = ray;
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
            double d = 2 * camera.FocusDistance * System.Math.Tan(camera.FOV / 2);
            double h = d / System.Math.Sqrt(camera.Ratio * camera.Ratio + 1);
            double w = h * camera.Ratio;

            double hFOV = 2 * System.Math.Atan(w / (2 * camera.FocusDistance));
            double vFOV = 2 * System.Math.Atan(h / (2 * camera.FocusDistance));

            double alpha0 = -hFOV / 2;
            double beta0 = -vFOV / 2;
            double dAlpha = hFOV / canvas.Width;
            double dBeta = vFOV / canvas.Height;

            double alpha = alpha0, beta = beta0;

            var ray = new ColoredRay3D();
            Vector3D dir = new Vector3D();
            dir.Z = (float)camera.FocusDistance;
            for (int cy = 0; cy < canvas.Height; cy++) {
                dir.Y = (float)(System.Math.Tan(beta) * camera.FocusDistance);
                for (int cx = 0; cx < canvas.Width; cx++) {
                    dir.X = (float)(System.Math.Tan(alpha) * camera.FocusDistance);

                    ray.Color = new PreciseColor(1.0f, 1.0f, 1.0f);
                    ray.Start = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z - (float)camera.FocusDistance);
                    ray.End = ray.Start + dir;
                    PreciseColor color = ProcessRay(ray, scene, 0);
                    canvas[cx, cy] += color;
                    alpha += dAlpha;
                }
                alpha = alpha0;
                beta += dBeta;
            }
        }

    }
}
