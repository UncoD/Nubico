using Box2DX.Dynamics;
using Nubico.GameBase;
using Nubico.Utils;
using SFML.Graphics;
using SFML.System;

namespace Nubico.Objects.Physics.Shapes;

public class PhysicsBody
{
    private readonly Game game = GameProvider.ProvideDependency();
    protected Body Body;
    protected Shape? Shape;
    protected bool IsStaticBody;

    internal PhysicsBody(Vector2f position, bool isStaticBody)
    {
        IsStaticBody = isStaticBody;

        var bodyDef = new BodyDef();
        var bodyPos = position * 2 / Constants.PPM;
        bodyDef.Position.Set(bodyPos.X, bodyPos.Y);
        Body = game.PhysicsWorld.CreateBody(bodyDef);
        SetVelocity(new Vector2f(0, 0));
    }

    internal Shape? GetShape()
    {
        return Shape;
    }

    internal void SetVelocity(Vector2f velocity)
    {
        Body?.SetLinearVelocity(velocity.ToVec());
    }
    
    internal void SyncShapeBody()
    {
        if (Shape == null) return;
        Shape.Position = Body.GetPosition().ToVector() * Constants.PPM / 2;
        Shape.Rotation = Body.GetAngle() * 180 / MathF.PI;
    }
}