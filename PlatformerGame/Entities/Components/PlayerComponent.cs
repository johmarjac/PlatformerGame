using MonoGame.Additions.Entities;

namespace PlatformerGame.Entities.Components
{
    public sealed class PlayerComponent : EntityComponent
    {
        public PlayerComponent()
        {
            WalkSpeed = 1f;
        }
        
        public float WalkSpeed { get; set; }
    }
}