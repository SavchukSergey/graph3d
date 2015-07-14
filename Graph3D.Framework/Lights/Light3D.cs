namespace Graph3D.Framework.Lights {
    public abstract class Light3D : Object3D {

        private bool _enabled = true;
        public bool Enabled {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public abstract void AcceptVisitor(ILight3DVisitor visitor);

    }
}
