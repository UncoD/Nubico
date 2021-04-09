using SFML.System;

using csharp_sfml_game_framework;

namespace Tetris
{
    public class Background : GameObject
    {
        public Background(string pathToTexture, float x, float y) : base(x, y, pathToTexture)
        {
            float scale = Config.height / Height;
            Scale = new Vector2f(scale, scale);
        }
    }
}
