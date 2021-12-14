using SFML.Window;

namespace Nubico.Interfaces
{
    /// <summary>
    /// Используется для обозначения объектов (и сцен), которые могут реагировать на ввод с клавиатуры
    /// </summary>
    internal interface IOnKeyPressable
    {
        /// <summary>
        /// Вызывается при нажатии клавиши клавиатуры
        /// </summary>
        /// <param name="pressedKey">Нажатая клавиша</param>
        /// <param name="isAlreadyPressed">Была ли нажата клавиша на предыдущем кадре (определение зажатия)</param>
        void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed);
    }
}