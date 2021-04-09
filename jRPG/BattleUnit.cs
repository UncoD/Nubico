using csharp_sfml_game_framework;
using System;

namespace jRPG
{
    class BattleUnit : GameObject
    {
        public int health;
        public int attack;
        int speed;

        public double energy;

        bool blocked = false;

        protected BattleUnit opponent;

        protected MapScene mapScene;

        public GameObject blocking;
        public GameObject attacked;
        public GameObject healing;

        public BattleUnit(float x, float y, int health, int attack, int speed) : base(x, y)
        {
            this.attack = attack;
            this.health = health;
            this.speed = speed;

            attacked = new GameObject(x, y, "Art/attacked.png");
            attacked.Scale = new SFML.System.Vector2f(1, 1);
            attacked.Scale = new SFML.System.Vector2f(1, 1);
            attacked.DrawPriority = 8;


            blocking = new GameObject(x, y, "Art/blocking.png");
            blocking.Scale = new SFML.System.Vector2f(1, 1);
            blocking.Scale = new SFML.System.Vector2f(1, 1);
            blocking.DrawPriority = 8;

            healing = new GameObject(x, y, "Art/healing.png");
            healing.Scale = new SFML.System.Vector2f(1, 1);
            healing.Scale = new SFML.System.Vector2f(1, 1);
            healing.DrawPriority = 8;
        }

        public void SetMapScene(MapScene mapScene) {
            this.mapScene = mapScene;
        }

        public void SetOpponent(BattleUnit opponent) {
            this.opponent = opponent;
        }

        public override void OnEachFrame()
        {
            energy += speed;
        }

        public bool IsReady() {
            return energy >= 100.0;
        }
        
        public virtual void Heal() {
            health += attack / 2;
            energy -= 100;
            DeleteEffectsFromScene();
            mapScene.AddToScene(healing);
            blocked = false;
        }

        public void TakeDamage(int damage) {
            if (blocked) {
                damage = 0;
            }
            health -= damage;
            DeleteEffectsFromScene();
            mapScene.AddToScene(attacked);
            blocked = false;
        }

        public virtual void Block() {
            energy -= 100;
            DeleteEffectsFromScene();
            mapScene.AddToScene(blocking);
            blocked = true;
        }

        public virtual void Attack() {
            opponent.TakeDamage(attack);
            energy -= 100;
            DeleteEffectsFromScene();
            blocked = false;
        }

        public bool IsKilled() {
            return health <= 0;
        }

        public string GetStatsString() {
            return "health: " + health + "\nattack: " + attack + "\nenergy: " + energy;
        }

        public void DeleteEffectsFromScene()
        {
            blocking.DeleteFromGame();
            healing.DeleteFromGame();
            attacked.DeleteFromGame();
        }

        public override void BeforeDeleteFromScene()
        {
            attacked.DrawPriority = -1;
            blocking.DrawPriority = -1;
            healing.DrawPriority = -1;
            DeleteEffectsFromScene();

            base.BeforeDeleteFromScene();
        }
    }
}
