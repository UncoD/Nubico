using csharp_sfml_game_framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jRPG
{
    class Background : GameObject
    {
        public Background(float x, float y, string pathToTexture) : base(x, y, pathToTexture)
        {
            Scale = new SFML.System.Vector2f(1f, 1f);
            Scale = new SFML.System.Vector2f(1f, 1f);
        }
    }
}
