using SFML.Window;

namespace Ungine
{
    public interface IOnKeyPressable
    {
        void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed);
    }
}