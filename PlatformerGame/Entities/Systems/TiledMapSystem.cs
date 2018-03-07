using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Attributes;
using MonoGame.Extended;
using PlatformerGame.Entities.Components;

namespace PlatformerGame.Entities.Systems
{
    [ComponentSystem]
    [RequiredComponents(typeof(TiledMapComponent), typeof(TiledMapRendererComponent))]
    public class TiledMapSystem : ComponentSystem
    {
        public override void UpdateEntity(Entity entity, GameTime gameTime)
        {
            base.UpdateEntity(entity, gameTime);
        }

        public override void DrawEntity(Entity entity, GameTime gameTime)
        {
            base.DrawEntity(entity, gameTime);

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            var mapComponent = entity.GetComponent<TiledMapComponent>();
            var renderComponent = entity.GetComponent<TiledMapRendererComponent>();
            var camera = Game.Services.GetService<Camera2D>();

            var viewMatrix = camera.GetViewMatrix();
            var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, -1);

            renderComponent.Renderer.Draw(mapComponent.Map, ref viewMatrix, ref projectionMatrix);
        }
    }
}