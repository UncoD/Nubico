using SFML.System;
using Nubico;
using Nubico.Objects;

namespace MyFirstGame
{
    public class Ground : PhysicsObject
    {
        public Ground(float x, float y) : base(x, y, "Art/ground.png")
        {
            Scale = new Vector2f(3, 3);
        }
    }
}