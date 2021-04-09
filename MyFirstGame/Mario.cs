using csharp_sfml_game_framework;
using SFML.Window;

namespace MyFirstGame
{
    public class Mario : PhysicsObject
    {
        private bool isMoveLeft;
        public Mario(float x, float y) : base(x, y, "Art/Mario.png")
        {
            SpeedX = 5;
            SpeedY = 0;
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            switch (pressedKey)
            {
                case Keyboard.Key.Left:
                    if (X - Width / 2 <= 0)
                    {
                        break;
                    }
                    if (!isMoveLeft)
                    {
                        isMoveLeft = true;
                        FlipX();
                    }
                    MoveIt(-SpeedX, SpeedY);
                    break;
                case Keyboard.Key.Right:
                    if (X + Width / 2 >= Game.Width)
                    {
                        break;
                    }
                    if (isMoveLeft)
                    {
                        isMoveLeft = false;
                        FlipX();
                    }
                    MoveIt(SpeedX, SpeedY);
                    break;
            }
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Enemy)
            {
                Game.OnLose();
            }
        }
    }
}