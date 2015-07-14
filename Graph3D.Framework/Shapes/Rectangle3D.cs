namespace Graph3D.Framework.Shapes {
    public class Rectangle3D : FlatShape3D {

        public override void AcceptVisitor(IShape3DVisitor visitor) {
            visitor.Visit(this);
        }
    }
}
