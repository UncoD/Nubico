using System.Collections.Generic;
using Ungine;
using SFML.System;

namespace RealContra.Backgrounds
{
    internal class StartBackground : GameObject
    {
        public StartBackground(float x, float y) : base(x, y, "Art/StartBackground1.png")
        {
            AddAnimation("move", 15,
                "Art/StartBackground1.png",
                "Art/StartBackground2.png");
            PlayAnimation("move");
            Scale = new Vector2f(Game.Width / Width, Game.Height / Height);
        }
    }
}