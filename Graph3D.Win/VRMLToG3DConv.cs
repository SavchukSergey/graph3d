using System.Collections.Generic;
using System.IO;
using Graph3D.Framework;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.VRML;
using Graph3D.VRML.Fields;
using Graph3D.VRML.Nodes;
using Graph3D.VRML.Nodes.Geometry;
using Graph3D.VRML.Nodes.Grouping;
using Graph3D.VRML.Nodes.Bindable;
using Graph3D.VRML.Nodes.LightSources;
using Graph3D.VRML.Nodes.Appearance;

namespace Graph3D.TestConvers {
    public class VRMLToG3DTestConver {

        public VRMLToG3DTestConver() {
        }

        private float[,] transformation;

        public Scene3D TestConv(string vrmlPath) {
            VRML97Tokenizer tokenizer = new VRML97Tokenizer(File.Open(vrmlPath, FileMode.Open));
            VRMLParser parser = new VRMLParser(tokenizer);
            VRMLScene vrml = parser.Parse();
            return TestConv(vrml);
        }

        public Scene3D TestConv(VRMLScene vrml) {
            Scene3D scene = new Scene3D();
            foreach (BaseNode node in vrml.root) {
                List<Object3D> objs = TestConvNode(node);
                foreach (Object3D obj in objs) {
                    if (obj != null) {
                        if (obj is Shape3D) {
                            scene.Objects.Add((Shape3D)obj);
                        }
                    }
                }
            }
            return scene;
        }

        private List<Object3D> TestConvNode(BaseNode node, float[,] transformation) {
            List<Object3D> objects = new List<Object3D>();
            if (node is TransformNode) {
                objects.AddRange(WrapTransform((TransformNode)node, transformation));
                return objects;
            }
            if (node is NavigationInfoNode) {
                return objects;
            }
            if (node is DirectionalLightNode) {
                return objects;
            }
            if (node is ShapeNode) {
                objects.Add(TestConvShape((ShapeNode)node));
                return objects;
            }
            return objects;
        }

        private List<Object3D> WrapTransform(TransformNode node, float[,] transformation) {
            float[,] matrix = node.GenerateTransformMatrix();
            Shape3DComposite shapes = new Shape3DComposite();
            List<Object3D> objects = new List<Object3D>();
            objects.Add(shapes);
            foreach (BaseNode child in node) {
                List<Object3D> t = TestConvNode(child);
                foreach (Object3D obj in t) {
                    if (obj != null) {
                        if (obj is Shape3D) {
                            shapes.Add((Shape3D)obj);
                        } else {
                            objects.Add(obj);
                        }
                    }
                }
            }
            return objects;
        }

        private int triangles = 0;

        private Shape3D TestConvShape(ShapeNode node) {
            AppearanceNode appearance = (AppearanceNode)node.appearance.node;
            MaterialNode material = appearance.material.node as MaterialNode;
            if (node.geometry.node is SphereNode) {
                SphereNode sphereNode = (SphereNode)node.geometry.node;
                Sphere3D sphere = new Sphere3D();
                sphere.Position = new Graph3D.Framework.Math.Vector3D(0, 0, 0);
                sphere.Radius = sphereNode.radius.value;
                SetAppearance(sphere, appearance);
                return sphere;
            }
            if (node.geometry.node is IndexedFaceSetNode) {
                IndexedFaceSetNode faceSetNode = (IndexedFaceSetNode)node.geometry.node;
                Shape3DComposite composite = new Shape3DComposite();
                int facesCount = 0;
                for (int i = 0; i < faceSetNode.coordIndex.length; i++)
                    if (faceSetNode.coordIndex[i] == -1) facesCount++;
                MFVec3f coords = ((CoordinateNode)faceSetNode.coord.node).point;
                for (int faceOffsetIndex = 0; faceOffsetIndex < faceSetNode.coordIndex.length; faceOffsetIndex++) {
                    Triangle3D triangle;
                    triangle = new Triangle3D();
                    SFVec3f a = coords[faceSetNode.coordIndex[faceOffsetIndex]];
                    SFVec3f b = coords[faceSetNode.coordIndex[faceOffsetIndex + 1]];
                    SFVec3f c = coords[faceSetNode.coordIndex[faceOffsetIndex + 2]];
                    triangle.A = new Vector3D(a.x, a.y, a.z);
                    triangle.B = new Vector3D(b.x, b.y, b.z);
                    triangle.C = new Vector3D(c.x, c.y, c.z);
                    SetAppearance(triangle, appearance);
                    composite.Add(triangle);
                    faceOffsetIndex += 3;
                    while (faceSetNode.coordIndex[faceOffsetIndex] != -1) {
                        faceOffsetIndex++;
                    }
                }
                triangles += facesCount;
                return composite;
            }
            return null;
        }

        private void SetAppearance(Shape3D shape, AppearanceNode appearance) {
            shape.Material.DiffuseColor = TestConvColor(((MaterialNode)appearance.material.node).diffuseColor);
            shape.Material.SpecularColor = TestConvColor(((MaterialNode)appearance.material.node).specularColor);
            shape.Material.AmbientIntensity = ((MaterialNode)appearance.material.node).ambientIntensity.value;
            shape.Material.Shininess = ((MaterialNode)appearance.material.node).shininess.value;
            shape.Material.EmmisiveColor = TestConvColor(((MaterialNode)appearance.material.node).emissiveColor);
        }

        private PreciseColor TestConvColor(SFColor color) {
            return new PreciseColor(color.red, color.green, color.blue);
        }

        private Vector3D TestConvVector(SFVec3f vector) {
            return new Vector3D(vector.x, vector.y, vector.z);
        }

    }
}
