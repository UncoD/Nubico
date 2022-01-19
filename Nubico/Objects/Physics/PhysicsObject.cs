using Nubico.Objects.Physics.Shapes;
using SFML.Graphics;

namespace Nubico.Objects.Physics
{
    public class PhysicsObject : GameObject
    {
        private readonly PhysicsBody physicsBody;

        public PhysicsObject(PhysicsBody body) : base(0, 0)
        {
            physicsBody = body;
        }

        internal override void UpdateObject()
        {
            physicsBody.SyncShapeBody();
            var shape = physicsBody.GetShape();
            if (shape != null)
            {
                Position = shape.Position;
                Rotation = shape.Rotation;
            }
            OnEachFrame();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Game.DrawObjectBorders)
            {
                target.Draw(physicsBody.GetShape(), states);
            }
        }
    }
}
