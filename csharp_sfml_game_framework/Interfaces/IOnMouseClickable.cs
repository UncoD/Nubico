using SFML.System;
using SFML.Window;

namespace csharp_sfml_game_framework
{
    public interface IOnMouseClickable
    {
        void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClick);
    }
}