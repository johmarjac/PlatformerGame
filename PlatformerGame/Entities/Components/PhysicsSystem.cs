using Microsoft.Xna.Framework;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Attributes;
using MonoGame.Additions.Entities.Components;

namespace PlatformerGame.Entities.Components
{
    [ComponentSystem]
    [RequiredComponents(typeof(RigidbodyComponent), typeof(TransformComponent))]
    public class PhysicsSystem : ComponentSystem
    {
        public const float Gravity = 40f;

        public override void UpdateEntity(Entity entity, GameTime gameTime)
        {
            base.UpdateEntity(entity, gameTime);

            var body = entity.GetComponent<RigidbodyComponent>();
            var transform = entity.GetComponent<TransformComponent>();

            if(!body.IsStatic)
            {
                var delta = gameTime.ElapsedGameTime.Milliseconds / 100f;

                transform.Position += body.Velocity * delta;
                body.Velocity += new Vector2(0, Gravity) * delta;
            }
        }
    }
}