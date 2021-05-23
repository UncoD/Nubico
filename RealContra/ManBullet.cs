using Ungine;

namespace RealContra
{
    internal class ManBullet : PhysicsObject
    {
        private readonly string side;

        public ManBullet(float x, float y, string side) : base(x, y, "Art/ManBullet.png")
        {
            SoundController.PlaySound("Sound/bullet.wav");
            this.side = side;
            SpeedX = 30;
        }

        public override void OnEachFrame()
        {
            if (side == "right")
            {
                MoveIt(SpeedX, 0);
                if (X > Game.Width)
                    DeleteFromGame();
            }
            else
            {
                MoveIt(-SpeedX, 0);
                if (X < 0)
                    DeleteFromGame();
            }
            base.OnEachFrame();
        }

        public override void OnCollide(GameObject gameObject)
        {
            DeleteFromGame();
        }
    }
}