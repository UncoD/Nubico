using Ungine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jRPG
{
    class Battle : GameObject
    {
        public Battle(float x, float y) : base(x, y)
        {
            SetSprite("Art/battleground.png");
            DrawPriority = 3;
            Scale = new SFML.System.Vector2f(1, 1);
            Scale = new SFML.System.Vector2f(1, 1);
        }
    }
}
