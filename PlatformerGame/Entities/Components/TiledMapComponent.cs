using MonoGame.Additions.Entities;
using MonoGame.Extended.Tiled;

namespace PlatformerGame.Entities.Components
{
    public class TiledMapComponent : EntityComponent
    {
        public TiledMap Map { get; set; }
    }
}