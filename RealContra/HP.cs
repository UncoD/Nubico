using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ungine;

namespace RealContra
{
    class HP : GameObject
    {
        private List<string> sprites;

        public HP(float x, float y, int n) : base(x, y)
        {
            sprites = new List<string>
            {"Art/HP1.png",
             "Art/HP2.png",
             "Art/HP3.png",
             "Art/HP4.png",
             "Art/HP5.png",
             "Art/HP6.png",
             "Art/HP7.png",
             "Art/HP8.png",
             "Art/HP9.png",
             "Art/HP10.png"};
            SetSprite(sprites[n-1]);
            Scale = new SFML.System.Vector2f(1, 1);
            DrawPriority = 10;
        }
    }
}
