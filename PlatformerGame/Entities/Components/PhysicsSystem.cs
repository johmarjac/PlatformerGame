using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Attributes;

namespace PlatformerGame.Entities.Components
{
    [ComponentSystem]
    [RequiredComponents(typeof(CollisionBodyComponent))]
    public class PhysicsSystem : ComponentSystem
    {
    }
}