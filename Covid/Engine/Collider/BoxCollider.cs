using OpenTK;
using System;

namespace Covid {
    public class BoxCollider : Collider {

        protected float halfWidth;
        protected float halfHeight;
        public float Height
        {
            get { return halfHeight * 2; }
        }
        public float Width
        {
            get { return halfWidth * 2; }
        }

        public BoxCollider (Rigidbody owner , Vector2 offset , float w , float h) : base (owner , offset) {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;
        }

        //Vistor
        public override bool Collides (Collider collider) {
            return collider.Collides (this);
        }

        //Box vs Box
        public override bool Collides (BoxCollider box) {
            float deltaX = Position.X - box.Position.X;
            float deltaY = Position.Y - box.Position.Y;

            return (Math.Abs (deltaX) <= halfWidth + box.halfWidth) && (Math.Abs (deltaY) <= halfHeight + box.halfHeight);
        }

        //Box vs Circle
        public override bool Collides (CircleCollider circle) {
            float x3 = Math.Max (Position.X - halfWidth , 
                Math.Min (circle.Position.X , Position.X + halfWidth)); //la x del punto del rettangolo più vicino al centro della circonferenza
            float y3 = Math.Max (Position.Y - halfHeight ,
                Math.Min (circle.Position.Y , Position.Y + halfHeight)); //la y del punto del rettangolo più vicino al centro della circonferenza

            return circle.Contains (new Vector2 (x3 , y3));
        }

        public override bool Contains (Vector2 point) {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

    }
}
