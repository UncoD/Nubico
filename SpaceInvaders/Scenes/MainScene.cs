using System.Collections.Generic;
using Ungine;

namespace SpaceInvaders
{
    public class MainScene : GameScene
    {
        /// <summary>
        /// Главная сцена игры, поле боя с пришельцами.
        /// </summary>
        public MainScene() : base()
        {
            // Задаем начальный счет
            Game.Score = 0;
            
            // Создаем Доску (Переименовать в Platform)
            var player = new Player("Art/platform_000.png", Game.Width / 2f, Game.Height - 40f);
            // Размещаем игровой счет
            var scoreText = new ScoreText("0", 460, 20, 30);
            // Создаем фон
            var background = new Background("Art/background.png", Game.Width / 2f, Game.Height / 2f);
            // Создаем армию врагов
            var army = new Army(14);
            // Создаем индикатор снарядов Доски (переименовать в ChargesIndicator)
            var store = new Store(3, player);
            // Запускаем музыку
            MusicController.PlayMusic("Music/stage_2.ogg");
            MusicController.LoopMusic(true);

            // Добавляем созданные объекты на сцену
            AddToScene(background, player, scoreText, army, store);
           
            foreach (var invader in army.Invaders)
            {
                AddToScene(invader);
            }
            
            // Создаем энерегетические Щиты (переименовать в Shield) через равные интервалы
            for (var i = 1; i < 3 * 2; i += 2)
            {
                var item = new Bunker("Art/shelter_000.png", Game.Width * i / (3 * 2f), Game.Height - 150f);
                AddToScene(item);
            }
        }
    }
}
