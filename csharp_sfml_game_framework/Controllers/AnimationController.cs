using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace csharp_sfml_game_framework
{
    internal class AnimationController
    {
        private int delayFrames;
        private int counter;
        private List<Sprite> sprites = new List<Sprite>();
        private int currentIndexOfSprite;

        internal AnimationController(int delayFrames, params Texture[] textures)
        {
            this.delayFrames = delayFrames;
            foreach (var texture in textures)
            {
                sprites.Add(new Sprite(texture));
            }
        }

        internal void SetDelay(int delay)
        {
            delayFrames = delay;
        }

        public Sprite NextSprite()
        {
            if (counter == 0)
            {
                currentIndexOfSprite = (currentIndexOfSprite + 1) % sprites.Count;
            }

            if (delayFrames != 0)
            {
                counter = (counter + 1) % delayFrames;
            }

            return sprites[currentIndexOfSprite];
        }
    }
}