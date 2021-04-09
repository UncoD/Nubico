using System;
using SFML.System;

namespace Outrun
{
    public class Palm : Obstacle
    {
        public Palm(float x, float y, string mode) : base(x, y, $"Art/{mode}_palms_000.png", true, true)
        {
            var palms = new[]
            {
                $"Art/{mode}_palms_000.png",
                $"Art/{mode}_palms_002.png",
                $"Art/{mode}_palms_003.png",
            };

            SetSprite(palms[new Random().Next(palms.Length)]);
            Origin = new Vector2f(Width / 2, Height);
        }
    }

    public class BrokenPalm : Obstacle
    {
        public BrokenPalm(float x, float y, string mode) : base(x, y, $"Art/{mode}_palms_001.png", true, true)
        {
        }
    }
}