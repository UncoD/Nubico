using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace RealContra
{
    internal class Grass : PhysicsObject
    {
        private readonly List<string> grassSprite;
        private int size;

        public Grass(float x, float y, int size) : base(x, y)
        {
            this.size = --size;
            grassSprite = new List<string>
            {
                "Art/1Grass.png",
                "Art/2Grass.png",
                "Art/3Grass.png",
                "Art/4Grass.png",
                "Art/5Grass.png",
                "Art/6Grass.png",
                "Art/7Grass.png",
                "Art/8Grass.png",
                "Art/24Grass.png"
            };
            SetSprite(grassSprite[size]);
            Scale = new SFML.System.Vector2f(2, 2);
        }
    }
}