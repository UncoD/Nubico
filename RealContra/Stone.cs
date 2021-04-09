using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace RealContra
{
    internal class Stone : PhysicsObject
    {
        private readonly List<string> stoneSprite;
        private int size;

        public Stone(float x, float y, int size) : base(x, y)
        {
            this.size = --size;
            stoneSprite = new List<string>
            {
                "Art/1Stone.png",
                "Art/2Stone.png",
                "Art/3Stone.png",
                "Art/4Stone.png",
                "Art/5Stone.png",
                "Art/15Stone.png"
            };
            SetSprite(stoneSprite[size]);
            Scale = new SFML.System.Vector2f(2, 2);
        }
    }
}