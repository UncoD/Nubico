using SFML.Graphics;
using SFML.System;

namespace csharp_sfml_game_framework
{
    public class PhysicsObject : GameObject, IOnCollidable
    {
        protected CollideType collideType;
        
        public PhysicsObject(float x, float y, string pathToSprite) : base(x, y, pathToSprite) {}
        public PhysicsObject(float x, float y) : base(x, y) {}

        protected enum CollideType
        {
            Vertical,
            Horizontal,
            None
        }

        public bool IsIntersects(PhysicsObject other)
        {
            collideType = CollideType.None;
            other.collideType = collideType;

            var thisBoundsOnNextFrame = new FloatRect(X - Width / 2 + SpeedX, Y - Height / 2, Width, Height);
            var otherBoundsOnNextFrame = new FloatRect
            (
                other.X - other.Width / 2 + other.SpeedX,
                other.Y - other.Height / 2,
                other.Width, other.Height
            );

            if (thisBoundsOnNextFrame.Intersects(otherBoundsOnNextFrame))
            {
                collideType = CollideType.Horizontal;
                other.collideType = collideType;
                return true;
            }

            thisBoundsOnNextFrame = new FloatRect(X - Width / 2 + SpeedX, Y - Height / 2 + SpeedY, Width, Height);
            otherBoundsOnNextFrame = new FloatRect
            (
                other.X - other.Width / 2 + other.SpeedX,
                other.Y - other.Height / 2 + other.SpeedY,
                other.Width, other.Height
            );
            
            if (thisBoundsOnNextFrame.Intersects(otherBoundsOnNextFrame))
            {
                collideType = CollideType.Vertical;
                other.collideType = collideType;
                return true;
            }
            
            return false;
        }

        public virtual void OnCollide(GameObject collideObject) { }
    }
}