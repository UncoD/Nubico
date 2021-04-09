using System;
using csharp_sfml_game_framework;
using SFML.System;

namespace Asteroid
{
    public class Obstacle : PhysicsObject
    {
        public Obstacle(float x, float y) : base(y, x, "Art/Danger Zone Outrun/palm_000.png")
        {
            DrawPriority = 1;
            SpeedX = new Random().Next() % 2 - 1;
            if (SpeedX == 0)
            {
                SpeedX = 1;
            }

            if (SpeedX == 1)
            {
                Position = new Vector2f(X * 3 - 290, Y);
            }

            Scale = new Vector2f(0.5f, 0.5f);
            Sprite.Scale = Scale;
        }

        public override void OnEachFrame()
        {
            MoveIt(SpeedX, 0.2f);

            SpeedX += 0.15f * Math.Sign(SpeedX);
            Scale += new Vector2f(0.03f, 0.03f);
            Sprite.Scale = Scale;

            if (Y > Game.Height)
                DeleteFromGame();

            base.OnEachFrame();
        }
    }
}