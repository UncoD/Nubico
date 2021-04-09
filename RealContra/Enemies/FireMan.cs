using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace RealContra
{
    internal class FireMan : PhysicsObject
    {
        private readonly string side;
        private int reload;
        private int timeReload = 1;

        public FireMan(float x, float y, string side, int reload = 200) : base(x, y, "Art/FireManRight1.png")
        {
            Scale = new SFML.System.Vector2f(2, 2);
            Health = 1;
            this.side = side;
            this.reload = reload;
            AddAnimation("right", reload / 2,
                "Art/FireManRight1.png",
                "Art/FireManRight2.png");
            AddAnimation("left", reload / 2,
                "Art/FireManLeft1.png",
                "Art/FireManLeft2.png");
            if (side == "right")
                PlayAnimation("right");
            else
                PlayAnimation("left");
        }

        public override void OnEachFrame()
        {
            if (timeReload == 0)
            {
                timeReload = reload;
                if (side == "right")
                    GameScene.AddToScene(new Bullet(X + Width, Y - Height / 3, "right"));
                else
                    GameScene.AddToScene(new Bullet(X - Width, Y - Height / 3, "left"));
            }
            else
                timeReload--;
            base.OnEachFrame();
        }

        public override void OnCollide(GameObject gameObject)
        {
            if (gameObject is ManBullet)
            {
                DeleteFromGame();
                Health--;
            }
        }
    }
}