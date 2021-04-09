using csharp_sfml_game_framework;

namespace RealContra
{
    internal class Bullet : PhysicsObject
    {
        private readonly string side;

        public Bullet(float x, float y, string side, int speed = 8) : base(x, y, "Art/Bullet.png")
        {
            SoundController.PlaySound("Sound/bullet.wav");
            this.side = side;
            SpeedX = speed;
        }

        public override void OnEachFrame()
        {
            if (side == "right")
            {
                MoveIt(SpeedX, 0);
                if (X > Game.Width)
                    DeleteFromGame();
            }
            else if (side == "left")
            {
                MoveIt(-SpeedX, 0);
                if (X < 0)
                    DeleteFromGame();
            }
            else
            {
                MoveIt(0, SpeedX);
                if (Y > Game.Height)
                    DeleteFromGame();
            }
            base.OnEachFrame();
        }

        public override void OnCollide(GameObject gameObject)
        {
            if (!(gameObject is FireMan || gameObject is Helicopter || gameObject is RunMan))
                DeleteFromGame();
        }
    }
}