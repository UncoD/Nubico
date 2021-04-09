using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace RealContra
{
    internal class Arrow : GameObject
    {
        public Arrow(float x, float y, string side) : base(x, y, "Art/ArrowRight1.png")
        {
            AddAnimation("right", 30,
                "Art/ArrowRight1.png",
                "Art/ArrowRight2.png");
            AddAnimation("left", 30,
                "Art/ArrowLeft1.png",
                "Art/ArrowLeft2.png");
            if (side == "right")
                PlayAnimation("right");
            else
                PlayAnimation("left");
        }
    }
}