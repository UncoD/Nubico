using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using Nubico.Properties;

namespace Nubico.Objects
{
    /// <summary>
    /// Представляет текстовую надпись в игровом приложении
    /// </summary>
    public class TextObject : GameObject
    {
        /// <summary>
        /// Размер шрифта
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
        /// Объект текста класса Text, предоставляемого SFML
        /// </summary>
        protected readonly Text Text;
        /// <summary>
        /// Ширина текстового объекта
        /// </summary>
        public new float Width => Text.GetGlobalBounds().Width;
        /// <summary>
        /// Высота текстового объекта
        /// </summary>
        public new float Height => Text.GetGlobalBounds().Height;

        /// <summary>
        /// Конструктор текстового объекта (есть встроенный шрифт)
        /// </summary>
        /// <param name="text">Надпись, отображаемая объектом</param>
        /// <param name="x">Горизонтальная позиция надписи (центр - середина объекта)</param>
        /// <param name="y">Вертикальная позиция надписи (центр - середина объекта)</param>
        /// <param name="height">Размер шрифта</param>
        /// <param name="pathToFont">Путь к файлу шрифта .ttf</param>
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
        /// <br>Получить границы текстовой надписи</br>
        /// <br>Содержит координаты верхнего левого угла относительно окна приложения</br>
        /// </summary>
        /// <returns>Прямоугольник, обозначающий границы текстовой надписи</returns>
        public FloatRect GetBounds()
        {
            return Text.GetGlobalBounds();
        }

        /// <summary>
        /// Задать текст, отображаемый объектом
        /// </summary>
        /// <param name="text">Любой объект, поддерживающий конвертацию в строку</param>
        public void SetText(object text)
        {
            Text.DisplayedString = Convert.ToString(text);
        }

        /// <summary>
        /// Задать цвет текста
        /// </summary>
        /// <param name="color">Цвет, устанавливаемый тексту</param>
        public void SetColor(Color color)
        {
            Text.FillColor = color;
        }

        /// <summary>
        /// Получить текущий текст в объекта
        /// </summary>
        /// <returns>Текст, отображаемый объектом</returns>
        public override string ToString()
        {
            return Text.ToString();
        }
        
        /// <summary>
        /// Вызывается на каждом кадре, содержит описание логики обновления объекта во время работы приложения
        /// </summary>
        public override void OnEachFrame() { }

        internal override void UpdateObject()
        {
            Text.Position = Position;
            Text.Origin = Origin;

            OnEachFrame();
        }

        /// <summary>
        /// <br>Отображает игровой объект в окне приложения</br>
        /// <br>Если Game.DrawObjectBorders = true, отображает границы объекта</br>
        /// </summary>
        /// <param name="target">Цель отрисовки (окно приложения)</param>
        /// <param name="states">Параметры трансформации при отображениии</param>
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
        /// Проверка, что указанная позиция находится над объектом
        /// </summary>
        /// <param name="position">Позиция точки для проверки</param>
        /// <returns>Находится ли точка над объектом</returns>
        public new bool HoverOnThis(Vector2i position)
        {
            return GetBounds().Contains(position.X, position.Y);
        }

        /// <summary>
        /// Проверка, что курсор наведен на данный объект
        /// </summary>
        /// <returns>Наведен ли курсор на объект</returns>
        public new bool HoverOnThis()
        {
            return HoverOnThis(Mouse.GetPosition(Game.Window));
        }
    }
}