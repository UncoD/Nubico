using System.Collections.Generic;
using Ungine;

namespace RealContra
{
    internal class Heart : GameObject
    {
        private readonly List<string> sprites;
        private int n;

        public Heart(float x, float y, int n) : base(x, y)
        {
            this.n = n;
            sprites = new List<string>
            {
                "Art/EmptyHeart.png",
                "Art/Heart.png"
            };
            SetSprite(sprites[n]);
        }
    }
}