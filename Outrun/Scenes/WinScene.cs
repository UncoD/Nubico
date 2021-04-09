using System.CodeDom;
using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Outrun
{
    public class WinScene : GameScene
    {
        private GameObject background;
        private GameObject car;
        public WinScene()
        {
            var winText = new BlinkingTextObject("WIN!", Game.Width / 2f, 100, 40);
            winText.SetColor(Color.Cyan);
            background = new GameObject(Game.Width / 2f, Game.Height / 2f, "Art/danger_background_000.png");
            car = new GameObject(Game.Width / 2f, Game.Height / 2f + 100, "Art/danger_obstacle_001.png");
            car.Scale = new Vector2f(0.3f, 0.3f);

            var controlText = new TextObject("Esc - exit\nSpace - restart", 150, Game.Height - 50);

            AddToScene(background, winText, controlText, car);

            MusicController.StopMusic();
            MusicController.PlayMusic("Music/Win.ogg");
        }

        public override void OnEachFrame()
        {
            if (background.Scale.X < 5)
            {
                background.Scale += new Vector2f(0.02f, 0.02f);
            }

            if (car.Scale.X < 2.5)
            {
                car.Scale += new Vector2f(0.02f, 0.02f);
            }
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            switch (pressedKey)
            {
                case Keyboard.Key.Space:
                    Game.SetCurrentScene(new OutrunScene());
                    break;
                case Keyboard.Key.Escape:
                    Game.Close();
                    break;
            }
        }
    }
}