using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class ScoreText : TextObject
    {
        public ScoreText(string text, float x, float y) : base(text, x, y)
        {
        }

        public override void OnEachFrame()
        {
            SetText(Game.Score);
            // Origin = new Vector2f(Bounds().Width, 0);
            
            base.OnEachFrame();
        }
    }
}