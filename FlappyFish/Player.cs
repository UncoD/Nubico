using csharp_sfml_game_framework;
using SFML.Window;

namespace FlappyFish
{
    public class Player : PhysicsObject
    {
        private int slowMoveUp = 0;

        public Player(string pathToTexture, float x, float y, string name) : base(x, y, pathToTexture)
        {
            Scale = new SFML.System.Vector2f(0.15f, 0.15f);
        }

        public override void OnEachFrame()
        {
            if (slowMoveUp > 0)
            {
                if (slowMoveUp > 50)
                {
                    if (Y - 10 > 0)
                    {
                        MoveIt(0, -2);
                    }
                }
                --slowMoveUp;
            }
            else if (Y + 1 < Game.Height - 10)
            {
                MoveIt(0, 1);
            }
            base.OnEachFrame();
        }
        
        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (slowMoveUp == 0)
            {
                slowMoveUp = 150;
            }
        }
  
        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Wall || collideObject is Missile || collideObject is Shark)
            {
                Game.OnLose();
            }
            if (collideObject is Bonus)
            {
                Game.Score += 20;
            }
        }
    }
}