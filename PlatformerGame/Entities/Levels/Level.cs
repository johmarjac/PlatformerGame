using MonoGame.Extended.Tiled;

namespace PlatformerGame.Entities.Levels
{
    public class Level
    {
        public Level(TiledMap map)
        {
            Map = map;
        }

        public readonly TiledMap Map;
    }
}