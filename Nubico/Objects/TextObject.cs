using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Nubico.Properties;

namespace Nubico.Objects
{
    /// <summary>
    /// ������������ ��������� ������� � ������� ����������
    /// </summary>
    public class TextObject : GameObject
    {
        /// <summary>
        /// ������ ������
        /// </summary>
        public int Size
        {
            get => (int) Text.CharacterSize;
            set {
                Text.CharacterSize = (uint)value;
                Text.Origin = new Vector2f(Width / 2, Height / 2);
                Origin = Text.Origin;
            }
        }

        /// <summary>
        /// ������ ������ ������ Text, ���������������� SFML
        /// </summary>
        protected readonly Text Text;
        /// <summary>
        /// ������ ���������� �������
        /// </summary>
        public new float Width => Text.GetGlobalBounds().Width;
        /// <summary>
        /// ������ ���������� �������
        /// </summary>
        public new float Height => Text.GetGlobalBounds().Height;

        /// <summary>
        /// ����������� ���������� ������� (���� ���������� �����)
        /// </summary>
        /// <param name="text">�������, ������������ ��������</param>
        /// <param name="x">�������������� ������� ������� (����� - �������� �������)</param>
        /// <param name="y">������������ ������� ������� (����� - �������� �������)</param>
        /// <param name="height">������ ������</param>
        /// <param name="pathToFont">���� � ����� ������ .ttf</param>
        public TextObject(string text, float x, float y, int height = 15, string pathToFont = "") : base(x, y)
        {
            Text = new Text
            {
                Font = string.IsNullOrEmpty(pathToFont) ? new Font(Resources.DefaultFont) : new Font(pathToFont),
                DisplayedString = text,
                Position = new Vector2f(x, y),
                CharacterSize = (uint) height
            };

            Text.Origin = new Vector2f(Width / 2, Height / 2);
            Origin = Text.Origin;
            Text.LineSpacing = 2;
        }

        /// <summary>
        /// <br>�������� ������� ��������� �������</br>
        /// <br>�������� ���������� �������� ������ ���� ������������ ���� ����������</br>
        /// </summary>
        /// <returns>�������������, ������������ ������� ��������� �������</returns>
        public FloatRect GetBounds()
        {
            return Text.GetGlobalBounds();
        }

        /// <summary>
        /// ������ �����, ������������ ��������
        /// </summary>
        /// <param name="text">����� ������, �������������� ����������� � ������</param>
        public void SetText(object text)
        {
            Text.DisplayedString = Convert.ToString(text);
        }

        /// <summary>
        /// ������ ���� ������
        /// </summary>
        /// <param name="color">����, ��������������� ������</param>
        public void SetColor(Color color)
        {
            Text.FillColor = color;
        }

        /// <summary>
        /// �������� ������� ����� � �������
        /// </summary>
        /// <returns>�����, ������������ ��������</returns>
        public override string ToString()
        {
            return Text.ToString();
        }
        
        /// <summary>
        /// ���������� �� ������ �����, �������� �������� ������ ���������� ������� �� ����� ������ ����������
        /// </summary>
        public override void OnEachFrame() { }

        internal override void UpdateObject()
        {
            Text.Position = Position;
            Text.Origin = Origin;

            OnEachFrame();
        }

        /// <summary>
        /// <br>���������� ������� ������ � ���� ����������</br>
        /// <br>���� Game.DrawObjectBorders = true, ���������� ������� �������</br>
        /// </summary>
        /// <param name="target">���� ��� ����������� (���� ����������)</param>
        /// <param name="states">��������� ������������� ��� �����������</param>
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(Text);

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

        /// <summary>
        /// ��������, ��� ��������� ������� ��������� ��� ��������
        /// </summary>
        /// <param name="position">������� ����� ��� ��������</param>
        /// <returns>��������� �� ����� ��� ��������</returns>
        public new bool HoverOnThis(Vector2i position)
        {
            return GetBounds().Contains(position.X, position.Y);
        }

        /// <summary>
        /// ��������, ��� ������ ������� �� ������ ������
        /// </summary>
        /// <returns>������� �� ������ �� ������</returns>
        public new bool HoverOnThis()
        {
            return HoverOnThis(Mouse.GetPosition(Game.Window));
        }
    }
}