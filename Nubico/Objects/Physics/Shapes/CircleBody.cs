using Box2DX.Dynamics;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace Nubico.Objects.Physics.Shapes;

public class CircleBody : PhysicsBody
{
    public CircleBody(float radius, Vector2f position, BodyParams bodyParams, bool isStaticBody = false) : base(position, isStaticBody)
    {
        var def = new CircleDef
        {
            Radius = radius * 2 / Constants.PPM,
            Density = bodyParams.Density,
            Friction = bodyParams.Friction,
            Restitution = bodyParams.Restitution,
        };
        Body.CreateFixture(def);

        if (!isStaticBody)
        {
            Body.SetMassFromShapes();
        }

        Shape = new CircleShape
        {
            Radius = radius,
            Position = position,
            FillColor = new Color(8, 20, 222, 125),
            OutlineColor = new Color(0, 255, 0, 125),
            OutlineThickness = 2,
            Origin = new Vector2f(radius, radius)
        };
    }
}