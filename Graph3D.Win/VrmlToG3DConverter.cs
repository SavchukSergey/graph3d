using System;
using System.Collections.Generic;
using System.IO;
using Graph3D.Drawing;
using Graph3D.Framework;
using Graph3D.Framework.Drawing;
using Graph3D.Framework.Math;
using Graph3D.Framework.Shapes;
using Graph3D.Math;
using Graph3D.Vrml;
using Graph3D.Vrml.Fields;
using Graph3D.Vrml.Nodes;
using Graph3D.Vrml.Nodes.Appearance;
using Graph3D.Vrml.Nodes.Bindable;
using Graph3D.Vrml.Nodes.Geometry;
using Graph3D.Vrml.Nodes.Grouping;
using Graph3D.Vrml.Nodes.LightSources;
using Graph3D.Vrml.Parser;
using Graph3D.Vrml.Tokenizer;

namespace Graph3D.Win {
    public class VrmlToG3DConverter {

        private int triangles = 0;

        public Scene3D Convert(string vrmlPath) {
            try {
                Vrml97Tokenizer tokenizer = new Vrml97Tokenizer(File.Open(vrmlPath, FileMode.Open));
                VrmlParser parser = new VrmlParser(tokenizer);
                VRMLScene scene = new VRMLScene();
                parser.parse(scene);
                return Convert(scene);
            } catch (Exception exc) {
                return null;
            }
        }

        public Scene3D Convert(VRMLScene vrml) {
            Scene3D scene = new Scene3D();
            float[,] transformation = VrmlMath.GetUnitMatrix();
            foreach (Node node in vrml.root.children) {
                List<Object3D> objs = ConvertNode(node, transformation);
                foreach (Object3D obj in objs) {
                    if (obj != null) {
                        if (obj is Shape3D) {
                            scene.Shapes.Add((Shape3D)obj);
                        }
                    }
                }
            }
            return scene;
        }

        private List<Object3D> ConvertNode(Node node, float[,] transformation) {
            List<Object3D> objects = new List<Object3D>();
            if (node is GroupingNode) {
                objects.AddRange(ConvertGroupingNode((GroupingNode)node, transformation));
                return objects;
            }
            if (node is NavigationInfoNode) {
                return objects;
            }
            if (node is DirectionalLightNode) {
                return objects;
            }
            if (node is ShapeNode) {
                objects.Add(ConvertShapeNode((ShapeNode)node, transformation));
                return objects;
            }
            return objects;
        }

        private List<Object3D> ConvertGroupingNode(GroupingNode node, float[,] transformation) {
            if (node is TransformNode) {
                float[,] matrix = ((TransformNode)node).GenerateTransformMatrix();
                VrmlMath.ConcatenateMatrixes(transformation, matrix, matrix);
                transformation = matrix;
            }
            Shape3DComposite shapes = new Shape3DComposite();
            List<Object3D> objects = new List<Object3D>();
            objects.Add(shapes);
            foreach (Node child in node.children) {
                List<Object3D> t = ConvertNode(child, transformation);
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

        private Shape3D ConvertShapeNode(ShapeNode node, float[,] transformation) {
            AppearanceNode appearance = (AppearanceNode)node.appearance.node;
            MaterialNode material = appearance.material.node as MaterialNode;
            if (node.geometry.node is SphereNode) {
                SphereNode sphereNode = (SphereNode)node.geometry.node;
                Sphere3D sphere = new Sphere3D();
                //sphere.Position = new Graph3D.Framework.Math.Vector3D(0, 0, 0);
                sphere.Radius = sphereNode.radius.value;
                SetAppearance(sphere, appearance);
                return sphere;
            }
            if (node.geometry.node is BoxNode) {
                return null;
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
                    triangle.A = ConvertVector3D(a, transformation);
                    triangle.B = ConvertVector3D(b, transformation);
                    triangle.C = ConvertVector3D(c, transformation);
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
            shape.Material.DiffuseColor = ConvertColor(((MaterialNode)appearance.material.node).diffuseColor) * 7;
            shape.Material.SpecularColor = ConvertColor(((MaterialNode)appearance.material.node).specularColor);
            shape.Material.EmmisiveColor = ConvertColor(((MaterialNode)appearance.material.node).emissiveColor);
            shape.Material.AmbientIntensity = ((MaterialNode)appearance.material.node).ambientIntensity.value;
            shape.Material.Shininess = ((MaterialNode)appearance.material.node).shininess.value;
        }

        private PreciseColor ConvertColor(SFColor color) {
            return new PreciseColor(color.red, color.green, color.blue);
        }

        private Vector3D ConvertVector3D(SFVec3f vector) {
            return new Vector3D(vector.x, vector.y, vector.z);
        }

        private Vector3D ConvertVector3D(SFVec3f vector, float[,] transformation) {
            float[] transformed = VrmlMath.TransformVector(vector.x, vector.y, vector.z, transformation);
            return new Vector3D(transformed[0], transformed[1], transformed[2]);
        }

    }
}
