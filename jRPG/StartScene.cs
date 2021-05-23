using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ungine;
using SFML.Window;

namespace jRPG
{
    class StartScene : GameScene
    {
        public StartScene() 
        {
            MusicController.PlayMusic("Music/allods tears.ogg");
            Background background = new Background(Game.Width / 2, Game.Height / 2, "Art/start.png");
            AddToScene(background);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isKeyPressed) {
            if (key == Keyboard.Key.Space) {
                MusicController.StopMusic();
                MapScene mapScene = new MapScene();
                Game.SetCurrentScene(mapScene);
            }
        }
    }
}
