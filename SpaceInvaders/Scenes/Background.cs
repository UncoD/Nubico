using csharp_sfml_game_framework;
using SFML.System;

namespace SpaceInvaders
{
    public class Background : GameObject
    {
        public Background(string pathToSprite, float x, float y) : base(x, y, pathToSprite)
        {
            Scale = new Vector2f(2, 2);
        }
    }
}