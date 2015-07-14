using System;
using Graph3D.Drawing;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Lights;

namespace Graph3D.Framework.Engines.RayEngines.Lights {
    public abstract class REBaseLight {

        protected REBaseLight(Light3D origin, REScene scene) {
            this.origin = origin;
            this.scene = scene;
        }

        private readonly Light3D origin;
        public Light3D Origin {
            get { return origin; }
        }

        private readonly REScene scene;

        protected virtual NearestIntersection GetIntersections(ColoredRay3D ray) {
            NearestIntersection intersections = new NearestIntersection();
            ray.End = ray.Start + (ray.End - ray.Start).Normalize();
            scene.Objects.GetIntersections(ray, intersections);
            return intersections;
        }

        public abstract PreciseColor GetSpecularIllumination(Intersection intersection);

        public abstract PreciseColor GetDiffuseIllumination(Intersection intersection);

        //public static PreciseColor GetRGBIntensity(Intersection intersection, RELightComposite lights) {
        //    PreciseColor iRGB = new PreciseColor();
        //    PreciseColor iFogRGB = new PreciseColor();
        //    float fogInterpolant = 0.0f;
        //    iRGB += iFogRGB * (1 - fogInterpolant);
        //    PreciseColor emissiveColor = intersection.Shape3D.Origin.Material.EmmisiveColor;
        //    PreciseColor sum = new PreciseColor();
        //    foreach (REBaseLight light in lights) {
        //        if (light.Origin.Enabled) {
        //            float attenuation = 1.0f;
        //            float spotLightFactor = 1.0f;
        //            float ambient = 0.0f;
        //            float diffuse = 0.0f;
        //            float specular = 0.0f;
        //            sum += light.Origin.Color * attenuation * spotLightFactor * (ambient + diffuse + specular);

        //        }
        //    }
        //    return iRGB;
        //}

        public abstract ColoredRay3D IssueRandomRay();

    }
}
