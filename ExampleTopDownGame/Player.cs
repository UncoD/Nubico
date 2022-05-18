using SFML.System;
using SFML.Window;
using Nubico.Utils;
using Nubico.Objects.Physics;
using Nubico.Objects.Physics.Shapes;

namespace ExampleTopDownGame
{
    public class Player : PhysicsObject
    {
        private int speed = 2;
        public Player(float x, float y) : base(x: x, y: y, pathToTexture: "Art/player.png")
        {
            PhysicsBody = new RectangleBody
            (
                new Vector2f(22, 19), new Vector2f(x, y),
                new BodyParams { Density = 1, Friction = 0.1f, Restitution = 0.1f }
            );
            EnableShapeRotation = false;
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (pressedKey == Keyboard.Key.A)
            {
                Velocity -= new Vector2f(speed, 0);
            }
            if (pressedKey == Keyboard.Key.D)
            {
                Velocity += new Vector2f(speed, 0);
            }
            if (pressedKey == Keyboard.Key.W)
            {
                Velocity -= new Vector2f(0, speed);
            }
            if (pressedKey == Keyboard.Key.S)
            {
                Velocity += new Vector2f(0, speed);
            }
        }

        public override void OnEachFrame()
        {
            if (Velocity.X != 0 || Velocity.Y != 0)
            {
                Rotation = Velocity.GetDegreesAngle() + 90;
            }
            
            Velocity = new Vector2f(0, 0);
        }
    }
}