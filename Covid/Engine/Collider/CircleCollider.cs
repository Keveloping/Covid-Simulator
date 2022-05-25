using OpenTK;
using System;

namespace Covid {
    public class CircleCollider : Collider{

        public float Radius;

        public CircleCollider (Rigidbody owner , Vector2 offset , float radius) : base (owner, offset) {
            Radius = radius;
        }


        //Visitors
        public override bool Collides (Collider collider) {
            return collider.Collides (this);
        }

        //Circle vs Box
        public override bool Collides (BoxCollider box) {
            return box.Collides (this);
        }

        //Circle vs Circle
        public override bool Collides (CircleCollider circle) {
            Vector2 vectDist = Position - circle.Position;
            return vectDist.LengthSquared <= Math.Pow (Radius + circle.Radius , 2);
        }

        public override bool Contains (Vector2 point) {
            Vector2 distFromCenter = point - Position;
            return distFromCenter.LengthSquared <= Radius * Radius;
        }

    }
}


