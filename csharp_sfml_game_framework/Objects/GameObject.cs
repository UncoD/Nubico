using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Ungine
{
    /// <summary>
    /// ����� �������� �������� �������, �������� �� ����������� � ���������, ��������� �� ���� � ���������� � ����
    /// </summary>
    public class GameObject : Transformable, Drawable, IOnKeyPressable, IOnMouseClickable
    {
        /// <summary>
        /// ������ �� ����, � ������� ��������� ������
        /// </summary>
        public readonly Game Game = GameProvider.ProvideDependency();
        /// <summary>
        /// ������ �� �����, �� ������� ��������� ������
        /// </summary>
        public GameScene GameScene => CurrentSceneProvider.ProvideDependency();
        /// <summary>
        /// ��������� ��������� ������� (��� ������, ��� ������ ������������ �������, ��� "������" �� �� ������)
        /// </summary>
        public int DrawPriority = 0;
        internal SpriteController SpriteController;
        /// <summary>
        /// ������� ������� �� �����������
        /// </summary>
        public float X => Position.X;
        /// <summary>
        /// ������� ������� �� ���������
        /// </summary>
        public float Y => Position.Y;
        /// <summary>
        /// ������ �������
        /// </summary>
        public float Width => SpriteController.GetWidth();
        /// <summary>
        /// ������ �������
        /// </summary>
        public float Height => SpriteController.GetHeight();
        /// <summary>
        /// ������ �������� �����������
        /// </summary>
        public Vector2f Velocity;
        /// <summary>
        /// ���������� ������
        /// </summary>
        protected SoundController SoundController = new SoundController();
        /// <summary>
        /// ���������� ������ - ����� ��� ���� ����
        /// </summary>
        protected MusicController MusicController;

        /// <summary>
        /// ������ �� ������ �� �����
        /// </summary>
        public bool IsBroken { get; internal set; }

        /// <summary>
        /// ������������ �������� �������, ����������� ����������� �������������
        /// </summary>
        /// <param name="x">�������������� �������</param>
        /// <param name="y">������������ �������</param>
        /// <param name="pathToTexture">���� � ����� ����������� (.png, .jpg)</param>
        public GameObject(float x, float y, string pathToTexture) : this (x, y)
        {
            SetSprite(pathToTexture);
        }

        /// <summary>
        /// ����������� ������� ��� ���������� ������������ �������������
        /// </summary>
        /// <param name="x">�������������� �������</param>
        /// <param name="y">������������ �������</param>
        public GameObject(float x, float y)
        {
            SpriteController = new SpriteController();
            MusicController = Game.MusicController;
            Position = new Vector2f(x, y);

            // TODO: getter setter for Scale (change Origin).
        }

        /// <summary>
        /// ������� ������ �� �����
        /// </summary>
        public void DeleteFromGame()
        {
            BeforeDeleteFromScene();
            IsBroken = true;
            GameScene.DeleteFromScene(this);
        }

        internal virtual void UpdateObject()
        {
            Position += Velocity;

            OnEachFrame();

            SpriteController.UpdateAnimation();
            SpriteController.SynchronizeSprite(this);
        }

        /// <summary>
        /// <br>���������� ������� ������ � ���� ����������</br>
        /// <br>���� Game.DrawObjectBorders = true, ���������� ������� �������</br>
        /// </summary>
        /// <param name="target">���� ��������� (���� ����������)</param>
        /// <param name="states">��������� ������������� ��� ������������</param>
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
                    Origin = new Vector2f(Width / 2, Height / 2),
                    Position = Position
                };

                target.Draw(border);
            }
        }

        /// <summary>
        /// �������� �������� �������
        /// </summary>
        /// <param name="animationName">�������� ��������</param>
        /// <param name="delay">�������� ����� ������� �������� � ��������</param>
        /// <param name="pathsToTextures">���� � ������ ������ (.png, .jpg) �������� ����� �������</param>
        public void AddAnimation(string animationName, float delay, params string[] pathsToTextures)
        {
            SpriteController.AddAnimation(animationName, delay, pathsToTextures);
        }

        /// <summary>
        /// ������� �������� �� ��������
        /// </summary>
        /// <param name="animationName">�������� ��������� ��������</param>
        public void RemoveAnimation(string animationName)
        {
            SpriteController.RemoveAnimation(animationName);
        }

        /// <summary>
        /// ��������� �������� �� �����, ���� ��� ���� ��������� � ������
        /// </summary>
        /// <param name="animationName">�������� ����������� ��������</param>
        /// <param name="restart">������������� �������� � ������� �����</param>
        public void PlayAnimation(string animationName, bool restart = false)
        {
            SpriteController.PlayAnimation(animationName, restart, this);
        }

        /// <summary>
        /// ���������� ������������ ��������
        /// </summary>
        /// <param name="withRestart">�������� �������� � ������� �����</param>
        public void StopAnimation(bool withRestart = false)
        {
            SpriteController.StopAnimation(withRestart);
        }

        /// <summary>
        /// ���������� �������� ������� ������������� ��������
        /// </summary>
        /// <returns>�������� ������������� ��������</returns>
        public string CurrentAnimationName => SpriteController.CurrentAnimationName();

        /// <summary>
        /// ���������� ������� ���������� ������ ��� ���������� �������
        /// </summary>
        /// <param name="animationName">�������� ����������� ��������</param>
        /// <param name="delay">�������� ����� ������� � ��������</param>
        public void SetAnimationDelay(string animationName, float delay)
        {
            SpriteController.SetAnimationDelay(animationName, delay);
        }

        /// <summary>
        /// ���������� ����������� ����������� ��� ������� (������)
        /// </summary>
        /// <param name="path">���� � ����� ����������� (.png, .jpg)</param>
        public void SetSprite(string path)
        {
            SpriteController.CreateSprite(path);
            Origin = SpriteController.CurrentSprite.Origin;
        }

        /// <summary>
        /// ���������� ���� ������ �������
        /// </summary>
        /// <param name="color">��������������� ����</param>
        public void SetSpriteColor(Color color)
        {
            SpriteController.SetColor(color);
        }

        /// <summary>
        /// �������� ������ �� �����������
        /// </summary>
        public void FlipX()
        {
            Scale = new Vector2f(-Scale.X, Scale.Y);
        }
        /// <summary>
        /// �������� ������ �� ���������
        /// </summary>
        public void FlipY()
        {
            Scale = new Vector2f(Scale.X, -Scale.Y);
        }

        /// <summary>
        /// ���������� �� ������ �����, �������� �������� ������ ���������� ������� �� ����� ������ ����������
        /// </summary>
        public virtual void OnEachFrame() { }
        public virtual void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed) { }
        public virtual void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked) { }
        /// <summary>
        /// ���������� ����� ��������� ������� �� �����
        /// </summary>
        public virtual void BeforeDeleteFromScene() { }

        /// <summary>
        /// ��������, ��� ��������� ������� ��������� ��� ��������
        /// </summary>
        /// <param name="position">������� ����� ��� ��������</param>
        /// <returns>��������� �� ����� ��� ��������</returns>
        public bool HoverOnThis(Vector2i position)
        {
            return SpriteController.GetBounds().Contains(position.X, position.Y);
        }

        /// <summary>
        /// ��������, ��� ������ ������� �� ������ ������
        /// </summary>
        /// <returns>������� �� ������ �� ������</returns>
        public bool HoverOnThis()
        {
            return HoverOnThis(Mouse.GetPosition(Game.Window));
        }
    }
}