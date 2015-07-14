namespace Graph3D.Framework.Shapes {
    public class Triangle3D : FlatShape3D {
        
        public override void AcceptVisitor(IShape3DVisitor visitor) {
            visitor.Visit(this);
        }

    }
}
