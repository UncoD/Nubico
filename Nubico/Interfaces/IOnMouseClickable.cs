using SFML.System;
using SFML.Window;

namespace Nubico.Interfaces
{
    /// <summary>
    /// Используется для обозначения объектов (и сцен), которые могут реагировать на ввод с мыши
    /// </summary>
    public interface IOnMouseClickable
    {
        /// <summary>
        /// <br>Вызывается при любом нажатии мыши, даже мимо обеъкта</br>
        /// <br>Для проверки нажатия на объект использовать успловие HoverOnThis()</br>
        /// </summary>
        /// <param name="mouseButton">Нажатая кнопка</param>
        /// <param name="position">Позиция указателя в момент клика</param>
        /// <param name="isAlreadyClicked">Была ли нажата кнопка на предыдущем кадре (определение зажатия)</param>
        void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked);
    }
}