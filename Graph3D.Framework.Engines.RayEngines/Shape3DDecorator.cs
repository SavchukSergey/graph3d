using System;
using System.Collections.Generic;
using Graph3D.Framework.Engines.RayEngines.Lights;
using Graph3D.Framework.Engines.RayEngines.Shapes;
using Graph3D.Framework.Lights;
using Graph3D.Framework.Shapes;
using Graph3D.Math;

namespace Graph3D.Framework.Engines.RayEngines {
    public class Shape3DDecorator : IShape3DVisitor, ILight3DVisitor {

        private readonly RenderPreparationContext _context;

        public Shape3DDecorator(RenderPreparationContext context) {
            _context = context;
        }

        public REBaseShape DecoratedShape { get; private set; }

        public REBaseLight DecoratedLight { get; private set; }

        private Vector3D ToAbsolute(in Vector3D source) {
            return _context.ToAbsolute(in source);
        }

        #region IShape3DVisitor Members

        public void Visit(Box3D box) {
            Shape3DComposite composite = new Shape3DComposite();
            composite.CoordinateSystem = box.CoordinateSystem;

            float dx = box.Width / 2;
            float dy = box.Height / 2;
            float dz = box.Depth / 2;

            var top = new Rectangle3D {
                A = new Vector3D(-dx, -dy, -dz),
                B = new Vector3D(-dx, -dy, +dz),
                C = new Vector3D(+dx, -dy, -dz),
                Material = box.Material
            };

            var bottom = new Rectangle3D {
                A = new Vector3D(-dx, +dy, -dz),
                B = new Vector3D(-dx, +dy, +dz),
                C = new Vector3D(+dx, +dy, -dz),
                Material = box.Material
            };

            var left = new Rectangle3D {
                A = new Vector3D(-dx, -dy, -dz),
                B = new Vector3D(-dx, +dy, -dz),
                C = new Vector3D(-dx, -dy, +dz),
                Material = box.Material
            };

            var right = new Rectangle3D {
                A = new Vector3D(+dx, -dy, -dz),
                B = new Vector3D(+dx, +dy, -dz),
                C = new Vector3D(+dx, -dy, +dz),
                Material = box.Material
            };

            var front = new Rectangle3D {
                A = new Vector3D(-dx, -dy, -dz),
                B = new Vector3D(-dx, +dy, -dz),
                C = new Vector3D(+dx, -dy, -dz),
                Material = box.Material
            };

            var back = new Rectangle3D {
                A = new Vector3D(-dx, -dy, +dz),
                B = new Vector3D(-dx, +dy, +dz),
                C = new Vector3D(+dx, -dy, +dz),
                Material = box.Material
            };

            composite.Add(top);
            composite.Add(bottom);
            composite.Add(left);
            composite.Add(right);
            composite.Add(front);
            composite.Add(back);

            Visit(composite);
        }

        public void Visit(CustomShape3D shape) {
            DecoratedShape = null;
        }

        public void Visit(Rectangle3D rect) {
            var globalRectangle = new Rectangle3D {
                A = ToAbsolute(rect.A),
                B = ToAbsolute(rect.B),
                C = ToAbsolute(rect.C),
                Material = rect.Material
            };
            DecoratedShape = new RERectangle(globalRectangle);
        }

        public void Visit(Shape3DComposite composite) {
            _context.PushCoordinateSystem(composite.CoordinateSystem);
            var decorated = new REShapeComposite(composite);
            foreach (Shape3D child in composite) {
                child.AcceptVisitor(this);
                decorated.Add(DecoratedShape);
            }
            OptimizeComposite(decorated);
            _context.PopCoordinateSystem();
            DecoratedShape = decorated;
        }

        public void Visit(Sphere3D sphere) {
            DecoratedShape = new RESphere(sphere);
        }

        public void Visit(Triangle3D triangle) {
            var globalTriangle = new Triangle3D {
                A = ToAbsolute(triangle.A),
                B = ToAbsolute(triangle.B),
                C = ToAbsolute(triangle.C),
                Material = triangle.Material
            };
            DecoratedShape = new RETriangle(globalTriangle);
        }

        #endregion


        #region ILight3DVisitor Members

        public void Visit(CustomLight3D light) {
            throw new NotImplementedException();
        }

        public void Visit(Light3DComposite composite) {
            _context.PushCoordinateSystem(composite.CoordinateSystem);
            RELightComposite decorated = new RELightComposite(composite, _context.Scene);
            foreach (Light3D child in composite) {
                child.AcceptVisitor(this);
                decorated.Add(this.DecoratedLight);
            }
            _context.PopCoordinateSystem();
            DecoratedLight = decorated;
        }

        public void Visit(OmniLight3D omni) {
            DecoratedLight = new REOmniLight(omni, ToAbsolute(omni.Position), _context.Scene);
        }

        #endregion

        protected virtual void OptimizeComposite(REShapeComposite composite) {
            var points = new List<Vector3D>();
            foreach (REBaseShape child in composite) {
                if (child is RETriangle) {
                    RETriangle triangle = (RETriangle)child;
                    points.Add(triangle.A);
                    points.Add(triangle.B);
                    points.Add(triangle.C);
                    continue;
                }
                if (child is RERectangle) {
                    var rectangle = (RERectangle)child;
                    points.Add(rectangle.A);
                    points.Add(rectangle.B);
                    points.Add(rectangle.C);
                    points.Add(rectangle.B + rectangle.C - rectangle.A);
                    continue;
                }
            }

            List<Vector3D> prev = points;
            for (int i = 0; i < 0; i++) {
                List<Vector3D> additional = GetAdditionalPoints(prev, composite);
                points.AddRange(additional);
                prev = additional;
            }

            Vector3D a = new Vector3D(), b = new Vector3D();
            float maxLength = float.MinValue;
            for (int i = 0; i < points.Count; i++) {
                for (int j = 0; j < points.Count; j++) {
                    float length = (points[i] - points[j]).Length;
                    if (length > maxLength) {
                        a = points[i];
                        b = points[j];
                        maxLength = length;
                    }
                }

            }

            if ((a - b).Length > 0.01) {
                Sphere3D holder = new Sphere3D();
                Vector3D center = (a + b) * 0.5f;
                holder.CoordinateSystem.Position = center;
                holder.Radius = (a - b).Length / 2;
                composite.BoundingShape = new RESphere(holder);
            }

        }

        protected List<Vector3D> GetAdditionalPoints(List<Vector3D> points, REShapeComposite composite) {
            List<Vector3D> additional = new List<Vector3D>();
            foreach (REBaseShape child in composite) {
                RESphere sphere = child as RESphere;
                if (child is REShapeComposite) {
                    sphere = ((REShapeComposite)child).BoundingShape as RESphere;
                }
                if (sphere == null) continue;
                for (int i = 0; i < points.Count; i++) {
                    Vector3D c = points[i];
                    Vector3D dir = sphere.Position - c;
                    additional.Add(sphere.Position + dir.Normalize() * sphere.Radius);
                }
            }
            return additional;
        }
    }
}
