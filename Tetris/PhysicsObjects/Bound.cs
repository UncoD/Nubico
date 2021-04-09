using csharp_sfml_game_framework;
using SFML.System;

namespace Tetris
{
    public class Bound :PhysicsObject
    {
        public Shape owner;
        public Block myBlock;
        public Bound(string pathToTexture, float x, float y, Shape owner, Block myBlock) : base(x, y, pathToTexture)
        {
            float scale = Config.blockSize / Width;
            Scale = new Vector2f(scale, scale); 

            this.owner = owner;
            this.myBlock = myBlock;
        }

        public override void OnCollide(GameObject gameObject)
        {
            if(gameObject is Block block)
                if(block.type == 0)
                {
                    if(block.Position.X == myBlock.Position.X)
                    {
                        if (myBlock.Position.Y <= Config.upperOffset + Config.blockSize * 2)
                            Game.OnLose();
                        if (block.Position.Y == myBlock.Position.Y + Config.blockSize)
                        {
                            if (!owner.needKill)
                                (block.owner as Ground).addBlocks(owner.blocks);
                            owner.needKill = true;
                        }
                    }                    
                        owner.returnBounds();
                }                 
        }
    }
}
