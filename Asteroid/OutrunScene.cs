using System;
using csharp_sfml_game_framework;

namespace Asteroid
{
    public class OutrunScene : GameScene
    {
        private Random rand = new Random();

        public OutrunScene(Game game) : base(game)
        {
            var car = new Car(game.Width / 2f, game.Height - 70);
            var background = new Background(game.Width / 2f, game.Height / 2f);
            AddToScene(background, car);
        }

        protected override void OnEachFrame()
        {
            if (rand.NextDouble() <= 0.01)
            {
                var barrel = new Obstacle(Game.Width / 4 + 80, 260);
                AddToScene(barrel);
            }

            base.OnEachFrame();
        }
    }
}