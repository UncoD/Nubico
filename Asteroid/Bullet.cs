using System;
using csharp_sfml_game_framework;
using SFML.System;
using SFML.Window;

namespace Asteroid
{
    public class Bullet : BounceObject
    {
        private readonly Random rand = new Random();

        public Bullet(float x, float y, float initAngle, Game game) : base(x, y, 135)
        {
            DrawPriority = 2;
            SetSprite("Art/ball.png");
            Scale = new Vector2f(0.5f, 0.5f);
            Scale = new Vector2f(0.5f, 0.5f);

            speed = 10;
            angle = initAngle;
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
        }

        public override void OnEachFrame()
        {
            base.OnEachFrame();

            Rotation = (float) rand.NextDouble() * 360;
        }

        public override void OnCollide(GameObject obj)
        {
            base.OnCollide(obj);

            if (obj is Asteroid) DeleteFromGame();
        }
    }
}