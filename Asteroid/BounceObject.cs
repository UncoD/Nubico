using System;
using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Asteroid
{
    public class BounceObject : PhysicsObject
    {
        protected float angle;
        protected float angleSprite;
        protected float speed = 0;

        public BounceObject(float x, float y, float angleSprite) : base(x, y)
        {
            this.angleSprite = angleSprite;
        }

        public static Vector2f RotateByAngle(float speed, float angle)
        {
            return new Vector2f(
                (float) (speed * Math.Cos(angle * Math.PI / 180) - speed * Math.Sin(angle * Math.PI / 180)),
                (float) (speed * Math.Sin(angle * Math.PI / 180) + speed * Math.Cos(angle * Math.PI / 180))
            );
        }

        private void BounceVertical()
        {
            angle = 270 - angle;
        }

        private void BounceHorizontal()
        {
            angle = 90 - angle;
        }

        public override void OnEachFrame()
        {
            var delta = RotateByAngle(speed, angle);
            MoveIt(delta.X, delta.Y);

            var changed = true;
            while (changed)
            {
                changed = false;
                if (X < 0)
                {
                    MoveIt(-3 * X, 0);
                    BounceHorizontal();
                    changed = true;
                }

                if (Y < 0)
                {
                    MoveIt(0, -3 * Y);
                    BounceVertical();
                    changed = true;
                }

                if (X > 900)
                {
                    MoveIt(-3 * (X - 900), 0);
                    BounceHorizontal();
                    changed = true;
                }

                if (Y > 600)
                {
                    MoveIt(0, -3 * (Y - 600));
                    BounceVertical();
                    changed = true;
                }
            }

            Rotation = angle + angleSprite;

            base.OnEachFrame();
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            base.OnKeyPress(key, isAlreadyPressed);
        }

        public override void OnCollide(GameObject obj)
        {
            base.OnCollide(obj);
        }

        public FloatRect GetBounds()
        {
            var COLLISION_COEF = 0.6f;
            return new FloatRect(
                X - COLLISION_COEF * Width / 2f,
                Y - COLLISION_COEF * Height / 2f,
                COLLISION_COEF * Width,
                COLLISION_COEF * Height
            );
        }
    }
}