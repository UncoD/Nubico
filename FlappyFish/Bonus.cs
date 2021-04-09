using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class Bonus : PhysicsObject
    {
        int curSprite = 1;
        int frame;

        public Bonus(string pathToTexture, float x, float y) : base(x, y, pathToTexture)
        {
            frame = 0;
            Scale = new Vector2f(2.0f, 2.0f);
        }

        public override void OnEachFrame()
        {
            ++frame;
            MoveIt(-1, 0);

            if (frame % 32 == 0)
            {
                SetSprite("Art/goldCoin" + curSprite + ".png");
                curSprite = (curSprite + 1) % 10 + 1;
            }

            base.OnEachFrame();
        }

        public override void OnCollide(GameObject collideObject)
        { 
            if (collideObject is Player)
            {
                DeleteFromGame();
            }
        }
    }
}
