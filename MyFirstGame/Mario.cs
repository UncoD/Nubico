using SFML.System;
using SFML.Window;
using System;
using Ungine;

namespace MyFirstGame
{
    public class Mario : PhysicsObject
    {
        private bool isMoveLeft;
        private bool isMoving;
        private int speedX = 5, speedY = 0;
        public Mario(float x, float y) : base(x, y, "Art/Player/player_0.png")
        {
            AddAnimation("walk", 0.1f,
                "Art/Player/player_0.png",
                "Art/Player/player_1.png",
                "Art/Player/player_2.png",
                "Art/Player/player_3.png"
            );
            Scale = new Vector2f(3, 3);
            Origin = new Vector2f(Width / 2, Height / 2);
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
                    Velocity = new Vector2f(-speedX, speedY);
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
                    Velocity = new Vector2f(speedX, speedY);
                    isMoving = true;
                    break;
            }
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Enemy)
            {
                Game.SetCurrentScene(new LoseScene());
            }
        }

        public override void OnEachFrame()
        {
            if (!isMoving)
            {
                StopAnimation(true);
            }
            else if (CurrentAnimationName != "walk")
            {
                PlayAnimation("walk");
            }

            isMoving = false;
            Velocity = new Vector2f(0, 0);
        }

        public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked)
        {
            Console.WriteLine(position);
        }
    }
}