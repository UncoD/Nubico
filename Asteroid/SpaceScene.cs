using System;
using csharp_sfml_game_framework;

namespace Asteroid
{
    public class SpaceScene : GameScene
    {
        public static readonly int PARTICLE_COUNT = 6;

        private readonly Random rand = new Random();
        protected Asteroid asteroid;

        protected int asteroidCount;
        protected Background background;

        protected int countdown;
        protected bool countdownStarted;
        protected Spaceship ship;

        public SpaceScene()
        {
            countdown = 300;
            countdownStarted = false;

            background = new Background(Game.Width / 2f + rand.Next(-200, 200),
                Game.Height / 2f + rand.Next(-200, 200));
            AddToScene(background);

            ship = new Spaceship(3 * Game.Width / 4f, 3 * Game.Height / 4f, Game, this);
            AddToScene(ship);

            asteroid = new Asteroid(Game.Width / 4f, Game.Height / 4f, 0, 4, this);
            AddToScene(asteroid);

            asteroidCount = 1;
        }

        public void AddAsteroid(float x, float y, int rank)
        {
            if (rank <= 0) return;

            var ast = new Asteroid(x, y, (float) rand.NextDouble() * 360, rank, this);
            AddToScene(ast);

            asteroidCount++;
        }

        public void RemoveAsteroid()
        {
            asteroidCount--;

            if (asteroidCount <= 0) countdownStarted = true;
        }

        public void AddExplosion(float x, float y, int power)
        {
            for (var i = 0; i < power + rand.Next() % (3 * PARTICLE_COUNT); i++)
            {
                var particle = new Particle(
                    x,
                    y,
                    (float) rand.NextDouble() * 360,
                    (float) rand.NextDouble() * 2,
                    (int) Math.Round(200 - Math.Sqrt(rand.Next(40000))),
                    (float) rand.NextDouble() * 3,
                    Game
                );
                AddToScene(particle);
            }
        }

        public void Finish()
        {
            countdownStarted = true;
        }

        public override void OnEachFrame()
        {
            if (!countdownStarted) return;

            countdown--;

            if (countdown <= 0)
            {
                if (asteroidCount <= 0)
                    Game.SetCurrentScene(new VictoryScene());
                else
                    Game.SetCurrentScene(new DefeatScene());
            }
        }
    }
}