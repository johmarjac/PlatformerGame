using Microsoft.Xna.Framework;
using MonoGame.Additions;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Components;

namespace PlatformerGame.Entities.Components
{
    public sealed class PlayerComponent : EntityComponent, ITransform2D
    {
        public PlayerComponent()
        {
            WalkSpeed = 1f;
        }

        public void MoveBy(Vector2 delta)
        {
            Position += delta;
            UpdateSprite();
        }

        public void MoveTo(Vector2 newPos)
        {
            Position = newPos;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            var spriteComp = Entity.GetComponent<SpriteComponent>();
            spriteComp.Sprite.Position = Position;
        }

        public Vector2 Position { get; private set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public Vector2 Acceleration { get; set; }
        public Vector2 Velocity { get; set; }
        public float WalkSpeed { get; set; }
        public bool IsOnGround { get; set; }
    }
}