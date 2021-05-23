using Ungine;
using SFML.System;

namespace RealContra
{
    internal class WinBackground2 : GameObject
    {
        public WinBackground2(float x, float y) : base(x, y, "Art/WinBackground2.png")
        {
            Scale = new Vector2f(Game.Width / Width, Game.Height / Height);
        }
    }
}