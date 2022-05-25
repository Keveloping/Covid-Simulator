using Aiv.Fast2D;
using OpenTK;
using System;

namespace Covid {

    public enum BallStatus { Ok, Near, Sick };

    public class Ball : GameObject {

        private const float ballSpeedOk = 80;
        private const float ballSpeedNear = 120;
        private const float ballSpeedSick = 50;
        private const float timeToDie = 10;
        private const float timeToGetSick = 3;
        public float Radius { get { return Width * 0.5f;  } }

        private BallStatus status;
        private bool isNearSick;
        private float sickProssimityCounter = 0;
        private float deathCounter = 0;

        public BallStatus Status {
            get { return status; }
            set { ChangeStatus(value); }
        }
        public Ball () : base ("Assets/grey_ball.png") {
            sprite.pivot = new Vector2(Radius, Radius);
            rigidbody.Collider = ColliderFactory.CreateCircleFor(this);
            rigidbody.Type = RigidbodyType.Ball;
            rigidbody.AddCollisionType(RigidbodyType.Ball);

            rigidbody.Velocity = new Vector2(RandomGenerator.GetRandomFloat(), RandomGenerator.GetRandomFloat());
            rigidbody.Velocity.X *= Math.Sign((RandomGenerator.GetRandomInt(-100, 101)));
            rigidbody.Velocity.Y *= Math.Sign(( RandomGenerator.GetRandomInt(-100, 101)));

            Status = BallStatus.Ok;
        }

        private void ChangeStatus(BallStatus s) {
            status = s;
            switch (status) {
                case BallStatus.Ok:
                    sprite.SetAdditiveTint(-255, 255, -255, 0);
                    rigidbody.Velocity = rigidbody.Velocity.Normalized() * ballSpeedOk;
                    break;
                case BallStatus.Near:
                    sprite.SetAdditiveTint(255, 255, -255, 0);
                    rigidbody.Velocity = rigidbody.Velocity.Normalized() * ballSpeedNear;
                    break;
                case BallStatus.Sick:
                    sprite.SetAdditiveTint(255, -255, -255, 0);
                    rigidbody.Velocity = rigidbody.Velocity.Normalized() * ballSpeedSick;
                    break;
            }
        }

        public override void Update() {
            KeepInBorder();

            switch (Status) {
                case BallStatus.Sick:
                    CountdownToDeath();
                    break;
                case BallStatus.Ok:
                case BallStatus.Near:
                    CheckProssimity();
                    if (isNearSick) {
                        CountdownToCovid();
                    }
                    break;

            }
        }


        private void KeepInBorder() {
            if(Position.X - Radius <0) {
                Position = new Vector2( Radius, Position.Y);
                rigidbody.Velocity.X *= -1;
            } else if (Position.X + Radius >= Game.Win.Width) {
                Position = new Vector2(Game.Win.Width-Radius, Position.Y);
                rigidbody.Velocity.X *= -1;
            }

            if(Position.Y - Radius <= 0) {
                Position = new Vector2(Position.X, Radius);
                rigidbody.Velocity.Y *= -1;
            } else if (Position.Y + Radius >= Game.Win.Height) {
                Position = new Vector2(Position.X, Game.Win.Height - Radius);
                rigidbody.Velocity.Y *= -1;
            }
        }

        private void CheckProssimity() {
            if (BallMgr.CheckProssimity(Position)) {
                Status = BallStatus.Near;
                isNearSick = true;
            }
            else {
                isNearSick = false;
                sickProssimityCounter = 0;
                Status = BallStatus.Ok;
            }
        }

        private void CountdownToCovid() {
            sickProssimityCounter += Game.Win.DeltaTime;
            if(sickProssimityCounter >= timeToGetSick) {
                Status = BallStatus.Sick;
            }
        }
        private void CountdownToDeath() {

        }

        public override void OnCollide(GameObject other) {
            Vector2 dist = other.Position - Position;
            if(Math.Abs (dist.X) > Math.Abs(dist.Y)) {
                rigidbody.Velocity.X *= -1;
            }
            else {
                rigidbody.Velocity.Y *= -1;
            }

            Position -= dist.Normalized() * (Radius + ((Ball)other).Radius - dist.Length + 2);
        }

    }
}
