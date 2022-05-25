using Aiv.Fast2D;
using OpenTK;

namespace Covid {
    public abstract class GameObject : IUpdatable, IDrawable {

        protected Sprite sprite;
        protected Texture texture;

        protected float speed;

        protected Rigidbody rigidbody;
        public Rigidbody Rigidbody
        {
            get { return rigidbody; }
        }

        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }
        public int Width
        {
            get { return (int) sprite.Width; }
        }
        public int Height
        {
            get { return (int) sprite.Height; }
        }

        public bool IsActive
        {
            get;
            protected set;
        }

        public GameObject (string texturePath) {
            texture = new Texture (texturePath);
            sprite = new Sprite (texture.Width , texture.Height);
            IsActive = true;
            rigidbody = new Rigidbody (this);
            UpdateMgr.AddItem (this);
            DrawMgr.AddItem (this);
        }

        public virtual void Update () {
        }

        public virtual void Draw () {
            sprite.DrawTexture (texture);
        }

        public virtual void OnCollide (GameObject other) {

        }

    }
}
