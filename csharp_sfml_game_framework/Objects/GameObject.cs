using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Ungine
{
    public class GameObject : Transformable, Drawable, IOnKeyPressable, IOnMouseClickable
    {
        public readonly Game Game = GameProvider.ProvideDependency();
        public readonly GameScene GameScene = CurrentSceneProvider.ProvideDependency();
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

            if (Game.DrawObjectBorders)
            {
                var border = new RectangleShape(new Vector2f(Width, Height))
                {
                    OutlineColor = Color.Green,
                    FillColor = Color.Transparent,
                    OutlineThickness = 2,
                    Position = Position,
                    Origin = new Vector2f(Width / 2, Height / 2)
                };

                target.Draw(border);
            }

        }
        
        public void MoveIt(float dx, float dy)
        {
            Position += new Vector2f(dx, dy);
        }

        public void AddAnimation(string animationName, float frequency, params string[] pathsToTextures)
        {
            SpriteController.AddAnimation(animationName, frequency, pathsToTextures);
        }

        public void PlayAnimation(string animationName, bool restart = false)
        {
            SpriteController.PlayAnimation(animationName, restart);
        }

        public void StopAnimation(bool withRestart = false)
        {
            SpriteController.StopAnimation(withRestart);
        }

        public string CurrentAnimationName()
        {
            return SpriteController.CurrentAnimationName();
        }

        public void SetAnimationDelay(string animationName, float delay)
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
        public virtual void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked) { }
        public virtual void BeforeDeleteFromScene() { }

        public bool HoverOnThis(Vector2i position)
        {
            return SpriteController.GetBounds().Contains(position.X, position.Y);
        }

        public bool HoverOnThis()
        {
            return HoverOnThis(Mouse.GetPosition(Game.Window));
        }
    }
}