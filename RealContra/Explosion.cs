using System.Collections.Generic;
using Ungine;

namespace RealContra
{
    internal class Explosion : GameObject
    {
        private int timer;

        public Explosion(float x, float y) : base(x, y, "Art/Explosion1.png")
        {
            timer = 0;
            SoundController.PlaySound("Sound/Explosion.wav");
            AddAnimation("explosion", 15,
                "Art/Explosion1.png",
                "Art/Explosion2.png",
                "Art/Explosion3.png",
                "Art/Explosion4.png",
                "Art/Explosion5.png",
                "Art/Explosion6.png");
            PlayAnimation("explosion");
        }

        public override void OnEachFrame()
        {
            timer++;
            if (timer == 90)
                DeleteFromGame();
            base.OnEachFrame();
        }
    }
}