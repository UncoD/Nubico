using SFML.System;
using SFML.Window;

namespace Ungine
{
    public interface IOnMouseClickable
    {
        /// <summary>
        /// Вызывается при любом нажатии мыши, даже мимо обеъкта
        /// Для проверки нажатия на объект использовать успловие HoverOnThis()
        /// </summary>
        /// <param name="mouseButton"></param>
        /// <param name="position"></param>
        /// <param name="IsAlreadyClicked"></param>
        void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked);
    }
}