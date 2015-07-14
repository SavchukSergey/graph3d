namespace Graph3D.Framework.Shapes {
    public abstract class CustomShape3D : Shape3D {

        public override void AcceptVisitor(IShape3DVisitor visitor) {
            visitor.Visit(this);
        }

    }
}
