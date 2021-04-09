using System;
using SFML.System;

namespace Outrun
{
    public class Puddle : Obstacle
    {
        public bool IsUnderCar;
        public Puddle(float x, float y, string mode) : base(x, y, $"Art/{mode}_obstacle_001.png", false, true)
        {
            ChangeDrawPriority = false;
            directions.Add((0, Game.Width / 2));

            var (startX, endX) = directions[new Random().Next(1, directions.Count)];

            Origin = new Vector2f(Width / 2, Height);

            if (new Random().NextDouble() < 0.5)
            {
                Position = new Vector2f(x - startX, y);
                Scale = new Vector2f(0.02f, 0.02f);
                direction = Normalize(new Vector2f(endX, Game.Height) - Position);
            }
            else
            {
                Position = new Vector2f(x + startX, y);
                Scale = new Vector2f(-0.02f, 0.02f);
                direction = Normalize(new Vector2f(Game.Width - endX, Game.Height) - Position);
            }
        }
    }
}