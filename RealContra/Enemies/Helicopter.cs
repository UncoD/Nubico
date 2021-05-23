using System.Collections.Generic;
using Ungine;
using SFML.System;

namespace RealContra
{
    internal class Helicopter : PhysicsObject
    {
        public HP hp;
        private int phase = 0;
        private int underPhase = 0;
        private int reload;
        private int timeReload;

        public Helicopter(float x, float y, int reload = 30, int health = 10) : base(x, y)
        {
            hp = new HP(Game.Width / 4 * 3 - 50, 50, 10);
            MusicController.PlayMusic("Sound/Helicopter.wav");
            this.reload = reload;
            Health = health;
            AddAnimation("move", 3,
                "Art/Helicopter1.png",
                "Art/Helicopter2.png",
                "Art/Helicopter3.png",
                "Art/Helicopter4.png");
            PlayAnimation("move");
            Scale = new Vector2f(1, 1);
            GameScene.AddToScene(hp);
        }

        public override void OnEachFrame()
        {
            if (timeReload == 0)
            {
                timeReload = reload;
                if (phase == 2)
                    GameScene.AddToScene(new Bullet(X, Y, "left"));
                if (phase == 1)
                    GameScene.AddToScene(new Bullet(X + Width, Y, "right"));
                if (phase == 0)
                {
                    GameScene.AddToScene(new Bullet(X + Width / 3, Y + Height / 2, "down"));
                    GameScene.AddToScene(new Bullet(X + 2 * Width / 3, Y + Height / 2, "down"));
                }
            }

            if (phase == 0)
            {
                if (underPhase == 0 || underPhase == 2)
                    MoveIt(5, 0);
                else
                    MoveIt(-5, 0);
                if (X > Game.Width - Width)
                    underPhase = 1;
                if (X < 0)
                    underPhase = 2;
                if (X < Game.Width / 2 - Width / 2 + 10 && X > Game.Width / 2 - Width / 2 - 10 && underPhase == 2)
                {
                    phase = 1;
                    underPhase = 0;
                }
            }

            if (phase == 1)
            {
                if (underPhase == 0)
                    MoveIt(0, 5);
                if (Y > Game.Height / 5 * 3)
                    underPhase = 1;
                if (underPhase == 1)
                    MoveIt(0, -5);
                if (Y < 0)
                {
                    phase = 2;
                    underPhase = 0;
                }
            }

            if (phase == 2)
            {
                if (underPhase == 0)
                    MoveIt(0, 5);
                if (Y > Game.Height / 5 * 3)
                    underPhase = 1;
                if (underPhase == 1)
                    MoveIt(0, -5);
                if (Y < 0)
                {
                    phase = 0;
                    underPhase = 0;
                }
            }

            timeReload--;
            base.OnEachFrame();
        }

        public override void OnCollide(GameObject gameObject)
        {
            if (gameObject is ManBullet)
            {
                Health--;
                if (Health == 0)
                    Game.OnWin();
                else
                {
                    hp.DeleteFromGame();
                    hp = new HP(Game.Width / 4 * 3 - 50, 50, Health);
                    GameScene.AddToScene(hp);
                }
            }
        }
    }
}