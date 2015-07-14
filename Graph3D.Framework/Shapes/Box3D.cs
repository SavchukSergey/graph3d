namespace Graph3D.Framework.Shapes {
    public class Box3D : Shape3D {
        
        public float Width { get; set; }

        public float Height { get; set; }

        public float Depth { get; set; }

        public override void AcceptVisitor(IShape3DVisitor visitor) {
            visitor.Visit(this);
        }

    }
}
