using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Attributes;
using MonoGame.Additions.Entities.Components;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using PlatformerGame.Entities.Components;
using System.Collections.Generic;
using System.Linq;

namespace PlatformerGame.Entities.Systems
{
    [ComponentSystem]
    [RequiredComponents(typeof(TiledMapComponent), typeof(TiledMapRendererComponent))]
    public class TiledMapSystem : ComponentSystem
    {
        private List<Entity> Entities;

        public TiledMapSystem()
        {
            Entities = new List<Entity>();
        }

        public override void OnEntityCreated(Entity entity)
        {
            base.OnEntityCreated(entity);

            Entities.Add(entity);
        }

        public override void OnEntityDestroyed(Entity entity)
        {
            base.OnEntityDestroyed(entity);

            Entities.Remove(entity);
        }

        public override void UpdateEntity(Entity entity, GameTime gameTime)
        {
            base.UpdateEntity(entity, gameTime);

            var map = entity.GetComponent<TiledMapComponent>();
            var layer = default(TiledMapObjectLayer);
            if ((layer = map.Map.GetLayer<TiledMapObjectLayer>("Collision")) == null)
                return;

            if (layer.Objects.Length < 1)
                return;

            foreach(var ent in Entities
                .Where(e => e.HasComponent<TransformComponent>() && e.HasComponent<RigidbodyComponent>() && e.HasComponent<PlayerComponent>()))
            {
                var transform = ent.GetComponent<TransformComponent>();
                var body = ent.GetComponent<RigidbodyComponent>();
                var player = ent.GetComponent<PlayerComponent>();

                if(!body.IsStatic)
                {
                    for (int j = 0; j < layer.Objects.Length; j++)
                    {
                        for (int i = 0; i < (layer.Objects[j] as TiledMapPolylineObject).Points.Length - 1; i++)
                        {
                            var obj = layer.Objects[j] as TiledMapPolylineObject;
                            var currentPoint = obj.Points[i] + obj.Position;
                            var nextPoint = obj.Points[i + 1] + obj.Position;

                            if (transform.Position.X + (transform.BoundingRectangle.Width * 0.5f) >= currentPoint.X && transform.Position.X + (transform.BoundingRectangle.Width * 0.5f) < nextPoint.X)
                            {
                                float lineY = SolveLinearEquation(currentPoint, nextPoint, transform.Position.X + (transform.BoundingRectangle.Width * 0.5f));

                                if ((transform.Position.Y + transform.BoundingRectangle.Height > lineY))
                                {
                                    if (player.IsOnGround)
                                        body.Velocity = new Vector2(body.Velocity.X, 0);

                                    transform.Position = new Vector2(transform.Position.X, lineY - transform.BoundingRectangle.Height);
                                    player.IsOnGround = true;
                                }
                            }
                        }
                    }
                }
            }
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
        
        private float SolveLinearEquation(Point2 p1, Point2 p2, float x)
        {
            var m = (p1.Y - p2.Y) / (p1.X - p2.X);
            var b = p1.Y - (m * p1.X);
            return m * x + b;
        }
    }
}