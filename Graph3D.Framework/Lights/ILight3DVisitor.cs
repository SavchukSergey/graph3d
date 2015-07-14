namespace Graph3D.Framework.Lights {
    public interface ILight3DVisitor {

        void Visit(CustomLight3D light);

        void Visit(Light3DComposite composite);

        void Visit(OmniLight3D omni);

    }
}
