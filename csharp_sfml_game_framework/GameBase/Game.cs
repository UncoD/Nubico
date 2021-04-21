using System.Collections.Generic;
using System.Threading;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace csharp_sfml_game_framework
{
    public class Game
    {
        public readonly Dictionary<Keyboard.Key, bool> PressedKeys = new Dictionary<Keyboard.Key, bool>();
        public readonly Dictionary<Mouse.Button, (int x, int y, bool IsAlreadyClick)> ClickedMouseButtons =
            new Dictionary<Mouse.Button, (int, int, bool)>();
        internal readonly RenderWindow Window;

        private GameScene currentScene;
        private Clock clock = new Clock();

        public SoundController SoundController;
        public MusicController MusicController;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <param name="style">
        /// По умолчанию активна кнопка скрытия окна, нельзя изменять размер
        /// Styles.Fullscreen - полноэкранный режим
        /// Styles.Default - можно закрывать/сворачивать окно, изменять раземер
        /// </param>

        public Game(int width, int height, string name, Styles style = Styles.Close)
        {
            // Для внедрения зависимости повсюду
            GameProvider.SetDependency(this);
            TexturesProvider.SetDependency(new Dictionary<string, Texture>());

            Window = new RenderWindow(new VideoMode((uint) width, (uint) height), name, style);
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (s, a) => Window.Close();
            Window.Resized += (s, a) => Window.SetView(new View(new FloatRect(0, 0, a.Width, a.Height)));

            Window.KeyPressed += (s, e) =>
            {
                PressedKeys[e.Code] = false;
            };

            Window.KeyReleased += (s, e) =>
            {
                PressedKeys.Remove(e.Code);
            };

            Window.MouseButtonPressed += (s, e) =>
            {
                ClickedMouseButtons[e.Button] = (e.X, e.Y, false);
            };

            Window.MouseButtonReleased += (s, e) =>
            {
                ClickedMouseButtons.Remove(e.Button);
            };
            
            SoundController = new SoundController();
            MusicController = new MusicController();
            MusicController.LoopMusic(true);
        }

        public int Width => (int) Window.Size.X;
        public int Height => (int) Window.Size.Y;
        public int Score { get; set; }

        //public Vector2f CursorPosition => Window.MapPixelToCoords(Mouse.GetPosition());
        public Vector2f CursorPosition => (Vector2f) Mouse.GetPosition(Window);

        public void SetWindowTitle(string title)
        {
            Window.SetTitle(title);
        }

        public void SetWindowSize(int width, int height)
        {
            Window.Position = new Vector2i(
                (int) ((VideoMode.DesktopMode.Width - width) / 2),
                (int) ((VideoMode.DesktopMode.Height - height) / 2));
            Window.Size = new Vector2u((uint) width, (uint) height);
        }

        /// <summary>
        ///     Переключиться на другую сцену. Предыдущая сцена удалится вместе со всеми объектами.
        /// </summary>
        /// <param name="gameScene"></param>
        public void SetCurrentScene(GameScene gameScene)
        {
            currentScene = gameScene;
            // Для предотвращения повторного реагирования на уже нажатую клавишу на новой сцене
            // поток усыпляется, чтобы клавиша успела отжаться
            Thread.Sleep(150);
            PressedKeys.Clear();
        }

        public void Close()
        {
            Window.Close();
        }

        public void Start()
        {
            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                if (clock.ElapsedTime.AsSeconds() > 0.004)
                {
                    clock.Restart();
                    currentScene.UpdateScene();
                }

                Window.Clear();
                currentScene.DrawScene();

                Window.Display();
            }
        }

        public virtual void OnLose()
        {
        }

        public virtual void OnWin()
        {
        }
    }
}