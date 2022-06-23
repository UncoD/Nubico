using Nubico.Objects.Physics.Shapes;
using SFML.Graphics;
using SFML.System;

namespace Nubico.Objects.Physics
{
    public class PhysicsObject : GameObject
    {
        protected PhysicsBody? PhysicsBody { get; set; }
        public bool EnableShapeRotation { get; set; } = true;

        private Vector2f velocity;
        private bool velocityChangedOnPreviousFrame;
        public new Vector2f Velocity
        {
            get => velocity;
            set
            {
                velocity = value;
                velocityChangedOnPreviousFrame = true;
            }
        }

        public PhysicsObject(PhysicsBody? body = null, float x = 0, float y = 0, string pathToTexture = "") : base(x, y, pathToTexture)
        {
            PhysicsBody = body;
        }

        internal override void UpdateObject()
        {
            if (PhysicsBody == null)
            {
                return;
            }

            if (velocityChangedOnPreviousFrame)
            {
                PhysicsBody?.SetVelocity(velocity);
                velocityChangedOnPreviousFrame = false;
            }

            PhysicsBody.SyncShapeBody();
            var shape = PhysicsBody.GetShape();
            if (shape != null)
            {
                Position = shape.Position;
                if (EnableShapeRotation)
                {
                    Rotation = shape.Rotation;
                }
                else
                {
                    shape.Rotation = Rotation;
                }
            }
            OnEachFrame();
            SpriteController.UpdateAnimation();
            SpriteController.SynchronizeSprite(this);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (PhysicsBody == null)
            {
                return;
            }

            SpriteController.TryDraw(target);
            if (Game.DrawObjectBorders)
            {
                target.Draw(PhysicsBody.GetShape(), states);
            }
        }
    }
}
