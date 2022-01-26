using Box2DX.Dynamics;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace Nubico.Objects.Physics.Shapes;

public class RectangleBody : PhysicsBody
{
    public RectangleBody(Vector2f size, Vector2f position, BodyParams bodyParams, bool isStaticBody = false) : base(position, isStaticBody)
    {
        var boxSize = size / Constants.PPM;

        var def = new PolygonDef
        {
            Density = bodyParams.Density,
            Friction = bodyParams.Friction,
            Restitution = bodyParams.Restitution,
        };
        def.SetAsBox(boxSize.X, boxSize.Y);
        Body.CreateFixture(def);
        if (!isStaticBody)
        {
            Body.SetMassFromShapes();
        }

        Shape = new RectangleShape(size)
        {
            Position = position,
            Origin = size / 2,
            FillColor = new Color(8, 20, 222, 125),
            OutlineColor = new Color(0, 255, 0, 125),
            OutlineThickness = 2
        };
    }
}