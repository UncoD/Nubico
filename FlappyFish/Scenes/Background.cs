using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class Background : GameObject
    {
        public Background(string pathToTexture, float x, float y) : base(x, y, pathToTexture) {
            Scale = new Vector2f(1, 1);
        }
    }
}