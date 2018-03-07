using Microsoft.Xna.Framework.Input;
using MonoGame.Additions.Entities;

namespace PlatformerGame.Entities.Components
{
    public class PlayerController : EntityComponent
    {
        public Keys MoveRightKey = Keys.D;
        public Keys MoveLeftKey = Keys.A;
        public Keys JumpKey = Keys.Space;
    }
}