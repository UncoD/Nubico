using System.Collections.Generic;
using Ungine;
using SFML.Window;

namespace RealContra
{
    internal class Man : PhysicsObject
    {
        private bool pauseFlag;
        private int reload;
        private int damagePause;
        private bool fallFlag = true;
        private bool rightFlag = true;
        private bool animationFlag;

        public Man(float x, float y, int health = 10) : base(x, y, "Art/ManRight.png")
        {
            Health = health;
            AddAnimation("right", 7,
                "Art/ManRight.png",
                "Art/ManRight1.png",
                "Art/ManRight2.png");
            AddAnimation("left", 7,
                "Art/ManLeft.png",
                "Art/ManLeft1.png",
                "Art/ManLeft2.png");
            Scale = new SFML.System.Vector2f(2, 2);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            switch (key)
            {
                case Keyboard.Key.Left:
                    PlayAnimation("left");
                    animationFlag = true;
                    if (!pauseFlag)
                        MoveIt(-4, 0);
                    rightFlag = false;
                    break;
                case Keyboard.Key.Right:
                    PlayAnimation("right");
                    animationFlag = true;
                    if (!pauseFlag)
                        MoveIt(4, 0);
                    rightFlag = true;
                    break;
                case Keyboard.Key.Up:
                    if (SpeedY == 0)
                        SpeedY = -15;
                    break;
                case Keyboard.Key.Space:
                    if (reload == 0)
                    {
                        if (rightFlag)
                            GameScene.AddToScene(new ManBullet(X + 50, Y - 18, "right"));
                        else
                            GameScene.AddToScene(new ManBullet(X - 50, Y - 18, "left"));
                        reload = 30;
                    }
                    break;
            }
        }

        public override void OnEachFrame()
        {
            if (!animationFlag)
            {
                StopAnimation();
                if (rightFlag)
                    SetSprite("Art/ManRight.png");
                else
                    SetSprite("Art/ManLeft.png");
            }

            if (reload > 0)
                reload--;

            if (SpeedY < 0)
                SpeedY *= 0.85f;
            else if (SpeedY > 0)
                if (SpeedY < 15)
                    SpeedY *= 1.15f;
            if (SpeedY > -1 && SpeedY < 0)
                SpeedY = 0;

            if (fallFlag && SpeedY == 0)
                SpeedY = 1;

            MoveIt(0, SpeedY);

            if (damagePause > 0)
                damagePause--;

            base.OnEachFrame();
            fallFlag = true;
            animationFlag = false;
            pauseFlag = false;

            if (Y > Game.Height)
                Game.OnLose();
        }

        public override void OnCollide(GameObject gameObject)
        {
            if (gameObject is Grass || gameObject is Stone)
            {
                if (X - 5 < gameObject.X - gameObject.Width / 2 - Width / 2 ||
                    X + 5 > gameObject.X + gameObject.Width / 2 + Width / 2)
                {
                    if (X - 5 < gameObject.X - gameObject.Width / 2 - Width / 2)
                        MoveIt(-1, 0);
                    if (X + 5 > gameObject.X + gameObject.Width / 2 + Width / 2)
                        MoveIt(1, 0);
                    SpeedX = 0;
                    pauseFlag = true;
                }
                else if (Y > gameObject.Y - gameObject.Height / 2 - Height / 2 - 15 &&
                         Y < gameObject.Y - gameObject.Height / 2 - Height / 2 + 15)
                {
                    SpeedY = 0;
                    fallFlag = false;
                }
                else if (Y >= gameObject.Y - gameObject.Height / 2 - Height / 2 + 15 &&
                          Y < gameObject.Y - gameObject.Height / 2 - Height / 2 + 20)
                {
                    MoveIt(0, -6);
                    SpeedY = 0;
                    fallFlag = false;
                }
            }

            if (gameObject is Bullet || gameObject is FireMan)
                if (damagePause == 0)
                {
                    GameScene.AddToScene(new Heart(50 + (Health - 1) * 28, 50, 0));
                    damagePause = 10;
                    Health--;
                    if (Health == 0)
                        Game.OnLose();
                }
            if (gameObject is RunMan)
                if (damagePause == 0)
                {
                    GameScene.AddToScene(new Heart(50 + (Health - 1) * 28, 50, 0));
                    GameScene.AddToScene(new Heart(50 + (Health - 2) * 28, 50, 0));
                    GameScene.AddToScene(new Heart(50 + (Health - 3) * 28, 50, 0));
                    damagePause = 10;
                    Health -= 3;
                    if (Health <= 0)
                        Game.OnLose();
                }
        }
    }
}