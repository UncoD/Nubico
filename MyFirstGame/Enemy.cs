using csharp_sfml_game_framework;

namespace MyFirstGame
{
    class Enemy : PhysicsObject
    {
        private float bottom;
        private float top;
        private bool isMoveDown;

        public Enemy(float x, float y) : base(x, y, "Art/enemy.png")
        {
            bottom = y;
            top = y - 120;
        }

        public override void OnEachFrame()
        {
            if (isMoveDown)
            {
                MoveIt(0, 6);
            }
            else
            {
                MoveIt(0, -6);
            }

            if (Y < top || Y > bottom)
            {
                isMoveDown = !isMoveDown;
            }
        }
    }
}
