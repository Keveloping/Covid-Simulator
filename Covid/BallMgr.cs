using System;
using OpenTK;

namespace Covid {

    public static class BallMgr{

        private const int numberOfBall = 30;
        private const int prossimity = 100;

        private static Ball[] balls;
        private static float prossimitySqaured;

        public static void Init() {
            balls = new Ball[numberOfBall];
            prossimitySqaured = prossimity * prossimity;

            for(int i=0; i<balls.Length; i++) {
                balls[i] = new Ball();
                balls[i].Position = new Vector2(RandomGenerator.GetRandomInt((int)balls[i].Radius, Game.Win.Width - (int)balls[i].Radius),
                    RandomGenerator.GetRandomInt((int)balls[i].Radius, Game.Win.Height - (int)balls[i].Radius));        
            }

            balls[0].Status = BallStatus.Sick;
        }

        public static bool CheckProssimity(Vector2 position) {
            for(int i=0; i<balls.Length; i++) {
                if (balls[i].Status != BallStatus.Sick) continue;
                if ((balls[i].Position - position).LengthSquared <= prossimitySqaured) return true;
            }
            return false;
        }

    }
}
