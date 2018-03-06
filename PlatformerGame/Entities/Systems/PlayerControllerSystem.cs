using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Attributes;
using PlatformerGame.Entities.Components;

namespace PlatformerGame.Entities.Systems
{
    [RequiredComponents(typeof(PlayerController), typeof(PlayerComponent))]
    public class PlayerControllerSystem : ComponentSystem
    {
        private readonly Vector2 GravityVec = new Vector2(0, 9.8f);

        public PlayerControllerSystem(Game game) : base(game)
        {
        }

        public override void UpdateEntity(Entity entity, GameTime gameTime)
        {
            base.UpdateEntity(entity, gameTime);

            var controller = entity.GetComponent<PlayerController>();
            var player = entity.GetComponent<PlayerComponent>();

            var state = Keyboard.GetState();

            if(state.IsKeyDown(controller.MoveLeftKey))
            {
                player.MoveBy(new Vector2(-player.WalkSpeed * gameTime.ElapsedGameTime.Milliseconds, 0));
            }
            
            if(state.IsKeyDown(controller.MoveRightKey))
            {
                player.MoveBy(new Vector2(player.WalkSpeed * gameTime.ElapsedGameTime.Milliseconds, 0));
            }
            
        }
    }
}