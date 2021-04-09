using csharp_sfml_game_framework;

namespace MyFirstGame
{
    public class Ground : PhysicsObject
    {
        public Ground(float x, float y) : base(x, y, "Art/ground.png")
        {
        }
    }
}