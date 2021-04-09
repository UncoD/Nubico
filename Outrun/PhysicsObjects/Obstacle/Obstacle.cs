using System;
using System.Collections.Generic;
using csharp_sfml_game_framework;
using SFML.System;

namespace Outrun
{
    public class Obstacle : PhysicsObject
    {
        private static int drawPriority = 49;
        protected List<(int, int)> directions;
        protected bool ChangeDrawPriority = true;
        protected Vector2f direction;

        public Obstacle(float x, float y, string path, bool isPalm, bool flip, float defScale = 0.3f) : base(x, y, path)
        {
            directions = new List<(int, int)>
            {
                (80, -180),
                (30, 50),
                (15, 230)
            };

            DrawPriority = drawPriority;
            drawPriority--;
            if (drawPriority == 3)
            {
                drawPriority = 49;
            }

            var (startX, endX) = isPalm ? directions[0] : directions[new Random().Next(1, directions.Count)];
            
            Origin = new Vector2f(Width / 2, Height);

            if (new Random().NextDouble() < 0.5)
            {
                Position = new Vector2f(x - startX, y);
                Scale = new Vector2f(defScale, defScale);
                direction = Normalize(new Vector2f(endX, Game.Height) - Position);
            }
            else
            {
                Position = new Vector2f(x + startX, y);
                Scale = flip ? new Vector2f(-defScale, defScale) : new Vector2f(defScale, defScale);
                direction = Normalize(new Vector2f(Game.Width - endX, Game.Height) - Position);
            }
        }

        protected Vector2f Normalize(Vector2f vector)
        {
            var len = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            return vector / len;
        }

        public override void OnEachFrame()
        {
            if (Y > Game.Height - 50 && ChangeDrawPriority)
            {
                DrawPriority = 51;
            }

            if (Car.Turbo == 2)
            {
                Scale += new Vector2f(0.00004f * Y * Math.Sign(Scale.X) * Car.Turbo, 0.00004f * Y * Car.Turbo);
            }
            else if (Car.Turbo > 2)
            {
                Scale += new Vector2f(0.000023f * Y * Math.Sign(Scale.X) * Car.Turbo, 0.000023f * Y * Car.Turbo);
            }
            else
            {
                Scale += new Vector2f(0.00005f * Y * Math.Sign(Scale.X), 0.00005f * Y);
            }
                
            Position += direction * Car.Turbo;
            direction *= 1.01f;

            if (Y - Height > Game.Height || X + Width / 2 < 0 || X - Width / 2 > Game.Width)
                DeleteFromGame();
        }
    }
}