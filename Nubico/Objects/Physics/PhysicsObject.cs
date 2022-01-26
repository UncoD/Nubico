using Nubico.Objects.Physics.Shapes;
using SFML.Graphics;

namespace Nubico.Objects.Physics
{
    public class PhysicsObject : GameObject
    {
        private readonly PhysicsBody physicsBody;

        public PhysicsObject(PhysicsBody body, string pathToTexture = "") : base(0, 0, pathToTexture)
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
            SpriteController.UpdateAnimation();
            SpriteController.SynchronizeSprite(this);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            SpriteController.TryDraw(target);
            if (Game.DrawObjectBorders)
            {
                target.Draw(physicsBody.GetShape(), states);
            }
        }
    }
}
