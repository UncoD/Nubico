using Nubico.Controllers;
using Nubico.Interfaces;
using Nubico.Objects;
using Nubico.Objects.Physics;
using SFML.System;
using SFML.Window;

namespace Nubico.GameBase
{
    /// <summary>
    /// <br>Игровая сцена, часть игры. Управляет объектами, отображает их на экране</br>
    /// <br>Может запускать музыку и реагировать на ввод с клавиатуры и мыши</br>
    /// </summary>
    public class GameScene : IOnKeyPressable, IOnMouseClickable
    {
        /// <summary>
        /// Текущая игра, которой принадлежит сцена
        /// </summary>
        public readonly Game Game = GameProvider.ProvideDependency();

        /// <summary>
        /// Контроллер звуков
        /// </summary>
        public readonly SoundController SoundController = new SoundController();
        /// <summary>
        /// Контроллер музыки - общий для всей игры
        /// </summary>
        public readonly MusicController MusicController;

        /// <summary>
        /// Список всех игровых объектов на сцене на текущем кадре
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }
        private readonly List<GameObject> objectsPreparedToRemove = new List<GameObject>();
        private readonly List<GameObject> objectsPreparedToAdd = new List<GameObject>();
        private readonly List<IOnCollidable> collidables = new List<IOnCollidable>();

        /// <summary>
        /// <br>Конструктор игровой сцены</br>
        /// <br>Можно инициализировать игровые объекты и добавлять их на создаваемую сцену</br>
        /// </summary>
        public GameScene()
        {
            CurrentSceneProvider.SetDependency(this);
            MusicController = Game.MusicController;
            GameObjects = new List<GameObject>();
        }

        internal void DrawScene()
        {
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.IsBroken)
                {
                    continue;
                }
                Game.Window.Draw(gameObject);
            }
        }

        internal void UpdateScene()
        {
            CheckIntersections();
            CheckKeyPressed();
            CheckMouseClicked();
            DeletePreparedObjects();
            AddPreparedObjects();

            UpdateObjects();

            OnEachFrame();

            ClearIntersections();
        }
        
        private void UpdateObjects()
        {
            foreach (var gameObject in GameObjects)
            {
                if (gameObject is PhysicsObject)
                {
                    ;
                }
                if (gameObject.IsBroken)
                {
                    continue;
                }
                gameObject.UpdateObject();
            }
        }

        private void CheckKeyPressed()
        {
            if (Game.PressedKeys.Any())
            {
                OnKeyPress(Game.PressedKeys);
                GameObjects.ForEach(o => o.OnKeyPress(Game.PressedKeys));
                foreach (var key in Game.PressedKeys.ToList())
                {
                    OnKeyPress(key.Key, key.Value);

                    foreach (var item in GameObjects.Where(item => !item.IsBroken))
                    {
                        item.OnKeyPress(key.Key, key.Value);
                    }

                    Game.PressedKeys[key.Key] = true;
                }
            }
        }

        private void CheckMouseClicked()
        {
            if (Game.ClickedMouseButtons.Any())
            {
                OnMouseClick(Game.ClickedMouseButtons);
                GameObjects.ForEach(o => o.OnMouseClick(Game.ClickedMouseButtons));

                foreach (var button in Game.ClickedMouseButtons.ToList())
                {
                    OnMouseClick(button.Key, new Vector2i(button.Value.x, button.Value.y), button.Value.IsAlreadyClicked);

                    foreach (var item in GameObjects.Where(item => !item.IsBroken))
                    {
                        item.OnMouseClick(button.Key, new Vector2i(button.Value.x, button.Value.y), button.Value.IsAlreadyClicked);
                    }

                    Game.ClickedMouseButtons[button.Key] = (button.Value.x, button.Value.y, true);
                }
            }
        }

        private void CheckIntersections()
        {
            for (var i = 0; i < collidables.Count - 1; i++)
            {
                for (var j = i + 1; j < collidables.Count; j++)
                {
                    if (collidables[i] is AreaObject first &&
                        collidables[j] is AreaObject second &&
                        first.IsIntersects(second))
                    {
                        collidables[j].OnCollide(first);
                        collidables[i].OnCollide(second);
                    }
                }
            }
        }

        private void ClearIntersections()
        {
            for (var i = 0; i < collidables.Count; i++)
            {
                if (collidables[i] is AreaObject obj)
                {
                    obj.ClearCollide();
                }
            }
        }

        internal void DeleteFromScene(GameObject gameObject)
        {
            objectsPreparedToRemove.Add(gameObject);
            gameObject.IsBroken = true;
        }

        /// <summary>
        /// <br>Добавить объект(ы) в игровую сцену, чтобы отразить на экране</br>
        /// <br>Можно перечислить несколько объектов через запятую</br>
        /// </summary>
        /// <param name="gameObjects"></param>
        public void AddToScene(params GameObject[] gameObjects)
        {
            objectsPreparedToAdd.AddRange(gameObjects);
        }

        private void DeletePreparedObjects()
        {
            foreach (var gameObject in objectsPreparedToRemove)
            {
                GameObjects.Remove(gameObject);

                if (gameObject is IOnCollidable collide)
                {
                    collidables.Remove(collide);
                }
            }
            
            objectsPreparedToRemove.Clear();
        }

        private void AddPreparedObjects()
        {
            foreach (var item in objectsPreparedToAdd)
            {
                item.IsBroken = false;
                GameObjects.Add(item);

                if (item is IOnCollidable collide)
                {
                    collidables.Add(collide);
                }
            }

            GameObjects = GameObjects.OrderBy(o => o.DrawPriority).ToList();
            
            objectsPreparedToAdd.Clear();
        }

        /// <summary>
        /// Вызывается на каждом кадре, содержит описание логики обновления сцены во время работы приложения
        /// </summary>
        public virtual void OnEachFrame() { }

        /// <summary>
        /// Вызывается при нажатии клавиши клавиатуры
        /// </summary>
        /// <param name="pressedKey">Нажатая клавиша</param>
        /// <param name="isAlreadyPressed">Была ли нажата клавиша на предыдущем кадре (определение удержания клавиши)</param>
        public virtual void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed) { }

        /// <summary>s
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
    }
}