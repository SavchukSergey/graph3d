﻿using Graph3D.Vrml.Fields;

namespace Graph3D.Vrml.Nodes.Interpolation {
    public class ScalarInterpolationNode : InterpolatorNode<MFFloat> {
        
        protected override BaseNode createInstance() {
            return new ScalarInterpolationNode();
        }

        public override void acceptVisitor(INodeVisitor visitor) {
            visitor.visit(this);
        }
    }
}
