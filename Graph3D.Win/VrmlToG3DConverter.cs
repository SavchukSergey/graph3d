﻿using System;
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
                var tokenizer = new Vrml97Tokenizer(File.Open(vrmlPath, FileMode.Open));
                var parser = new VrmlParser(tokenizer);
                var scene = new VrmlScene();
                parser.Parse(scene);
                return Convert(scene);
            } catch (Exception exc) {
                return null;
            }
        }

        public Scene3D Convert(VrmlScene vrml) {
            Scene3D scene = new Scene3D();
            float[,] transformation = VrmlMath.GetUnitMatrix();
            foreach (Node node in vrml.Root.Children) {
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
            var shapes = new Shape3DComposite();
            var objects = new List<Object3D> {
                shapes
            };
            foreach (Node child in node.Children) {
                var t = ConvertNode(child, transformation);
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
            var appearance = node.Appearance;
            MaterialNode material = appearance.Material.Node as MaterialNode;
            if (node.Geometry is SphereNode sphereNode) {
                var sphere = new Sphere3D {
                    //sphere.Position = new Graph3D.Framework.Math.Vector3D(0, 0, 0);
                    Radius = sphereNode.Radius.Value
                };
                SetAppearance(sphere, appearance);
                return sphere;
            }
            if (node.Geometry is BoxNode) {
                return null;
            }
            if (node.Geometry is IndexedFaceSetNode faceSetNode) {
                var composite = new Shape3DComposite();
                int facesCount = 0;
                for (int i = 0; i < faceSetNode.CoordIndex.Length; i++) {
                    if (faceSetNode.CoordIndex[i] == -1) facesCount++;
                }
                var coords = ((CoordinateNode)faceSetNode.Coord.Node).Point;
                for (int faceOffsetIndex = 0; faceOffsetIndex < faceSetNode.CoordIndex.Length; faceOffsetIndex++) {
                    var triangle = new Triangle3D();
                    var a = coords[faceSetNode.CoordIndex[faceOffsetIndex]];
                    var b = coords[faceSetNode.CoordIndex[faceOffsetIndex + 1]];
                    var c = coords[faceSetNode.CoordIndex[faceOffsetIndex + 2]];
                    triangle.A = ConvertVector3D(a, transformation);
                    triangle.B = ConvertVector3D(b, transformation);
                    triangle.C = ConvertVector3D(c, transformation);
                    SetAppearance(triangle, appearance);
                    composite.Add(triangle);
                    faceOffsetIndex += 3;
                    while (faceSetNode.CoordIndex[faceOffsetIndex] != -1) {
                        faceOffsetIndex++;
                    }
                }
                triangles += facesCount;
                return composite;
            }
            return null;
        }

        private void SetAppearance(Shape3D shape, AppearanceNode appearance) {
            shape.Material.DiffuseColor = ConvertColor(((MaterialNode)appearance.Material.Node).DiffuseColor) * 7;
            shape.Material.SpecularColor = ConvertColor(((MaterialNode)appearance.Material.Node).SpecularColor);
            shape.Material.EmmisiveColor = ConvertColor(((MaterialNode)appearance.Material.Node).EmissiveColor);
            shape.Material.AmbientIntensity = ((MaterialNode)appearance.Material.Node).AmbientIntensity.Value;
            shape.Material.Shininess = ((MaterialNode)appearance.Material.Node).Shininess.Value;
        }

        private PreciseColor ConvertColor(SFColor color) {
            return new PreciseColor(color.Red, color.Green, color.Blue);
        }

        private Vector3D ConvertVector3D(SFVec3f vector) {
            return new Vector3D(vector.X, vector.Y, vector.Z);
        }

        private Vector3D ConvertVector3D(SFVec3f vector, float[,] transformation) {
            float[] transformed = VrmlMath.TransformVector(vector.X, vector.Y, vector.Z, transformation);
            return new Vector3D(transformed[0], transformed[1], transformed[2]);
        }

    }
}
