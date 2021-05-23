using csharp_sfml_game_framework;
using SFML.System;

namespace Outrun
{
    public class Background : GameObject
    {
        public Background(string type, float x, float y) : base(x, y, $"Art/{type}_background_000.png")
        {
            AddAnimation("back", 0.5f,
                $"Art/{type}_background_000.png",
                $"Art/{type}_background_001.png",
                $"Art/{type}_background_002.png",
                $"Art/{type}_background_003.png",
                $"Art/{type}_background_004.png",
                $"Art/{type}_background_005.png"
            );

            PlayAnimation("back");
            Scale = new Vector2f(2, 2);
        }

        public override void OnEachFrame()
        {
            SetAnimationDelay("back", 0.5f - Car.Turbo * 0.01f);
        }
    }
}