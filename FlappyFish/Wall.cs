using System;
using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class Wall : PhysicsObject
    {
        public Wall(string pathToTexture, float x, float y, bool isUp) : base(x, y, pathToTexture)
        {
            int dx = 1, dy = 1;
            if (isUp)
            {
                dx = -1;
                dy = -1;
            }
            Scale = new Vector2f(dx, dy);
        }

        public override void OnEachFrame()
        {
            MoveIt(-1, 0);
            if (X <= -20)
            {
                Game.Score += 1;
                DeleteFromGame();
                GameScene.AddToScene(generateRandomWall());
            }
            base.OnEachFrame();
        }

        public Wall generateRandomWall()
        {
            var rnd = new Random();
            var width = 1000;
            var height = 600;

            if (rnd.NextDouble() < 0.5)
            {
                return new Wall("Art/anchor.png", width, height - rnd.Next(300), false);
            }
            else
            {
                return new Wall("Art/anchor.png", width, rnd.Next(300), true);
            }
        }
    }
}
