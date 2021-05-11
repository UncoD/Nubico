using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace csharp_sfml_game_framework
{
    internal class SpriteController
    {
        internal Sprite CurrentSprite { get; private set; }
        private AnimationController currentAnimation;
        private bool isAnimationPlay;
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

        public void AddAnimation(string animationName, int frequency, params string[] pathsToTextures)
        {
            if (!animations.ContainsKey(animationName))
            {
                animations.Add(animationName,
                    new AnimationController(frequency, pathsToTextures.Select(CheckTexture).ToArray()));
            }
            else
            {
                throw new ArgumentException("Анимация с таким именем уже существует");
            }
        }

        internal void UpdateAnimation()
        {
            if (isAnimationPlay)
            {
                CurrentSprite = currentAnimation.NextSprite();
            }
        }

        public void PlayAnimation(string animationName)
        {
            isAnimationPlay = true;
            if (!animations.ContainsKey(animationName))
            {
                throw new ArgumentException("Анимация с таким именем не найдена");
            }

            currentAnimation = animations[animationName];
        }

        public void StopAnimation()
        {
            isAnimationPlay = false;
        }

        public void SetAnimationDelay(string animationName, int delay)
        {
            animations[animationName].SetDelay(delay);
        }

        internal void SynchronizeSprite(GameObject owner)
        {
            if (CurrentSprite != null)
            {
                CurrentSprite.Rotation = owner.Rotation;
                CurrentSprite.Scale = owner.Scale;
                CurrentSprite.Origin = owner.Origin;
                CurrentSprite.Position = owner.Position;
            }
        }

        public float GetWidth()
        {
            if (CurrentSprite != null)
                return CurrentSprite.GetGlobalBounds().Width;
            return 0;
        }

        public float GetHeight()
        {
            if (CurrentSprite != null)
                return CurrentSprite.GetGlobalBounds().Height;
            return 0;
        }

        public FloatRect GetBounds()
        {
            return CurrentSprite.GetGlobalBounds();
        }

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