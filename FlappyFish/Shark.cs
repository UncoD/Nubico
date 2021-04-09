using System;
using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class Shark : PhysicsObject
    {
        Random rnd;
        int randomWalk;

        public Shark(string pathToTexture, float x, float y) : base(x, y, pathToTexture)
        {
            randomWalk = 0;
            float dx = 1, dy = 1;
            Scale = new Vector2f(dx, dy);
            rnd = new Random(0xDEAD);
        }

        public override void OnEachFrame()
        {
            if (randomWalk == 0) {
                randomWalk = rnd.Next(-100, 100);
                if (randomWalk == 0)
                {
                    randomWalk = 1;
                }
            }
            MoveIt(-2, (randomWalk < 0)? -1 : 1);
            randomWalk = randomWalk < 0 ? randomWalk + 1 : randomWalk - 1;
            if (X <= -20)
            {
                Game.Score += 1;
                DeleteFromGame();
            }

            base.OnEachFrame();
        }

    }
}
