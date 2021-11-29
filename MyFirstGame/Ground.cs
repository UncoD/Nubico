using SFML.System;
using Ungine;

namespace MyFirstGame
{
    public class Ground : PhysicsObject
    {
        public Ground(float x, float y) : base(x, y, "Art/ground.png")
        {
            Scale = new Vector2f(3, 3);
            Origin = new Vector2f(0, 0);
        }
    }
}