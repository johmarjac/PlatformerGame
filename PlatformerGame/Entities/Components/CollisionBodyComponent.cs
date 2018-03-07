using Microsoft.Xna.Framework;
using MonoGame.Additions;
using MonoGame.Additions.Entities;

namespace PlatformerGame.Entities.Components
{
    public class CollisionBodyComponent : EntityComponent, IRectangular
    {
        public Rectangle BoundingRectangle { get; set; }
        public bool IsStatic { get; set; }
    }
}