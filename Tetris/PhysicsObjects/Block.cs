using Ungine;
using SFML.System;

namespace Tetris
{   public class Block : PhysicsObject
    {
        public GameObject owner;
        public string frozenTex;
        public int type;
        public Block(string pathToTexture, float x, float y, GameObject owner) : base(x, y, pathToTexture)
        {
            float scale = Config.blockSize / Width;
            Scale = new Vector2f(scale, scale);
            this.owner = owner;


            if (owner is Shape)
                type = 1;
            else
                type = 0;
            frozenTex = pathToTexture.Split('.')[0] + "_Frozen." + pathToTexture.Split('.')[1];
        }

        public void ChangeSprite()
        {            
            SetSprite(frozenTex);
            float scale = Config.blockSize / Width;
            Scale = new Vector2f(scale, scale);
        }

    }
}
