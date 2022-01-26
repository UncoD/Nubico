using SFML.System;
using Nubico.Objects;

namespace ExampleGame
{
    class Enemy : AreaObject
    {
        private float bottom;
        private float top;
        private bool isMoveDown;
        private int speed = 3;

        public Enemy(float x, float y) : base(x, y, "Art/enemy.png")
        {
            bottom = y;
            top = y - 150;
            Scale = new Vector2f(3, 3);
        }

        public override void OnEachFrame()
        {
            Rotation += 5;
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
