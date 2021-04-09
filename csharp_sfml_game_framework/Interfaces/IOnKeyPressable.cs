using SFML.Window;

namespace csharp_sfml_game_framework
{
    public interface IOnKeyPressable
    {
        void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed);
    }
}