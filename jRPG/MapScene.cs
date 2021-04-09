using csharp_sfml_game_framework;
using SFML.Window;
using System;

namespace jRPG
{
    class MapScene : GameScene
    {
        public Player player;
        private Background background;

        public bool isBattle = false;
        public Battle battle;
        public BattlePlayer battlePlayer;
        public BattleMob battleMob;

        TextObject playerStats = new TextObject("", 10, 10, 15, "Font/allods.ttf");
        TextObject mobStats = new TextObject("", 1000, 10,15,  "Font/allods.ttf");

        TextObject win;
        TextObject lose;
        long lastBattleEndTime = 0;
        bool wasBattle = false;

        public MapScene() 
        {
            background = new Background(Game.Width / 2, Game.Height / 2, "Art/map.png");
            player = new Player(this);
            AddToScene(background, player);
            
            playerStats.DrawPriority = 7;
            playerStats.Size = 24;
            playerStats.SetColor(SFML.Graphics.Color.Black);
            
            mobStats.DrawPriority = 7;
            mobStats.Size = 24;
            playerStats.SetColor(SFML.Graphics.Color.Black);
            
            battle = new Battle(Game.Width / 2, Game.Height / 2);

            win = new TextObject("You win!\nLevel Up!\nHarder! Better! Faster! Stronger!", Game.Width / 2 - 500, Game.Height / 2, 15, "Font/allods.ttf");
            win.DrawPriority = 9;

            lose = new TextObject("You Lose!\nTry Harder!", Game.Width / 2 - 500, Game.Height / 2, 15, "Font/allods.ttf");
            lose.DrawPriority = 9;

            MusicController.PlayMusic("Music/fade - Inner Peace.ogg");
            MusicController.LoopMusic(true);
        }

        public void StartBattle(int level, int mobNumber) {
            isBattle = true;
            AddToScene(battle);
            battlePlayer = new BattlePlayer(Game.Width / 2 - 255, Game.Height / 2, level);
            if (mobNumber == 0) {
                battleMob = new BattleMob(Game.Width / 2 + 225, Game.Height / 2, 100, 12, 1, "Art/mob.png");
            } else if (mobNumber == 1) {
                battleMob = new BattleMob(Game.Width / 2 + 205, Game.Height / 2 + 50, 100, 12, 1, "Art/mob_2.png");
            } else if (mobNumber == 2) {
                battleMob = new BattleMob(Game.Width / 2 + 225, Game.Height / 2, 1000, 1, 48, "Art/boss_2.png");
            } else if (mobNumber == 3) {
                battleMob = new BattleMob(Game.Width / 2 + 225, Game.Height / 2, 10000, 200, 1, "Art/boss_1.png", true);
            }
            battleMob.SetOpponent(battlePlayer);
            battlePlayer.SetOpponent(battleMob);
            battleMob.SetMapScene(this);
            battlePlayer.SetMapScene(this);
            MusicController.StopMusic();
            MusicController.PlayMusic("Music/PMMM symposium magarum.ogg");
            AddToScene(battlePlayer, battleMob);
            AddToScene(playerStats, mobStats);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isKeyPressed)
        {
            if (key == Keyboard.Key.Escape) {
                Environment.Exit(0);
            }
        }

        public override void OnEachFrame() {
            if (wasBattle) {
                if (lastBattleEndTime + 1000 < DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) {
                    lose.DeleteFromGame();
                    win.DeleteFromGame();
                    wasBattle = false;
                }
            }
            if (isBattle) {
                bool battleOver = false;
                if (battlePlayer.IsKilled()) {
                    battleOver = true;
                    AddToScene(lose);
                }
                if (battleMob.IsKilled()) {
                    battleOver = true;
                    if (battleMob.isFinalBoss()) {
                        MusicController.StopMusic();
                        FinalScene finalScene = new FinalScene();
                        Game.SetCurrentScene(finalScene);
                        return;
                    }
                    player.LevelUp();
                    AddToScene(win);
                }
                if (battleOver) {
                    MusicController.StopMusic();
                    MusicController.PlayMusic("Music/fade - Inner Peace.ogg");
                    DeleteBattleSprites();
                    isBattle = false;
                    wasBattle = true;
                    lastBattleEndTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                } else {
                    playerStats.SetText(battlePlayer.GetStatsString());
                    mobStats.SetText(battleMob.GetStatsString());
                }
            }
        }

        private void DeleteBattleSprites() {
            battlePlayer.DeleteFromGame();
            battleMob.DeleteFromGame();
            battle.DeleteFromGame();
            playerStats.DeleteFromGame();
            mobStats.DeleteFromGame();
            battlePlayer.DeleteEffectsFromScene();
            battleMob.DeleteEffectsFromScene();
        }
    }
}
