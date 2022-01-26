using Nubico.Controllers;
using Nubico.GameBase;
using Nubico.Interfaces;
using Nubico.Objects.Physics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Nubico.Objects
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
        /// <summary>
        /// ���������� ��������
        /// </summary>
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
        /// ����������� �������� �������, ����������� ����������� �������������
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
                var border = new RectangleShape(new Vector2f(Width - 2, Height - 2))
                {
                    OutlineColor = Color.Green,
                    FillColor = Color.Transparent,
                    OutlineThickness = 1,
                    //Origin = new Vector2f(Math.Abs(Origin.X * Scale.X), Math.Abs(Origin.Y * Scale.Y)),
                    Origin = new Vector2f((Width - 2) / 2, (Height - 2) / 2),
                    Scale = new Vector2f(Math.Sign(Scale.X), Math.Sign(Scale.Y)),
                    Position = Position,
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
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            try
            {
                SpriteController.CreateSprite(path);
                Origin = SpriteController.CurrentSprite.Origin;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        /// <summary>
        /// ���������� ��� ������� ������� ����������
        /// </summary>
        /// <param name="pressedKey">������� �������</param>
        /// <param name="isAlreadyPressed">���� �� ������ ������� �� ���������� ����� (����������� ��������� �������)</param>
        public virtual void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed) { }

        /// <summary>
        /// <br>���������� ��� ����� ������� ����, ���� ���� �������</br>
        /// <br>��� �������� ������� �� ������ ������������ �������� HoverOnThis()</br>
        /// </summary>
        /// <param name="mouseButton">������� ������</param>
        /// <param name="position">������� ��������� � ������ �����</param>
        /// <param name="isAlreadyClicked">���� �� ������ ������ �� ���������� ����� (����������� �������)</param>
        public virtual void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked) { }
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