using System;
using Ungine;
using SFML.System;

namespace Asteroid
{
    public class Particle : GameObject
    {
        private readonly Random rand = new Random();

        public Particle(float x, float y, float angle, float speed, int hp, float scale, Game game) : base(x, y)
        {
            DrawPriority = 5;
            SetSprite("Art/particle.png");
            Scale = new Vector2f(scale, scale);
            Scale = new Vector2f(scale, scale);
            Health = hp;

            var delta = BounceObject.RotateByAngle(speed, angle);
            SpeedX = delta.X;
            SpeedY = delta.Y;
        }

        public override void OnEachFrame()
        {
            Health--;
            Position += new Vector2f(SpeedX, SpeedY);

            if (Health <= 0) DeleteFromGame();

            base.OnEachFrame();

            Rotation = (float) rand.NextDouble() * 360;
        }
    }
}