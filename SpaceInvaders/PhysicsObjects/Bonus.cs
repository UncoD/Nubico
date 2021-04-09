using csharp_sfml_game_framework;
using SFML.System;

namespace SpaceInvaders
{
    public class Bonus : PhysicsObject
    {
        public Bonus(string pathToSprite, float x, float y) : base(x, y, pathToSprite)
        {
            SpeedX = 0;
            SpeedY = 3;
            Scale = new Vector2f(2, 2);
        }

        public override void OnEachFrame()
        {
            MoveIt(SpeedX, SpeedY);

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
