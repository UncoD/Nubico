using Ungine;
using SFML.System;
using System.Collections.Generic;

namespace Tetris
{
    public class Ground : GameObject
    {
        private bool initialized = false;
        private List<Block> blocks;

        private TextObject Score, Lines;
        private int lines = 0;

        private string Ground_Block = "Art/Ground_Block.png";
        private string Frozen_Block = "Art/Ground_Block.png";
        private string Side_Block = "Art/Empty_Block.png";

        public Ground() : base(0, 0)
        {
            blocks = new List<Block>();
            float startX = Config.width / 2 - (float)Config.column / 2.0f * Config.blockSize + Config.blockSize / 2 - Config.leftOffset;
            for (int i = 0; i < Config.column; i++) 
                blocks.Add(new Block(Ground_Block, startX + i * Config.blockSize,
                    Config.upperOffset + Config.row * Config.blockSize - Config.blockSize / 2, this));

            for (int i = 0; i < Config.row - 1; i++) 
            {
                blocks.Add(new Block(Side_Block, startX - Config.blockSize ,
                    Config.upperOffset + Config.blockSize / 2 + i * Config.blockSize, this));

                blocks.Add(new Block(Side_Block, startX + Config.blockSize * Config.column,
                    Config.upperOffset + Config.blockSize / 2 + i * Config.blockSize, this));
            }
        }

        public override void OnEachFrame()
        {
            cheakLevel();
            if (!initialized)
            {
                Lines = new TextObject("0", 69, 220);
                Score = new TextObject("0", 69, 340);
                GameScene.AddToScene(Lines, Score);
                initialized = true;
                foreach (var block in blocks)
                {
                    GameScene.AddToScene(block);
                }
            }
            Lines.SetText(lines);
            Lines.Origin = new Vector2f(Lines.GetBounds().Width/2, 0);
            Score.SetText(Game.Score);
            Score.Origin = new Vector2f(Score.GetBounds().Width/2, 0);
            base.OnEachFrame();
        }

        private void cheakLevel()
        {
            Block[,] field = new Block[Config.column, Config.row];
            int[] count = new int[Config.row];

            int start = Config.width / 2 - Config.column / 2 * Config.blockSize + Config.blockSize / 2;
            foreach (var block in blocks)
            {
                int posY = ((int)block.Position.Y - Config.upperOffset) / Config.blockSize;
                int posX = ((int)block.Position.X - start) / Config.blockSize;
                if (posX > -1 && posX < Config.column)
                    field[posX, posY] = block;
                count[posY]++;
            }

            int removed = 0;
            for (int i = Config.row - 1; i >= 0; i--)
            {
                if (removed > 0)
                    for (int j = 0; j < Config.column; j++)
                        if (field[j, i] != null)
                            field[j, i].MoveIt(0, Config.blockSize * removed);

                if (count[i] == Config.column + 2)
                {
                    for (int j = 0; j < Config.column; j++)
                    {
                        field[j, i].DeleteFromGame();
                        blocks.Remove(field[j, i]);
                        field[j, i] = null;
                    }
                    SoundController.PlaySound("Music/remove.ogg");
                    removed++;
                    lines++;
                }
            }
            switch (removed)
            {
                case 1:
                    Game.Score += 100;
                    break;
                case 2:
                    Game.Score += 400;
                    break;
                case 3:
                    Game.Score += 900;
                    break;
                case 4:
                    Game.Score += 2000;
                    break;
            }
        }

        public void addBlocks(List<Block> blocks)
        {
            this.blocks.AddRange(blocks);
            foreach(var block in blocks)
            {                
                block.ChangeSprite();
                block.type = 0;
                block.owner = this;
            }
        }
    }
}
