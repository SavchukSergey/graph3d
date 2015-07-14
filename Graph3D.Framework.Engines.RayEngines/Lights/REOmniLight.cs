using Graph3D.Drawing;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Lights;
using Graph3D.Framework.Math;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines.Lights {
    public class REOmniLight : REBaseLight {

        public REOmniLight(OmniLight3D origin, Vector3D position, REScene scene)
            : base(origin, scene) {
            omni = origin;
            this.position = position;
        }

        private readonly OmniLight3D omni;

        private readonly Vector3D position;

        public override PreciseColor GetSpecularIllumination(Intersection intersection) {
            return new PreciseColor();
            //Material material = intersection.Shape3D.Origin.Material;
            //PreciseColor illumination = new PreciseColor();

            //Vector3D dir = omni.Position - intersection.Point;
            //bool shadowed = false;
            //Intersection obstacle = intersections.Get();
            //if (obstacle != null && obstacle.Length > 0.05 && obstacle.Length < distance) {
            //    shadowed = true;
            //}

            //if (!shadowed) {
            //    PreciseColor diffuseColor = new PreciseColor(omni.Color.Red * material.DiffuseColor.Red,
            //                                omni.Color.Green * material.DiffuseColor.Green,
            //                                omni.Color.Blue * material.DiffuseColor.Blue);
            //    float diffuseIntensity = omni.Power * System.Math.Abs(Vector3D.Scalar(dir.Normalize(), intersection.Normal));
            //    diffuseIntensity /= dir.Length * dir.Length;
            //    illumination += diffuseColor * diffuseIntensity;
            //}
            //return illumination;
        }

        public override PreciseColor GetDiffuseIllumination(Intersection intersection) {
            Material material = intersection.Shape3D.Material;
            Vector3D dir = position - intersection.Point;

            float distance = dir.Length;

            var ray = new ColoredRay3D { Start = intersection.Point, End = position };
            NearestIntersection intersections = GetIntersections(ray);
            bool shadowed = false;
            Intersection obstacle = intersections.Get();
            if (obstacle != null && obstacle.Length > 0.05 && obstacle.Length < distance) {
                shadowed = true;
            }

            if (!shadowed) {
                var diffuseColor = new PreciseColor(omni.Color.Red * material.DiffuseColor.Red,
                                            omni.Color.Green * material.DiffuseColor.Green,
                                            omni.Color.Blue * material.DiffuseColor.Blue);
                float diffuseIntensity = omni.Power * (1 - material.Shininess) * System.Math.Abs(Vector3D.Scalar(dir.Normalize(), intersection.Normal));
                diffuseIntensity /= dir.Length * dir.Length;

                Vector3D reflected = Math3D.GetReflectedVector((intersection.Point - position).Normalize(), intersection.Normal);
                float cosTeta = Vector3D.Scalar(reflected, (intersection.Ray.Start - intersection.Ray.End).Normalize());
                float specularIntensity = cosTeta < 0 ? 0 : omni.Power * material.Shininess * (float)System.Math.Pow(cosTeta, 200);
                if (intersection.Shape3D is RESphere) {
                    specularIntensity /= material.Shininess;
                }
                specularIntensity *= 0.05f;

                return diffuseColor * diffuseIntensity + material.SpecularColor * specularIntensity;
            }
            return new PreciseColor();
        }

        public override ColoredRay3D IssueRandomRay() {
            return new ColoredRay3D {
                Color = omni.Color,
                Start = omni.Position,
                End = omni.Position + Rnd.Vector()
            };
        }
    }
}
