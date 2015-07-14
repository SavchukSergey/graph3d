namespace Graph3D.Framework.Shapes {
    public interface IShape3DVisitor {

        void Visit(Box3D box);

        void Visit(CustomShape3D shape);

        void Visit(Rectangle3D rect);

        void Visit(Shape3DComposite composite);

        void Visit(Sphere3D sphere);

        void Visit(Triangle3D triangle);

    }
}
