using System;
using System.Collections.Generic;
using csharp_sfml_game_framework;
using SFML.System;

namespace Asteroid
{
    public class Background : GameObject
    {
        private List<string> backgroundSprites;
        private int currentBackground;
        private readonly Random rand = new Random();
        private int speed;

        public Background(float x, float y) : base(x, y)
        {
            SetSprite("Art/background.png");
            Rotation = (float) rand.NextDouble() * 360;
            Scale = new Vector2f(0.7f, 0.7f);
        }

        public override void OnEachFrame()
        {
            //SetSprite("Art/background.png");
            Scale += new Vector2f(0.0000001f, 0.0000001f);
            Rotation += 0.01f;
            base.OnEachFrame();
        }
    }
}