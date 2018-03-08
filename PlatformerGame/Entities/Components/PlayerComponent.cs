using MonoGame.Additions.Entities;

namespace PlatformerGame.Entities.Components
{
    public sealed class PlayerComponent : EntityComponent
    {
        public PlayerComponent()
        {
            MaxWalkSpeed = 50f;
        }
        
        public float MaxWalkSpeed { get; set; }
        public bool IsOnGround { get; set; }
    }
}