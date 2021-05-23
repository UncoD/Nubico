using Ungine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jRPG
{
    class FinalScene : GameScene
    {
        public FinalScene() 
        {
            MusicController.PlayMusic("Music/allods frost patterns.ogg");
            Background background = new Background(Game.Width / 2, Game.Height / 2, "Art/final.png");
            AddToScene(background);
        }
    }
}
