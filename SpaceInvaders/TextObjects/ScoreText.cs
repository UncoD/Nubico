using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders
{
    public class ScoreText : TextObject
    {
        public ScoreText(string text, float x, float y, int height = 15) : base(text, x, y, height)
        {
        }

        public override void OnEachFrame()
        {
            SetText(Game.Score);

            // Выравнивает текст по правому краю
            Origin = new Vector2f(GetBounds().Width, 0);
            
            base.OnEachFrame();
        }
    }
}