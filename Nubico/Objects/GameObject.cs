using Nubico.Controllers;
using Nubico.GameBase;
using Nubico.Interfaces;
using Nubico.Objects.Physics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Nubico.Objects
{
    /// <summary>
    /// Класс базового игрового объекта, отвечает за отображения и поведение, реагирует на ввод с клавиатуры и мыши
    /// </summary>
    public class GameObject : Transformable, Drawable, IOnKeyPressable, IOnMouseClickable
    {
        /// <summary>
        /// Ссылка на игру, в которой находится объект
        /// </summary>
        public readonly Game Game = GameProvider.ProvideDependency();
        /// <summary>
        /// Ссылка на сцену, на которой находится объект
        /// </summary>
        public GameScene GameScene => CurrentSceneProvider.ProvideDependency();
        /// <summary>
        /// Приоритет отрисовки объекта (чем меньше, тем раньше отображается объекта, тем "дальше" он от игрока)
        /// </summary>
        public int DrawPriority = 0;
        /// <summary>
        /// Контроллер спрайтов
        /// </summary>
        internal SpriteController SpriteController;
        /// <summary>
        /// Позиция объекта по горизонтали
        /// </summary>
        public float X => Position.X;
        /// <summary>
        /// Позиция объекта по вертикали
        /// </summary>
        public float Y => Position.Y;
        /// <summary>
        /// Ширина объекта
        /// </summary>
        public float Width => SpriteController.GetWidth();
        /// <summary>
        /// Высота объекта
        /// </summary>
        public float Height => SpriteController.GetHeight();
        /// <summary>
        /// Вектор скорости перемещения
        /// </summary>
        public Vector2f Velocity;
        /// <summary>
        /// Контроллер звуков
        /// </summary>
        protected SoundController SoundController = new();
        /// <summary>
        /// Контроллер музыки - общий для всей игры
        /// </summary>
        protected MusicController MusicController;

        /// <summary>
        /// Удален ли объект со сцены
        /// </summary>
        public bool IsBroken { get; internal set; }

        /// <summary>
        /// Конструктор игрового объекта, содержащего графическое представление
        /// </summary>
        /// <param name="x">Горизонтальная позиция</param>
        /// <param name="y">Вертикальная позиция</param>
        /// <param name="pathToTexture">Путь к файлу изображения (.png, .jpg)</param>
        public GameObject(float x, float y, string pathToTexture) : this (x, y)
        {
            SetSprite(pathToTexture);
        }

        /// <summary>
        /// Конструктор объекта без начального графического представления
        /// </summary>
        /// <param name="x">Горизонтальная позиция</param>
        /// <param name="y">Вертикальная позиция</param>
        public GameObject(float x, float y)
        {
            SpriteController = new SpriteController();
            MusicController = Game.MusicController;
            Position = new Vector2f(x, y);
        }

        /// <summary>
        /// Удалить объект со сцены
        /// </summary>
        public void DeleteFromGame()
        {
            BeforeDeleteFromScene();
            IsBroken = true;
            GameScene.DeleteFromScene(this);
        }

        internal virtual void UpdateObject()
        {
            Position += Velocity;

            OnEachFrame();

            SpriteController.UpdateAnimation();
            SpriteController.SynchronizeSprite(this);
        }

        /// <summary>
        /// <br>Отображает игровой объект в окне приложения</br>
        /// <br>Если Game.DrawObjectBorders = true, отображает границы объекта</br>
        /// </summary>
        /// <param name="target">Цель отрисовки (окно приложения)</param>
        /// <param name="states">Параметры трансформации при отображениии</param>
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            SpriteController.TryDraw(target);

            if (Game.DrawObjectBorders)
            {
                var border = new RectangleShape(new Vector2f(Width - 2, Height - 2))
                {
                    OutlineColor = Color.Green,
                    FillColor = Color.Transparent,
                    OutlineThickness = 1,
                    Origin = new Vector2f((Width - 2) / 2, (Height - 2) / 2),
                    Scale = new Vector2f(Math.Sign(Scale.X), Math.Sign(Scale.Y)),
                    Position = Position,
                };

                target.Draw(border);
            }
        }

        /// <summary>
        /// Добавить анимацию объекту
        /// </summary>
        /// <param name="animationName">Название анимации</param>
        /// <param name="delay">Задержка между кадрами анимации в секундах</param>
        /// <param name="pathsToTextures">Пути к файлам кадров (.png, .jpg) анимации через запятую</param>
        public void AddAnimation(string animationName, float delay, params string[] pathsToTextures)
        {
            SpriteController.AddAnimation(animationName, delay, pathsToTextures);
        }

        /// <summary>
        /// Удалить анимацию по названию
        /// </summary>
        /// <param name="animationName">Название удаляемой анимации</param>
        public void RemoveAnimation(string animationName)
        {
            SpriteController.RemoveAnimation(animationName);
        }

        /// <summary>
        /// Запустить анимацию по имени, если она была добавлена в объект
        /// </summary>
        /// <param name="animationName">Название запускаемой анимации</param>
        /// <param name="restart">Перезапустить анимацию с первого кадра</param>
        public void PlayAnimation(string animationName, bool restart = false)
        {
            SpriteController.PlayAnimation(animationName, restart, this);
        }

        /// <summary>
        /// Остановить проигрывание анимации
        /// </summary>
        /// <param name="withRestart">Сбросить анимацию к первому кадру</param>
        public void StopAnimation(bool withRestart = false)
        {
            SpriteController.StopAnimation(withRestart);
        }

        /// <summary>
        /// Возвращает название текущей проигрываемой анимации
        /// </summary>
        /// <returns>Название проигрываемой анимации</returns>
        public string CurrentAnimationName => SpriteController.CurrentAnimationName();

        /// <summary>
        /// Установить частоту обновления кадров для конкретной анимаии
        /// </summary>
        /// <param name="animationName">Название добавленной анимации</param>
        /// <param name="delay">Задержка между кадрами в секундах</param>
        public void SetAnimationDelay(string animationName, float delay)
        {
            SpriteController.SetAnimationDelay(animationName, delay);
        }

        /// <summary>
        /// Установить графическое отображение для объекта (спрайт)
        /// </summary>
        /// <param name="path">Путь к файлу изображения (.png, .jpg)</param>
        public void SetSprite(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            try
            {
                SpriteController.CreateSprite(path);
                Origin = SpriteController.CurrentSprite.Origin;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Установить цвет поверх объекта
        /// </summary>
        /// <param name="color">Устанавливаемый цвет</param>
        public void SetSpriteColor(Color color)
        {
            SpriteController.SetColor(color);
        }

        /// <summary>
        /// Отразить объект по горизонтали
        /// </summary>
        public void FlipX()
        {
            Scale = new Vector2f(-Scale.X, Scale.Y);
        }
        /// <summary>
        /// Отразить объект по вертикали
        /// </summary>
        public void FlipY()
        {
            Scale = new Vector2f(Scale.X, -Scale.Y);
        }

        /// <summary>
        /// Вызывается на каждом кадре, содержит описание логики обновления объекта во время работы приложения
        /// </summary>
        public virtual void OnEachFrame() { }

        /// <summary>
        /// Вызывается при нажатии клавиши клавиатуры
        /// </summary>
        /// <param name="pressedKey">Нажатая клавиша</param>
        /// <param name="isAlreadyPressed">Была ли нажата клавиша на предыдущем кадре (определение удержания клавиши)</param>
        public virtual void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed) { }

        /// <summary>
        /// Вызывается при нажатии клавиш клавиатуры
        /// </summary>
        /// <param name="pressedKeys">Список нажатых клавиш на текущем кадре</param>
        public virtual void OnKeyPress(Dictionary<Keyboard.Key, bool> pressedKeys) { }

        /// <summary>
        /// <br>Вызывается при любом нажатии мыши, даже мимо объекта</br>
        /// <br>Для проверки нажатия на объект использовать условие HoverOnThis()</br>
        /// </summary>
        /// <param name="mouseButton">Нажатая кнопка</param>
        /// <param name="position">Позиция указателя в момент клика</param>
        /// <param name="isAlreadyClicked">Была ли нажата кнопка на предыдущем кадре (определение зажатия)</param>
        public virtual void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked) { }

        /// <summary>
        /// <br>Вызывается при любом нажатии мыши, даже мимо объекта</br>
        /// <br>Для проверки нажатия на объект использовать условие HoverOnThis()</br>
        /// </summary>
        /// <param name="mouseButtons">Список нажатых кнопок мыши</param>
        public virtual void OnMouseClick(Dictionary<Mouse.Button, (int x, int y, bool IsAlreadyClicked)> mouseButtons) { }

        /// <summary>
        /// Вызывается перед удалением объекта со сцены
        /// </summary>
        public virtual void BeforeDeleteFromScene() { }

        /// <summary>
        /// Проверка, что указанная позиция находится над объектом
        /// </summary>
        /// <param name="position">Позиция точки для проверки</param>
        /// <returns>Находится ли точка над объектом</returns>
        public bool HoverOnThis(Vector2i position)
        {
            return SpriteController.GetBounds().Contains(position.X, position.Y);
        }

        /// <summary>
        /// Проверка, что курсор наведен на данный объект
        /// </summary>
        /// <returns>Наведен ли курсор на объект</returns>
        public bool HoverOnThis()
        {
            return HoverOnThis(Mouse.GetPosition(Game.Window));
        }
    }
}