using System;
using csharp_sfml_game_framework;
using SFML.System;

namespace Outrun
{
    public class DeathRay : PhysicsObject
    {
        public bool IsRay { get; private set; }
        public bool IsLeftRay { get; }
        private int timeCount;

        public DeathRay(float x, float y) : base(x, y, "Art/death_ray_000.png")
        {
            DrawPriority = 52;
            AddAnimation("ray", 60,
                "Art/death_ray_000.png",
                "Art/death_ray_001.png",
                "Art/death_ray_002.png",
                "Art/death_ray_003.png"
            );

            if (new Random().NextDouble() < 0.5)
            {
                Scale = new Vector2f(2, 2);
                IsLeftRay = true;
            }
            else
            {
                Scale = new Vector2f(-2, 2);
            }

            PlayAnimation("ray");
        }

        public override void OnEachFrame()
        {
            timeCount++;
            if (timeCount == 80)
            {
                IsRay = true;
            }

            if (timeCount == 120)
            {
                IsRay = false;
                DrawPriority = 1;
            }

            if (timeCount == 175)
            {
                DeleteFromGame();
            }
        }
    }
}