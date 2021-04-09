using System;
using csharp_sfml_game_framework;
using SFML.Window;

namespace Tetris
{
    class MainScene : GameScene
    {
        private Random rand = new Random();
        public MainScene()
        {
            var background = new Background("Art/BackGround.png", Game.Width / 2, Game.Height / 2);
            AddToScene(background);

            var ground = new Ground();
            var shape = new Shape(rand.Next() % 7, rand.Next() % Shape.getColors());
            AddToScene(shape, ground);
        }
        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            switch (key)
            {
                case Keyboard.Key.Escape:
                    Game.Close();
                    break;
            }
        }
    }
}
