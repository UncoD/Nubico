using System.Collections.Generic;
using System.Threading;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using Nubico.Controllers;
using Nubico.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Nubico.GameBase
{
    /// <summary>
    /// Базовый класс игры
    /// </summary>
    public class Game
    {
        private GameScene currentScene;
        private readonly Clock clock = new();

        internal readonly RenderWindow Window;
        internal World PhysicsWorld;

        /// <summary>
        /// Список нажатых клавиш на текущем кадре
        /// </summary>
        public readonly Dictionary<Keyboard.Key, bool> PressedKeys = new();
        /// <summary>
        /// Список нажатых кнопок мыши на текущем кадре
        /// </summary>
        public readonly Dictionary<Mouse.Button, (int x, int y, bool IsAlreadyClicked)> ClickedMouseButtons = new();

        /// <summary>
        /// Контроллер музыки - общий для всего игрового приложения
        /// </summary>
        public readonly MusicController MusicController;
        /// <summary>
        /// Контроллер звуков
        /// </summary>
        public readonly SoundController SoundController;

        /// <summary>
        /// Рисовать ли границы игровых объектов (зеленый прямоугольник вокруг объектов)
        /// </summary>
        public bool DrawObjectBorders = false;

        private Vector2f _gravity;
        public Vector2f Gravity
        {
            get => _gravity;
            set
            {
                _gravity = value;
                PhysicsWorld.Gravity = _gravity.ToVec();
            }
        }

        /// <summary>
        /// Конструктор, для создания объекта Игра (инициализация окна приложения)
        /// </summary>
        /// <param name="width">Ширина окна приложения</param>
        /// <param name="height">Высота окна приложения</param>
        /// <param name="name">Заголовок окна приложения</param>
        /// <param name="style">
        /// <br>По умолчанию активна кнопка скрытия окна, нельзя изменять размер</br>
        /// <br>Styles.Fullscreen - полноэкранный режим</br>
        /// <br>Styles.Default - можно закрывать/сворачивать окно, изменять размер</br>
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

            Window.KeyPressed += (s, e) => PressedKeys[e.Code] = false;
            Window.KeyReleased += (s, e) => PressedKeys.Remove(e.Code);
            Window.MouseButtonPressed += (s, e) => ClickedMouseButtons[e.Button] = (e.X, e.Y, false);
            Window.MouseButtonReleased += (s, e) => ClickedMouseButtons.Remove(e.Button);

            MusicController = new MusicController();
            MusicController.SetLoop(true);

            var worldAABB = new AABB();
            worldAABB.LowerBound.Set(-100.0f);
            worldAABB.UpperBound.Set(100.0f);
            var gravity = new Vec2(0.0f, 10f);
            const bool DoSleep = true;
            PhysicsWorld = new World(worldAABB, gravity, DoSleep);
            Gravity = gravity.ToVector();
        }

        /// <summary>
        /// Ширина окна приложения
        /// </summary>
        public int Width => (int)Window.Size.X;
        /// <summary>
        /// Высота окна приложения
        /// </summary>
        public int Height => (int)Window.Size.Y;
        /// <summary>
        /// Текущая позиция курсора мыши в окне приложения
        /// </summary>
        public Vector2f CursorPosition => (Vector2f)Mouse.GetPosition(Window);

        /// <summary>
        /// Установить заголовок окна приложения
        /// </summary>
        /// <param name="title">Устанавливаемый заголовок окна</param>
        public void SetWindowTitle(string title)
        {
            Window.SetTitle(title);
        }

        /// <summary>
        /// Установка размеров окна приложения
        /// </summary>
        /// <param name="width">Устанавливаемая ширина окна</param>
        /// <param name="height">Устанавливаемая высота окна</param>
        public void SetWindowSize(int width, int height)
        {
            Window.Position = new Vector2i(
                (int) ((VideoMode.DesktopMode.Width - width) / 2),
                (int) ((VideoMode.DesktopMode.Height - height) / 2));
            Window.Size = new Vector2u((uint) width, (uint) height);
        }

        /// <summary>
        /// Переключиться на другую сцену. Предыдущая сцена удалится вместе со всеми объектами.
        /// </summary>
        /// <param name="gameScene">Ссылка на запускаемую игровую сцену</param>
        public void SetCurrentScene(GameScene gameScene)
        {
            currentScene = gameScene;
            // Для предотвращения повторного реагирования на уже нажатую клавишу на новой сцене
            // поток усыпляется, чтобы клавиша успела отжаться
            Thread.Sleep(150);
            PressedKeys.Clear();
        }

        /// <summary>
        /// Закрыть окно приложения
        /// </summary>
        public void Close()
        {
            Window.Close();
        }

        /// <summary>
        /// Запустить игровой цикл
        /// </summary>
        public void Start()
        {
            const float TimeStep = 1.0f / 60.0f;
            const int VelocityIterations = 8;
            const int PositionIterations = 1;

            while (Window.IsOpen)
            {
                if (clock.ElapsedTime.AsSeconds() > 0.0004)
                {
                    Window.DispatchEvents();
                    clock.Restart();

                    PhysicsWorld.Step(TimeStep, VelocityIterations, PositionIterations);
                    currentScene.UpdateScene();

                    Window.Clear();
                    currentScene.DrawScene();

                    Window.Display();
                }
            }
        }
    }
}