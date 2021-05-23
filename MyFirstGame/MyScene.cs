using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.System;

namespace MyFirstGame
{
    public class MyScene : GameScene
    {
        private Mario mario;
        private Enemy enemy;
        private TextObject scoreText;
        public MyScene()
        {
            var helloText = new HelloText("Hello World!", 0, 50);
            helloText.Size = 30;
            helloText.Position = new Vector2f(Game.Width / 2, 50);
            helloText.SetColor(Color.Cyan);
            AddToScene(helloText);

            mario = new Mario(50, 238);
            AddToScene(mario);

            enemy = new Enemy(Game.Width / 2, 242);
            AddToScene(enemy);

            for (var i = 0; i < 15; i++)
            {
                AddToScene(new Ground(36 * i, 280));
            }

            scoreText = new TextObject("0", Game.Width / 2, 350);
            scoreText.Size = 25;
            AddToScene(scoreText);
        }

        public override void OnEachFrame()
        {
            if (mario.X <= enemy.X + 2 && mario.X >= enemy.X - 2)
            {
                Game.Score++;
                scoreText.SetText(Game.Score);
            }

        }
    }
}