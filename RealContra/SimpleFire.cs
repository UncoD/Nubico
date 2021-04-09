using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace RealContra
{
    internal class SimpleFire : GameObject
    {
        public SimpleFire(float x, float y) : base(x, y, "Art/SimpleFire1.png")
        {
            AddAnimation("fire", 15,
                "Art/SimpleFire1.png",
                "Art/SimpleFire2.png",
                "Art/SimpleFire3.png",
                "Art/SimpleFire4.png",
                "Art/SimpleFire5.png",
                "Art/SimpleFire6.png",
                "Art/SimpleFire7.png");
            PlayAnimation("fire");
        }
    }
}