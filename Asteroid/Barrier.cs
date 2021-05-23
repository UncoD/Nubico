using Ungine;
using SFML.System;

namespace Asteroid
{
    public class Barrier : GameObject
    {
        private readonly Spaceship attachedTo;

        public Barrier(Spaceship attachedTo, int hp, Game game) : base(0, 0)
        {
            DrawPriority = 3;
            SetSprite("Art/barrier.png");
            Scale = new Vector2f(0.33f, 0.33f);

            Health = hp;
            this.attachedTo = attachedTo;
        }

        public override void OnEachFrame()
        {
            Health--;
            Position = new Vector2f(attachedTo.X, attachedTo.Y);

            if (Health <= 0) DeleteFromGame();

            base.OnEachFrame();
        }
    }
}