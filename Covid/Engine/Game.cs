using Aiv.Fast2D;
using OpenTK;

namespace Covid {
    public static class Game {

        public const float gravity = 400f;

        private static Window win;
        public static Window Win
        {
            get { return win; }
        }

        private static Ball ball;
        public static Ball Ball {
            get { return ball; }
        }


        public static void Init () {
            win = new Window (1280 , 720 , "Covid-21");
            win.SetVSync(false);
            BallMgr.Init();
        }

        public static void Play () {
            while (Win.IsOpened) {
                if (Win.GetKey (KeyCode.Esc)) {
                    break;
                }

                //GameLoop
                

                //FixedUpdate
                PhysicsMgr.FixedUpdate ();
                PhysicsMgr.CheckCollisions ();

                //Update
                UpdateMgr.Update ();

                //Draw
                DrawMgr.Draw ();

                Win.Update ();

            }
        }

    }
}
