using Nubico.Objects.Physics;
using Nubico.Objects.Physics.Shapes;
using SFML.System;

namespace ExampleTopDownGame
{
    public class Wall : PhysicsObject
    {
        public Wall(float x, float y) : base(pathToTexture: "Art/ground_1.png")
        {
            PhysicsBody = new RectangleBody
            (
                new Vector2f(48, 48), new Vector2f(x, y),
                new BodyParams { Density = 1, Friction = 0.1f, Restitution = 0.1f },
                true
            );
            Scale = new Vector2f(3, 3);
        }
    }
}