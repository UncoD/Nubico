using csharp_sfml_game_framework;
using SFML.Window;
using System;

namespace MyFirstGame
{
    public class Mario : PhysicsObject
    {
        private bool isMoveLeft;
        private bool isMoving;
        public Mario(float x, float y) : base(x, y, "Art/Player/player_0.png")
        {
            SpeedX = 5;
            SpeedY = 0;
            AddAnimation("walk", 0.1f,
                "Art/Player/player_0.png",
                "Art/Player/player_1.png",
                "Art/Player/player_2.png",
                "Art/Player/player_3.png"
            );
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            switch (pressedKey)
            {
                case Keyboard.Key.A:
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
                    isMoving = true;
                    break;
                case Keyboard.Key.D:
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
                    isMoving = true;
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

        public override void OnEachFrame()
        {
            if (!isMoving)
            {
                StopAnimation(true);
            } else if (CurrentAnimationName() != "walk")
            {
                PlayAnimation("walk");
            }

            isMoving = false;
        }
    }
}