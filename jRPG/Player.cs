using csharp_sfml_game_framework;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jRPG
{
    class Player : GameObject
    {
        private readonly List<string> playerSprites;
        private int currentSprite = 0;
        private int currentDirection = 0;

        private int mapX = 0;
        private int mapY = 0;

        private int counter;

        private long lastKeyPressTime = 0;

        private readonly MapScene mapScene;

        private int level = 0;

        private Random r = new Random();

        public Player(MapScene mapScene) : base(0, 0)
        {
            this.mapScene = mapScene;

            DrawPriority = 2;
            playerSprites = new List<string> {
                "Art/player_00.png",
                "Art/player_01.png",
                "Art/player_02.png",
                "Art/player_03.png",
                "Art/player_04.png",
                "Art/player_05.png",
                "Art/player_06.png",
                "Art/player_07.png",
                "Art/player_08.png",
                "Art/player_09.png",
                "Art/player_10.png",
                "Art/player_11.png",
                "Art/player_12.png",
                "Art/player_13.png",
                "Art/player_14.png",
                "Art/player_15.png",
            };

            Scale = new SFML.System.Vector2f(1f, 1f);
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (mapScene.isBattle) {
                return;
            }

            long currentKeyPressTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentKeyPressTime < lastKeyPressTime + 100) {
                return;
            }
            lastKeyPressTime = currentKeyPressTime;

            if (pressedKey == Keyboard.Key.W) {
                currentDirection = 3;
                TryMove(0, -1);
            } else if (pressedKey == Keyboard.Key.S) {
                currentDirection = 0;
                TryMove(0, 1);
            } else if (pressedKey == Keyboard.Key.D) {
                currentDirection = 2;
                TryMove(1, 0);
            } else if (pressedKey == Keyboard.Key.A) {
                currentDirection = 1;
                TryMove(-1, 0);
            }
        }

        public override void OnEachFrame()
        {
            SetSprite(playerSprites[currentDirection * 4 + currentSprite]);
            Position = new SFML.System.Vector2f(mapX * 70 + 35, mapY * 70 + 35);
            Scale = new SFML.System.Vector2f(1f, 1f);

            counter++;
            if (counter % 7 == 0) {
                currentSprite++;
            }
            if (currentSprite == 4) {
                currentSprite = 0;
            }
            base.OnEachFrame();
        }

        public void LevelUp() {
            level++;
        }

        private void TryMove(int dx, int dy) {
            bool success = true;
            if (mapX + dx >= 0 && mapX + dx < 17) {
                mapX += dx;
            } else {
                success = false;
            }
            if (mapY + dy >= 0 && mapY + dy < 6) {
                mapY += dy;
            } else {
                success = false;
            }
            if (success) {
                if (mapX == 15 && mapY == 4)
                {
                    mapScene.StartBattle(level, 3);
                }
                else if (mapX == 11 && mapY == 1) {
                    mapScene.StartBattle(level, 2);
                } else {
                    double p = r.NextDouble();
                    if (p < 1.0 / 7.0)
                    {
                        mapScene.StartBattle(level, r.Next(2));
                    }
                }
            }
        }
    }
}
