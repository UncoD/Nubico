using csharp_sfml_game_framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jRPG
{
    class Projectile : GameObject
    {

        private float xStart;
        private float yStart;
        private float xFinish;
        private float yFinish;

        int counter = 0;

        public Projectile(float xStart, float yStart, float xFinish, float yFinish, GameScene gameScene, string pathToTexture) : base(xStart, yStart, pathToTexture)
        {
            float dy = (float) new Random().NextDouble() * 100 - 50;
            this.xStart = xStart;
            this.yStart = yStart + dy;
            this.xFinish = xFinish;
            this.yFinish = yFinish + dy;
            this.GameScene = gameScene;
            Position = new SFML.System.Vector2f(this.xStart, this.yStart);
            DrawPriority = 10;
            float coef = 0.5f;
            Scale = new SFML.System.Vector2f(coef, coef);
            Scale = new SFML.System.Vector2f(coef, coef);
        }

        public override void OnEachFrame()
        {
            if (counter == 20) {
                DeleteFromGame();
            }
            Position += new SFML.System.Vector2f((xFinish - xStart) / 20, (yFinish - yStart) / 20);
            counter++;
            base.OnEachFrame();
        }
    }
}
