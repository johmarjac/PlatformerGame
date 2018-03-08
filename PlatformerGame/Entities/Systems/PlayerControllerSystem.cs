using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Attributes;
using MonoGame.Additions.Entities.Components;
using PlatformerGame.Entities.Components;

namespace PlatformerGame.Entities.Systems
{
    [ComponentSystem]
    [RequiredComponents(typeof(PlayerController), typeof(PlayerComponent), typeof(RigidbodyComponent))]
    public class PlayerControllerSystem : ComponentSystem
    {        
        public override void UpdateEntity(Entity entity, GameTime gameTime)
        {
            base.UpdateEntity(entity, gameTime);

            var controller = entity.GetComponent<PlayerController>();
            var player = entity.GetComponent<PlayerComponent>();
            var body = entity.GetComponent<RigidbodyComponent>();
            var sprite = entity.GetComponent<SpriteComponent>();

            var state = Keyboard.GetState();

            if(state.IsKeyDown(controller.MoveLeftKey))
            {
                body.Velocity = new Vector2(-player.MaxWalkSpeed, body.Velocity.Y);
                sprite.Sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
            }
            else if(state.IsKeyDown(controller.MoveRightKey))
            {
                body.Velocity = new Vector2(player.MaxWalkSpeed, body.Velocity.Y);
                sprite.Sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
            }
            else
            {
                body.Velocity = new Vector2(0, body.Velocity.Y);
            }

            if(state.IsKeyDown(controller.JumpKey) && player.IsOnGround)
            {
                body.Velocity = new Vector2(0, -100f);
                player.IsOnGround = false;
            }
        }
    }
}