using SFML.System;
using Ungine;

namespace MyFirstGame
{
    class Enemy : PhysicsObject
    {
        private float bottom;
        private float top;
        private bool isMoveDown;
        private int speed = 3;

        public Enemy(float x, float y) : base(x, y, "Art/enemy.png")
        {
            bottom = y;
            top = y - 120;
            Scale = new Vector2f(3, 3);
            Origin = new Vector2f(0, 0);
        }

        public override void OnEachFrame()
        {
            if (isMoveDown)
            {
                Velocity = new Vector2f(0, speed);

                if (Y > bottom)
                {
                    isMoveDown = false;
                }
            }
            else
            {
                Velocity = new Vector2f(0, -speed);

                if (Y < top)
                {
                    isMoveDown = true;
                }
            }
        }
    }
}
