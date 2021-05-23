using Ungine;
using SFML.System;

namespace RealContra.Backgrounds
{
    internal class WinBackground1 : GameObject
    {
        public WinBackground1(float x, float y) : base(x, y, "Art/WinBackground.png")
        {
            Scale = new Vector2f(Game.Width / Width, Game.Height / Height);
        }
    }
}