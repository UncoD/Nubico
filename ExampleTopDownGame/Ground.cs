using Nubico.Objects;
using SFML.System;

namespace ExampleTopDownGame
{
    public class Ground : GameObject
    {
        public Ground(float x, float y) : base(x, y, "Art/ground_0.png")
        {
            Scale = new Vector2f(3, 3);
        }
    }
}