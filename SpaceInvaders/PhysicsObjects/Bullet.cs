using csharp_sfml_game_framework;

namespace SpaceInvaders
{
    public class Bullet : PhysicsObject
    {
        public Bullet(string pathToSprite, float x, float y) : base(x, y, pathToSprite)
        {
            SpeedX = 0;
            SpeedY = 3;
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

            if (collideObject is Bunker)
            {
                DeleteFromGame();
            }
        }
    }
}