using OpenTK;

namespace Covid {

    public enum RigidbodyType { Ball = 1}

    public class Rigidbody {

        protected uint collisionMask; 
        public RigidbodyType Type;
        private GameObject gameObject;
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public Vector2 Velocity;
        public bool IsGravityAffected;
        public bool IsCollisionsAffected;

        public Collider Collider
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get { return gameObject.Position; }
            set { gameObject.Position = value; }
        }
        public bool IsActive
        {
            get { return gameObject.IsActive; }
        }

        public Rigidbody (GameObject owner) {
            gameObject = owner;
            PhysicsMgr.AddItem (this);
            IsCollisionsAffected = true;
        }

        public virtual void FixedUpdate () {
            if (IsGravityAffected) {
                Velocity.Y += Game.gravity * Game.Win.DeltaTime;
            }
            Position += Velocity * Game.Win.DeltaTime;
        }

        public bool Collides (Rigidbody other) {
            return Collider.Collides (other.Collider);
        }

        public void AddCollisionType (RigidbodyType type) {
            collisionMask = collisionMask | (uint) type; //--> collisionMask = collisionMask | add;
        }

        public bool CollisionTypeMatches (RigidbodyType type) {
            return (collisionMask & (uint) type) != 0; 
        }
    }
}
