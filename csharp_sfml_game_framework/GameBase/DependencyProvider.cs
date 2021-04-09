using System.Collections.Generic;
using SFML.Graphics;

namespace csharp_sfml_game_framework
{
    internal class DependencyProvider<T>
    {
        private static T dependency;

        internal static void SetDependency(T dependencyObject)
        {
            dependency = dependencyObject;
        }

        internal static T ProvideDependency()
        {
            return dependency;
        }
    }
    internal class GameProvider : DependencyProvider<Game>
    {
    }

    internal class CurrentSceneProvider : DependencyProvider<GameScene>
    {
    }

    internal class TexturesProvider : DependencyProvider<Dictionary<string, Texture>>
    {
    }
}