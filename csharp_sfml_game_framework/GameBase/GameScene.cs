using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace csharp_sfml_game_framework
{
    /// <summary>
    /// Игровая сцена, часть игры. Управляет объектами, отображает их на экране. Может запускать музыку и реагировать на нажатия.
    /// </summary>
    public class GameScene : IOnKeyPressable, IOnMouseClickable
    {
        public Game Game = GameProvider.ProvideDependency();

        // Списки объектов
        public  List<GameObject> GameObjects = new List<GameObject>();
        public SoundController SoundController = new SoundController();
        public MusicController MusicController;
        private readonly List<GameObject> objectsPreparedToRemove = new List<GameObject>();
        private readonly List<GameObject> objectsPreparedToAdd = new List<GameObject>();
        private  List<IOnCollidable> collidables = new List<IOnCollidable>();

        /// <param name="game">Игровая сцена должна знать об игре, в которой она находится.</param>
        public GameScene()
        {
            CurrentSceneProvider.SetDependency(this);
            MusicController = Game.MusicController;
        }

        public void DrawScene()
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

        public void UpdateScene()
        {
            CheckIntersections();
            CheckKeyPressedAndMouseClicked();
            DeletePreparedObjects();
            AddPreparedObjects();

            UpdateObjects();

            OnEachFrame();
        }
        
        private void UpdateObjects()
        {
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.IsBroken)
                {
                    continue;
                }
                gameObject.UpdateObject();
            }
        }

        private void CheckKeyPressedAndMouseClicked()
        {
            if (Game.PressedKeys.Any())
            {
                foreach (var key in Game.PressedKeys.ToList())
                {
                    OnKeyPress(key.Key, key.Value);

                    foreach (var item in GameObjects)
                    {
                        if (item.IsBroken)
                        {
                            continue;
                        }
                        item.OnKeyPress(key.Key, key.Value);
                    }

                    Game.PressedKeys[key.Key] = true;
                }
            }

            if (Game.ClickedMouseButtons.Any())
            {
                foreach (var button in Game.ClickedMouseButtons.ToList())
                {
                    OnMouseClick(button.Key, new Vector2i(button.Value.x, button.Value.y), button.Value.IsAlreadyClick);

                    foreach (var item in GameObjects)
                    {
                        if (item.IsBroken)
                        {
                            continue;
                        }
                        item.OnMouseClick(button.Key, new Vector2i(button.Value.x, button.Value.y), button.Value.IsAlreadyClick);
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
                    if (collidables[i] is PhysicsObject first &&
                        collidables[j] is PhysicsObject second &&
                        first.IsIntersects(second))
                    {
                        collidables[j].OnCollide(first);
                        collidables[i].OnCollide(second);
                    }
                }
            }
        }

        internal void DeleteFromScene(GameObject gameObject)
        {
            objectsPreparedToRemove.Add(gameObject);
            gameObject.IsBroken = true;
        }

        /// <summary>
        /// Добавить объект(ы) в игровую сцену, чтобы отразить на экране и начать с ним(и) работу.
        /// Можно перечислить несколько объектов через запятую.
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
                item.Game = Game;
                item.GameScene = this;
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

        public virtual void OnEachFrame() { }
        public virtual void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed) { }
        public virtual void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClick) { }
    }
}