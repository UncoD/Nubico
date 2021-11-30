using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Nubico.Controllers
{
    internal class AnimationController
    {
        private float delayFrames;
        private List<Sprite> sprites = new List<Sprite>();
        private int currentIndexOfSprite;
        private Clock clock = new Clock();
        public bool Playing { get; private set; }

        internal AnimationController(float delayFrames, params Texture[] textures)
        {
            this.delayFrames = delayFrames;
            foreach (var texture in textures)
            {
                sprites.Add(new Sprite(texture));
            }
        }

        internal void SetDelay(float delay)
        {
            delayFrames = delay;
        }

        internal Sprite NextSprite()
        {
            if (clock != null && clock.ElapsedTime.AsSeconds() > delayFrames)
            {
                currentIndexOfSprite = (currentIndexOfSprite + 1) % sprites.Count;
                clock.Restart();
            }

            return sprites[currentIndexOfSprite];
        }

        internal void PlayAnimation()
        {
            Playing = true;
            if (clock == null)
            {
                clock = new Clock();
            }
        }

        internal void StopAnimation()
        {
            Playing = false;
            clock = null;
        }

        internal void RestartAnimaton()
        {
            currentIndexOfSprite = 0;
        }
    }
}