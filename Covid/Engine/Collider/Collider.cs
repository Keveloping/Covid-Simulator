using OpenTK;


namespace Covid {
    public abstract class Collider {

        public Vector2 Offset;
        public Rigidbody Rigidbody
        {
            get;
            protected set;
        }

        public Vector2 Position
        {
            get { return Rigidbody.Position + Offset; }
        }

        public Collider (Rigidbody owner, Vector2 offset) {
            this.Offset = offset;
            Rigidbody = owner;
        }

        public abstract bool Collides (Collider collider);

        public abstract bool Collides (CircleCollider circle);

        public abstract bool Collides (BoxCollider box);

        public abstract bool Contains (Vector2 point);
    }
}
