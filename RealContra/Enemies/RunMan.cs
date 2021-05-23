using System.Collections.Generic;
using Ungine;

namespace RealContra
{
    internal class RunMan : PhysicsObject
    {
        private readonly float leftPoint;
        private readonly float rightPoint;
        private bool rightFlag = true;

        public RunMan(float x, float y, float rightPoint) : base(x, y, "Art/RunManRight1.png")
        {
            Health = 1;
            leftPoint = x;
            this.rightPoint = rightPoint;
            AddAnimation("right", 15,
                "Art/RunManRight1.png",
                "Art/RunManRight2.png",
                "Art/RunManRight3.png",
                "Art/RunManRight4.png",
                "Art/RunManRight5.png",
                "Art/RunManRight6.png",
                "Art/RunManRight7.png",
                "Art/RunManRight8.png",
                "Art/RunManRight9.png",
                "Art/RunManRight10.png",
                "Art/RunManRight11.png",
                "Art/RunManRight12.png",
                "Art/RunManRight13.png");
            AddAnimation("left", 15,
                "Art/RunManLeft1.png",
                "Art/RunManLeft2.png",
                "Art/RunManLeft3.png",
                "Art/RunManLeft4.png",
                "Art/RunManLeft5.png",
                "Art/RunManLeft6.png",
                "Art/RunManLeft7.png",
                "Art/RunManLeft8.png",
                "Art/RunManLeft9.png",
                "Art/RunManLeft10.png",
                "Art/RunManLeft11.png",
                "Art/RunManLeft12.png",
                "Art/RunManLeft13.png");
            Scale = new SFML.System.Vector2f(2, 2);
        }

        public override void OnEachFrame()
        {
            if (rightFlag)
            {
                PlayAnimation("right");
                MoveIt(2, 0);
                if (X > rightPoint)
                    rightFlag = false;
            }
            else
            {
                PlayAnimation("left");
                MoveIt(-2, 0);
                if (X < leftPoint)
                    rightFlag = true;
            }

            base.OnEachFrame();
        }

        public override void OnCollide(GameObject gameObject)
        {
            if (gameObject is ManBullet)
            {
                DeleteFromGame();
                Health--;
            }
            else if(gameObject is Man)
            {
                Health--;
                DeleteFromGame();
                GameScene.AddToScene(new Explosion(X, Y));
            }
        }
    }
}