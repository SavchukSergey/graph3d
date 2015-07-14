namespace Graph3D.Framework.Lights {
    public abstract class CustomLight3D : Light3D {

        public override void AcceptVisitor(ILight3DVisitor visitor) {
            visitor.Visit(this);
        }

    }
}
