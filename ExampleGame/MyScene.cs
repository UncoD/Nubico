using Nubico.GameBase;
using Nubico.Objects;
using SFML.Graphics;
using SFML.System;

namespace ExampleGame
{
    public class MyScene : GameScene
    {
        private Player player;
        private Enemy enemy;
        private TextObject scoreText;
        private int score = 0;
        public MyScene(int text)
        {
            var helloText = new HelloText("Hello World!", 0, 50);
            helloText.Size = 30;
            helloText.Position = new Vector2f(Game.Width / 2, 50);
            helloText.SetColor(Color.Cyan);
            AddToScene(helloText);

            player = new Player(50, 200);
            AddToScene(player);

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
            if (player.X <= enemy.X + 2 && player.X >= enemy.X - 2)
            {
                score++;
                scoreText.SetText(score);
            }
        }
    }
}