using System;
using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class Missile : PhysicsObject
    {
        Random rnd;
        public Missile(string pathToTexture, float x, float y) : base(x, y, pathToTexture)
        {
            float dx = 0.5f, dy = 0.5f;
            Scale = new Vector2f(dx, dy);
            Rotation = 90.0f*3;
            rnd = new Random();
        }

        public override void OnEachFrame()
        {
            MoveIt(-3, 0);
            if (X <= -20)
            {
                Game.Score += 1;
                DeleteFromGame();
            }

            base.OnEachFrame();
        }
    }
}
