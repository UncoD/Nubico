using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace SpaceInvaders
{
    public class Store : GameObject
    {
        private List<GameObject> store;
        private Player owner;
        private int count;
        public Store(int count, Player player) : base(0, 0)
        {
            owner = player;
            this.count = count;
            store = new List<GameObject>();
                     
            for (var i = 1; i < count * 2; i += 2)
            {
                var item = new GameObject(120 * i / (count * 2f), 20, "Art/ball_000.png") {DrawPriority = 3};
                store.Add(item);
                GameScene.AddToScene(item);
            }
        }

        public override void OnEachFrame()
        {
            for (var i = 0; i < owner.Store; i++)
            {
                store[i].SetSprite("Art/ball_000.png");
            }

            for (var i = owner.Store; i < count; i++)
            {
                store[i].SetSprite("Art/ball_001.png");
            }
        }
    }
}