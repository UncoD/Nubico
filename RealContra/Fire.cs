using System.Collections.Generic;
using Ungine;

namespace RealContra
{
    internal class Fire : GameObject
    {
        public Fire(float x, float y) : base(x, y, "Art/Fire1.png")
        {
            AddAnimation("fire", 13,
                "Art/Fire1.png",
                "Art/Fire2.png",
                "Art/Fire3.png",
                "Art/Fire4.png",
                "Art/Fire5.png",
                "Art/Fire6.png",
                "Art/Fire7.png",
                "Art/Fire8.png",
                "Art/Fire9.png",
                "Art/Fire10.png",
                "Art/Fire11.png",
                "Art/Fire12.png",
                "Art/Fire13.png",
                "Art/Fire14.png",
                "Art/Fire15.png",
                "Art/Fire16.png");
            PlayAnimation("fire");
        }
    }
}