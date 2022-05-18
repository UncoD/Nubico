using Nubico.GameBase;
using Nubico.Objects;

namespace ExampleTopDownGame
{
    public class MyScene : GameScene
    {
        private Player player;

        public MyScene()
        {
            CreateMap();
            player = new Player(48, 48);
            var camera = new Camera(player, 0.3f);
            AddToScene(player, camera);
        }

        private void CreateMap()
        {
            var lines = File.ReadAllLines("Art/map.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char cell = line[j];
                    AddToScene(cell == '0' ? new Ground(48 * j, 48 * i) : new Wall(48 * j, 48 * i));
                }
            }
        }
    }
}