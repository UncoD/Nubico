using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace csharp_sfml_game_framework
{
    public class GameObject : Transformable, Drawable, IOnKeyPressable, IOnMouseClickable
    {
        public Game Game = GameProvider.ProvideDependency();
        public GameScene GameScene = CurrentSceneProvider.ProvideDependency();
        public int DrawPriority = 0;
        internal SpriteController SpriteController;
        public float X => Position.X;
        public float Y => Position.Y;
        public float Width => SpriteController.GetWidth();
        public float Height => SpriteController.GetHeight();
        public int Health { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        protected SoundController SoundController = new SoundController();
        protected MusicController MusicController;
        public bool IsBroken { get; internal set; }
        
        private readonly Vector2f defaultScale = new Vector2f(3, 3);

        public GameObject(float x, float y, string pathToTexture) : this (x, y)
        {
            SetSprite(pathToTexture);
        }

        public GameObject(float x, float y)
        {
            SpriteController = new SpriteController();
            MusicController = Game.MusicController;
            Scale = defaultScale;
            Position = new Vector2f(x, y);
        }

        public void DeleteFromGame()
        {
            BeforeDeleteFromScene();
            IsBroken = true;
            GameScene.DeleteFromScene(this);
        }

        internal virtual void UpdateObject()
        {
            OnEachFrame();
            OnHealthChanges(Health);

            SpriteController.UpdateAnimation();
            SpriteController.SynchronizeSprite(this);
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            SpriteController.TryDraw(target);

//            var board = new RectangleShape(new Vector2f(Width, Height))
//            {
//                OutlineColor = Color.Green,
//                FillColor = Color.Transparent,
//                OutlineThickness = 2,
//                Position = Position,
//                Origin = new Vector2f(Width / 2, Height / 2)
//            };
//
//            target.Draw(board);
        }
        
        public void MoveIt(float dx, float dy)
        {
            Position += new Vector2f(dx, dy);
        }

        public void AddAnimation(string animationName, int frequency, params string[] pathsToTextures)
        {
            SpriteController.AddAnimation(animationName, frequency, pathsToTextures);
        }

        public void PlayAnimation(string animationName)
        {
            SpriteController.PlayAnimation(animationName);
        }

        public void StopAnimation()
        {
            SpriteController.StopAnimation();
        }

        public void SetAnimationDelay(string animationName, int delay)
        {
            SpriteController.SetAnimationDelay(animationName, delay);
        }

        public void SetSprite(string path)
        {
            SpriteController.CreateSprite(path);
            Origin = SpriteController.CurrentSprite.Origin;
        }

        public void SetSpriteColor(Color color)
        {
            SpriteController.SetColor(color);
        }

        public void FlipX()
        {
            Scale = new Vector2f(-Scale.X, Scale.Y);
        }
        public void FlipY()
        {
            Scale = new Vector2f(Scale.X, -Scale.Y);
        }

        public virtual void OnEachFrame() { }
        public virtual void OnHealthChanges(int health) { }
        public virtual void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed) { }
        public virtual void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClick) { }
        public virtual void BeforeDeleteFromScene() { }

        public bool ClickedOnThis(Vector2i position)
        {
            return SpriteController.GetBounds().Contains(position.X, position.Y);
        }
    }
}