using Graph3D.Drawing;
using Graph3D.Framework.Cameras;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Math;

namespace Graph3D.Framework.Engines.RayEngines {
    public abstract class Ray3DEngine : Graph3DEngine {

        protected virtual NearestIntersection GetIntersections(ColoredRay3D ray, REScene scene) {
            var intersections = new NearestIntersection();
            ray.End = ray.Start + (ray.End - ray.Start).Normalize();
            scene.Objects.GetIntersections(ray, intersections);
            return intersections;
        }

        public sealed override void Render(Scene3D scene, Camera3D camera, Canvas canvas) {
            REScene wrappedScene = PrepareScene(scene, camera);
            Render(wrappedScene, camera, canvas);
        }

        #region Scene Preparation

        private Shape3DDecorator _shapeDecorator;

        protected virtual REScene PrepareScene(Scene3D scene, Camera3D camera) {
            var wrappedScene = new REScene();
            var preparationContext = new RenderPreparationContext(wrappedScene);
            preparationContext.PushCoordinateSystem(camera.CoordinateSystem);
            _shapeDecorator = new Shape3DDecorator(preparationContext);

            
            scene.Shapes.AcceptVisitor(_shapeDecorator);
            wrappedScene.Objects.Add(_shapeDecorator.DecoratedShape);
            
            scene.Lights.AcceptVisitor(_shapeDecorator);
            wrappedScene.Lights.Add(_shapeDecorator.DecoratedLight);
            
            preparationContext.PopCoordinateSystem();
            return wrappedScene;
        }

        #endregion

        protected virtual PreciseColor GetDiffuseIllumination(Intersection intersection, REScene scene) {
            return scene.Lights.GetDiffuseIllumination(intersection);
        }

        protected virtual PreciseColor GetSpecularIllumination(Intersection intersection, REScene scene) {
            return scene.Lights.GetSpecularIllumination(intersection);
        }

        protected abstract void Render(REScene scene, Camera3D camera, Canvas canvas);
    }
}
