using Box2DX.Dynamics;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;
using Shape = SFML.Graphics.Shape;

namespace Nubico.Objects.Physics.Shapes;

public class RectangleBody : PhysicsBody
{
    public RectangleBody(Vector2f size, Vector2f position, bool isStaticBody = false) : base(position, isStaticBody)
    {
        var boxSize = size / Constants.PPM;

        var def = new PolygonDef
        {
            Density = 0,
            Friction = 0,
            Restitution = 0
        };
        def.SetAsBox(boxSize.X, boxSize.Y);
        Body.CreateFixture(def);

        Shape = new RectangleShape(size)
        {
            Position = position,
            Origin = size / 2,
            FillColor = Color.Blue,
            OutlineColor = Color.Green,
            OutlineThickness = 2
        };
    }
}