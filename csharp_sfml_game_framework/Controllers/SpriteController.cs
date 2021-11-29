using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace Ungine
{
    /// <summary>
    /// <br>Контроллер спрайтов</br>
    /// <br>Управление отображением игрового объекта</br>
    /// </summary>
    internal class SpriteController
    {
        internal Sprite CurrentSprite { get; private set; }
        private string currentAnimation = "";
        private readonly Dictionary<string, Texture> textures = TexturesProvider.ProvideDependency();
        private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private readonly Dictionary<string, AnimationController> animations = new Dictionary<string, AnimationController>();

        internal void CreateSprite(string pathToTexture)
        {
            var texture = CheckTexture(pathToTexture);
            CheckAndSetSprite(pathToTexture, texture);
        }

        private void CheckAndSetSprite(string pathToTexture, Texture texture)
        {
            if (sprites.ContainsKey(pathToTexture))
            {
                CurrentSprite = sprites[pathToTexture];
            }
            else
            {
                var newSprite = new Sprite(texture);
                newSprite.Origin = new Vector2f(newSprite.GetGlobalBounds().Width / 2,
                                                newSprite.GetGlobalBounds().Height / 2);
                CurrentSprite = newSprite;

                sprites.Add(pathToTexture, newSprite);
            }
        }

        private Texture CheckTexture(string pathToTexture)
        {
            Texture texture;
            if (textures.ContainsKey(pathToTexture))
            {
                texture = textures[pathToTexture];
            }
            else
            {
                texture = new Texture(pathToTexture);
                textures.Add(pathToTexture, texture);
            }

            return texture;
        }

        /// <summary>
        /// Добавить анимацию объекту
        /// </summary>
        /// <param name="animationName">Название анимации</param>
        /// <param name="delay">Задержка между кадрами анимации в секундах</param>
        /// <param name="pathsToTextures">Пути к файлам кадров (.png, .jpg) анимации через запятую</param>
        public void AddAnimation(string animationName, float delay, params string[] pathsToTextures)
        {
            if (!animations.ContainsKey(animationName))
            {
                animations.Add(animationName,
                    new AnimationController(delay, pathsToTextures.Select(CheckTexture).ToArray()));
            }
            else
            {
                throw new ArgumentException("Анимация с таким именем уже существует");
            }
        }

        /// <summary>
        /// Удалить анимацию по названию
        /// </summary>
        /// <param name="animationName">Название удаляемой анимации</param>
        public void RemoveAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                animations.Remove(animationName);
            }
        }

        internal void UpdateAnimation()
        {
            if (animations.ContainsKey(currentAnimation) && animations[currentAnimation].Playing)
            {
                CurrentSprite = animations[currentAnimation].NextSprite();
            }
        }

        /// <summary>
        /// Возвращает название текущей проигрываемой анимации
        /// </summary>
        /// <returns>Название проигрываемой анимации</returns>
        public string CurrentAnimationName()
        {
            return animations.ContainsKey(currentAnimation) && animations[currentAnimation].Playing ? currentAnimation : "";
        }

        /// <summary>
        /// Запустить анимацию по имени, если она была добавлена в объект
        /// </summary>
        /// <param name="animationName">Название запускаемой анимации</param>
        /// <param name="restart">Перезапустить анимацию с первого кадра</param>
        public void PlayAnimation(string animationName, bool restart = false, object parent = null)
        {
            if (!animations.ContainsKey(animationName))
            {
                throw new ArgumentException($"Анимация с именем {animationName} не найдена в объекте {parent.GetType()}");
            }


            if (animations.ContainsKey(currentAnimation) && currentAnimation != animationName)
            {
                animations[currentAnimation].StopAnimation();
            }
            
            currentAnimation = animationName;

            if (restart)
            {
                animations[currentAnimation].RestartAnimaton();
            }
            animations[currentAnimation].PlayAnimation();
        }

        /// <summary>
        /// Остановить проигрывание анимации
        /// </summary>
        /// <param name="withRestart">Сбросить анимацию к первому кадру</param>
        public void StopAnimation(bool withRestart = false)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (withRestart)
                {
                    animations[currentAnimation].RestartAnimaton();
                }
                CurrentSprite = animations[currentAnimation].NextSprite();
                animations[currentAnimation].StopAnimation();
            }
        }

        /// <summary>
        /// Установить частоту обновления кадров для конкретной анимаии
        /// </summary>
        /// <param name="animationName">Название добавленной анимации</param>
        /// <param name="delay">Задержка между кадрами в секундах</param>
        public void SetAnimationDelay(string animationName, float delay)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                animations[animationName].SetDelay(delay);
            }
        }

        internal void SynchronizeSprite(GameObject owner)
        {
            if (CurrentSprite != null)
            {
                CurrentSprite.Rotation = owner.Rotation;
                CurrentSprite.Origin = owner.Origin;
                CurrentSprite.Scale = owner.Scale;
                CurrentSprite.Position = owner.Position;
            }
        }

        /// <summary>
        /// Получить ширину спрайта
        /// </summary>
        /// <returns>Ширина спрайта</returns>
        public float GetWidth()
        {
            if (CurrentSprite != null)
                return CurrentSprite.GetGlobalBounds().Width;
            return 0;
        }

        /// <summary>
        /// Получить высоту спрайта
        /// </summary>
        /// <returns>Высота спрайта</returns>
        public float GetHeight()
        {
            if (CurrentSprite != null)
                return CurrentSprite.GetGlobalBounds().Height;
            return 0;
        }

        /// <summary>
        /// <br>Получить границы спрайта</br>
        /// <br>Соответствуют границам текущей текстуры спрайта</br>
        /// <br>Содержит координаты верхнего левого угла относительно окна приложения</br>
        /// </summary>
        /// <returns>Прямоугольник, обозначающий границы спрайта</returns>
        public FloatRect GetBounds()
        {
            return CurrentSprite.GetGlobalBounds();
        }

        /// <summary>
        /// Установить цвет поверх тектсуры спрайта
        /// </summary>
        /// <param name="color">Устанавливаемый цвет</param>
        public void SetColor(Color color)
        {
            if (CurrentSprite != null)
                CurrentSprite.Color = color;
        }

        internal void TryDraw(RenderTarget target)
        {
            if (CurrentSprite != null)
            {
                target.Draw(CurrentSprite);
            }
        }
    }
}