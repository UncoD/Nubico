using csharp_sfml_game_framework;
using SFML.System;
using SFML.Window;

namespace MyFirstGame.Scenes
{
    class Menu : GameScene
    {
        public Menu(bool isDraw = false, bool isEnd = false, bool redWinner = false)
        {
            TextObject gameName = new TextObject("~ Double Snake ~", Game.Width / 2, 200, 50);
            gameName.SetColor(SFML.Graphics.Color.Yellow);
            AddToScene(gameName);
            AddToScene(new ExitButton(Game.Width / 2, Game.Height - 200, 30));
            AddToScene(new StartButton(Game.Width / 2, Game.Height / 2 - 60, 40));

            var redHead = new GameObject(256, Game.Height / 2, "Art/Help/Red.png");
            var blueHead = new GameObject(Game.Width - 256, Game.Height / 2, "Art/Help/Blue.png");
            redHead.Scale = new Vector2f(5, 5);
            blueHead.Scale = new Vector2f(5, 5);
            AddToScene(redHead);
            AddToScene(blueHead);

            if (isDraw)
            {
                TextObject endText = new TextObject("Draw", Game.Width / 2, 300, 30);
                endText.SetColor(SFML.Graphics.Color.Green);
                AddToScene(endText);
            } else if (isEnd)
            {
                TextObject endText = new TextObject($"{(redWinner ? "Red" : "Blue")} is the winner",
                    Game.Width / 2, 300, 30);
                endText.SetColor(redWinner ? SFML.Graphics.Color.Red : SFML.Graphics.Color.Cyan);
                AddToScene(endText);
            }
        }
    }

    class ExitButton : TextObject
    {
        public ExitButton(float x, float y, int height) : base("Exit", x, y, height)
        {
        }

        public override void OnEachFrame()
        {
            if (HoverOnThis())
            {
                SetColor(SFML.Graphics.Color.Red);
            } else
            {
                SetColor(SFML.Graphics.Color.White);
            }
        }

        public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked)
        {
            if (HoverOnThis())
            {
                Game.Close();
            }
        }
    }

    class StartButton : TextObject
    {
        public StartButton(float x, float y, int height) : base("Start", x, y, height)
        {
        }

        public override void OnEachFrame()
        {
            if (HoverOnThis())
            {
                SetColor(SFML.Graphics.Color.Magenta);
            }
            else
            {
                SetColor(SFML.Graphics.Color.White);
            }
        }

        public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked)
        {
            if (HoverOnThis())
            {
                Game.SetCurrentScene(new Play());
            }
        }
    }

}
