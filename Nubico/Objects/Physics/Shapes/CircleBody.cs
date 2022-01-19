using Box2DX.Dynamics;
using SFML.Graphics;
using SFML.System;

namespace Nubico.Objects.Physics.Shapes;

public class CircleBody : PhysicsBody
{
    public CircleBody(float radius, Vector2f position, bool isStaticBody = false) : base(position, isStaticBody)
    {
        var def = new CircleDef
        {
            Radius = radius,
            Density = 1.0f,
            Friction = 0.3f,
            Restitution = 0.8f
        };
        Body.CreateFixture(def);

        if (!isStaticBody)
        {
            Body.SetMassFromShapes();
        }

        Shape = new CircleShape
        {
            Radius = radius * Constants.PPM / 2,
            Position = position,
            OutlineColor = SFML.Graphics.Color.Green,
            OutlineThickness = 2,
            FillColor = SFML.Graphics.Color.Blue,
            Origin = new Vector2f(radius, radius) * Constants.PPM / 2
        };
    }
}