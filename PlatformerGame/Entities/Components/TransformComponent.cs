using Microsoft.Xna.Framework;
using MonoGame.Additions;
using MonoGame.Additions.Entities;

namespace PlatformerGame.Entities.Components
{
    public class TransformComponent : EntityComponent, ITransform2D
    {
        public TransformComponent()
        {
            Scale = 1f;
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
    }
}