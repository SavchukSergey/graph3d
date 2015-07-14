﻿using Graph3D.Vrml.Fields;

namespace Graph3D.Vrml.Nodes.Interpolation {
    public class CoordinateInterpolatorNode : InterpolatorNode<MFVec3f> {

        public CoordinateInterpolatorNode() {
        }
        
        protected override BaseNode createInstance() {
            return new CoordinateInterpolatorNode();
        }

        public override void acceptVisitor(INodeVisitor visitor) {
            visitor.visit(this);
        }
    }
}
