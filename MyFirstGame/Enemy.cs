using csharp_sfml_game_framework;

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
        }

        public override void OnEachFrame()
        {
            if (isMoveDown)
            {
                MoveIt(0, speed);
            }
            else
            {
                MoveIt(0, -speed);
            }

            if (Y < top || Y > bottom)
            {
                isMoveDown = !isMoveDown;
            }
        }
    }
}
