using System;
using System.Collections.Generic;
using csharp_sfml_game_framework;
using SFML.System;

namespace FlappyFish
{
    public class MainScene : GameScene
    {
        Random rnd;

        public MainScene()
        {
            Game.Score = 0;
            
            var player = new Player("Art/hero.png", 50, Game.Height / 2, "Player");
            var scoreText = new ScoreText("", 460, 20);
            var background = new Background("Art/ocean.jpg", Game.Width / 2f, Game.Height / 2f);
            var walls = new List<Wall>();
            rnd = new Random();
            var gwall = new Wall("Art/anchor.png", -1, -1, false);
            for (int i = 0; i < Game.Width / 500; ++i)
            {
                var newWall = gwall.generateRandomWall();
                newWall.Position = new Vector2f(500*(i + 1), newWall.Y);
                walls.Add(newWall);
            }
            AddToScene(background, player, scoreText);
            foreach (var wall in walls)
            {
                AddToScene(wall);
            }
        }

        public override void OnEachFrame()
        {
            if (CountSharks() == 0 && CanAddRareObject())
            {
                AddRandomShark();
            }
            if (CountMissiles() == 0 && CanAddRareObject())
            {
                AddRandomMissile();
            }
            if (rnd.Next(500) == 0)
            {
                AddRandomBonus();
            }
            base.OnEachFrame();
        }

        private void AddRandomShark()
        {
            var width = 1000;
            var height = 600;
            var shark = new Shark("Art/shark.png", width, rnd.Next(50, height - 50));
            AddToScene(shark);
        }

        private void AddRandomMissile()
        {
            var width = 1000;
            var height = 600;
            var textures = new string[] { "Art/alienmissile.png",
                "Art/humanmissile.png", "Art/testmissile.png"};

            var missile = new Missile(textures[rnd.Next(3)], width, rnd.Next(50, height - 50));
            AddToScene(missile);
        }

        private void AddRandomBonus()
        {
            var width = 1000;
            var height = 600;
            Bonus bonus;
            bool isIntersects = false;
            do {
                bonus = new Bonus("Art/goldCoin1.png", rnd.Next(width), rnd.Next(height));
                foreach (var obj in this.GameObjects)
                {
                    var someGameObject = obj as PhysicsObject;
                    if (someGameObject != null && bonus.IsIntersects(someGameObject))
                    {
                        isIntersects = true;
                    }
                }
            } while (isIntersects);

            AddToScene(bonus);
        }

        private bool CanAddRareObject()
        {
            return rnd.Next(2000) == 0;
        }

        private int CountSharks()
        {
            var count = 0;
            foreach (var obj in  GameObjects)
            {
                if (obj is Shark)
                {
                    ++count;
                }
            }
            return count;
        }

        private int CountMissiles()
        {
            var count = 0;
            foreach (var obj in GameObjects)
            {
                if (obj is Missile)
                {
                    ++count;
                }
            }
            return count;
        }

        private int CountBonuses()
        {
            var count = 0;
            foreach (var obj in GameObjects)
            {
                if (obj is Bonus)
                {
                    ++count;
                }
            }
            return count;
        }
    }
}