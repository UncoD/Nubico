using Ungine;
using MyFirstGame.Objects;

namespace MyFirstGame.Scenes
{
    class Play : GameScene
    {
        System.Random rand;
        Apple apple;

        public Play()
        {
            rand = new System.Random();
            AddToScene(new Snake(48 * 5, 48 * 12, "Art/Snake/SnakeHeadRed.png",
                "Art/Snake/SnakePart.png", false, this));
            AddToScene(new Snake(48 * (Game.Width / 48) - 48 * 5, 48 * 12, "Art/Snake/SnakeHeadBlue.png",
                "Art/Snake/SnakePart.png", true, this));

            apple = new Apple(0, 0, this);
            RespawnApple();
            AddToScene(apple);
        }

        public void RespawnApple()
        {
            var x = 48 * (rand.Next(48, 48 * (Game.Width / 48) - 48) / 48);
            var y = 48 * (rand.Next(48, 48 * (Game.Height / 48) - 48) / 48);

            apple.Position = new SFML.System.Vector2f(x, y);
        }
    }
}
