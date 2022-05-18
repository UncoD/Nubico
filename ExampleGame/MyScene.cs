using Nubico.GameBase;
using Nubico.Objects;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
            helloText.Position = new Vector2f(Game.Width / 2.0f, 50);
            helloText.SetColor(Color.Cyan);
            AddToScene(helloText);

            player = new Player(50, 225);
            AddToScene(player);

            enemy = new Enemy(Game.Width / 2.0f, 242);
            AddToScene(enemy);

            for (var i = 0; i < 15; i++)
            {
                AddToScene(new Ground(36 * i, 280));
            }

            scoreText = new TextObject("0", 10, 350);
            scoreText.Size = 10;
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

        public override void OnKeyPress(Dictionary<Keyboard.Key, bool> pressedKeys)
        {
            scoreText.SetText("Нажаты: " + string.Join(',', pressedKeys.Keys.ToList()));
        }
    }
}