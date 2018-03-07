using MonoGame.Additions.Entities;
using MonoGame.Extended.Tiled.Graphics;

namespace PlatformerGame.Entities.Components
{
    public class TiledMapRendererComponent : EntityComponent
    {
        public TiledMapRenderer Renderer { get; set; }
    }
}