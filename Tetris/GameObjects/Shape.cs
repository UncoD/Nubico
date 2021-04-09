using csharp_sfml_game_framework;
using System.Collections.Generic;
using SFML.Window;
using SFML.System;
using System;

namespace Tetris
{

    public class Shape : GameObject
    {
        private Random rand = new Random();
        private bool initialized = false;
        
        private int num = 0;
        private uint tick = 0;
        private uint ltick = 0;
        private uint btick = 0;

        private int speed = 1; 

        public float ldx = 0, ldy = 0;
        public bool returned = false;
        public int lrot = 0;
        public bool needKill = false;

        private static int[,] figures = new int[,] { 
            { 1, 3, 5, 7 },     // I
            { 2, 4, 5, 7 },     // S
            { 3, 5, 4, 6 },     // Z
            { 3, 5, 4, 7 },     // T
            { 2, 3, 5, 7 },     // L
            { 3, 5, 7, 6 },     // J
            { 2, 3, 4, 5 } };   // O
        private List<Vector2f> centers = new List<Vector2f>
        {
            new Vector2f(2, 2),
            new Vector2f(0.5f, 2.5f),
            new Vector2f(1.5f, 2.5f),
            new Vector2f(1.5f, 2.5f),
            new Vector2f(1.5f, 2.5f),
            new Vector2f(1.5f, 2.5f),
            new Vector2f(1, 2) };
        private static List<string> colors = new List<string>  {
                "Art/Block_Green.png",
                "Art/Block_Blue.png",
                "Art/Block_Orange.png",
                "Art/Block_Purple.png",
                "Art/Block_Red.png",
                "Art/Block_Sapphirine.png",
                "Art/Block_Yellow.png",
            };
        public List<Block> blocks;
        private List<Bound> bounds;
        public Shape(int figure, int color) : base(0, 0)
        {
            num = figure;
            centers[figure] *= Config.blockSize;
            centers[figure] += new Vector2f(Config.width / 2 - Config.blockSize / 2 - Config.leftOffset, Config.upperOffset + Config.blockSize / 2);

            blocks = new List<Block>();
            bounds = new List<Bound>();
            for (int i = 0; i < 4; i++) 
            {
                int xOffset = figures[figure, i] % 2 * Config.blockSize;
                int yOffset = figures[figure, i] / 2 * Config.blockSize;
                Block block = new Block(colors[color], Config.width / 2 + xOffset - Config.blockSize / 2 - Config.leftOffset, Config.upperOffset + Config.blockSize / 2 + yOffset, this);
                blocks.Add(block);
                bounds.Add(new Bound("Art/Empty_Block.png", Config.width / 2 + xOffset - Config.blockSize / 2 - Config.leftOffset, Config.upperOffset + Config.blockSize / 2 + yOffset, this, block));
            }
        }

        public override void OnEachFrame()
        {
            if (tick % 2 == 1) 
                moveBound(0, speed);

            if (tick != btick)
            {
                if (needKill)
                {
                    var shape = new Shape(rand.Next() % 7, rand.Next() % Shape.getColors());
                    GameScene.AddToScene(shape);
                    DeleteFromGame();
                    foreach (var bound in bounds)
                        bound.DeleteFromGame();
                    SoundController.PlaySound("Music/hit.ogg");
                }
                    
                moveBlocks(ldx, ldy);
                rotateBlocks(lrot);
            }
                        
            if (!initialized)
            {
                initialized = true;
                for (int i = 0; i < 4; i++) 
                    GameScene.AddToScene(blocks[i],bounds[i]);
            }

            tick++;
            if (tick > 4294967294)
                tick = 0;
            base.OnEachFrame();
        }
        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {           
            if (ltick + 1 != tick)
                switch (key)
                {
                    case Keyboard.Key.Left:    
                        moveBound(-Config.blockSize, 0);                          
                        break;
                    case Keyboard.Key.Right:
                        moveBound(Config.blockSize, 0);                        
                        break;
                    case Keyboard.Key.Space:
                        rotateBound(1);
                        break;
                }
            if (key == Keyboard.Key.Down)
                if(!returned)
                    speed = Config.fastSpeed;
            ltick = tick;
        }

        public void returnBounds()
        {
            returned = true;
            foreach (var bound in bounds)
            {
                if (lrot == 2)
                    bound.Position = new Vector2f(
                        centers[num].X + centers[num].Y - bound.Position.Y - Config.blockSize,
                        bound.Position.X + centers[num].Y - centers[num].X);
                else if (lrot == 1)
                    bound.Position = new Vector2f(
                        bound.Position.Y + centers[num].X - centers[num].Y,
                        centers[num].X + centers[num].Y - bound.Position.X - Config.blockSize);

                bound.MoveIt(-ldx, -ldy);                
            }
            centers[num] += new Vector2f(-ldx, -ldy);
            lrot = 0;
            ldx = 0;
            ldy = 0;
        }
        private void rotateBound(int clockwise)
        {
            returned = false;
            foreach (var bound in bounds)
            {
                if(clockwise == 1)
                    bound.Position = new Vector2f(
                        centers[num].X + centers[num].Y - bound.Position.Y - Config.blockSize, 
                        bound.Position.X + centers[num].Y - centers[num].X);
                else if(clockwise == 2)
                    bound.Position = new Vector2f(
                        bound.Position.Y + centers[num].X - centers[num].Y,
                        centers[num].X + centers[num].Y - bound.Position.X - Config.blockSize);
            }
            lrot = clockwise;
            btick = tick;
        }
        private void rotateBlocks(int clockwise)
        {
            foreach (var block in blocks)
            {
                if (clockwise == 1)
                    block.Position = new Vector2f(
                        centers[num].X + centers[num].Y - block.Position.Y - Config.blockSize,
                        block.Position.X + centers[num].Y - centers[num].X);
                else if(clockwise == 2)
                    block.Position = new Vector2f(
                        block.Position.Y + centers[num].X - centers[num].Y,
                        centers[num].X + centers[num].Y - block.Position.X - Config.blockSize);
            }
            lrot = 0;
        }

        private void moveBound(float dx, float dy)
        {            
            returned = false;
            centers[num] += new Vector2f(dx, dy);
            foreach (var bound in bounds)
            {
                bound.MoveIt(dx, dy);
            }
            ldx += dx;
            ldy += dy;
            btick = tick;
            speed = Config.slowSpeed;
        }

        private void moveBlocks(float dx, float dy)
        {
            foreach (var block in blocks)
            {
                block.MoveIt(dx, dy);
            }
            ldx = 0;
            ldy = 0;
        }

        public static int getColors()
        {
            return colors.Count;
        }
    }
}
