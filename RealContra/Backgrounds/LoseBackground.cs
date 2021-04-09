using csharp_sfml_game_framework;
using SFML.System;

namespace RealContra.Backgrounds
{
    internal class LoseBackground : GameObject
    {
        public LoseBackground(float x, float y) : base(x, y, "art/LoseBackground.png")
        {
            Scale = new Vector2f(Game.Width / Width, Game.Height / Height);
        }
    }
}