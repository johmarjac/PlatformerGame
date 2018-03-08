using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Components;
using MonoGame.Additions.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace PlatformerGame.Entities.Components
{
    internal sealed class EntityFactory : DrawableGameComponent
    {
        public EntityFactory(Game game, EntityComponentSystem ecs) : base(game)
        {
            Content = new ContentManager(Game.Content.ServiceProvider, game.Content.RootDirectory);
            Ecs = ecs;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            alienGreen_stand = Content.Load<Texture2D>("Assets/alienGreen_stand");
        }

        public Entity CreatePlayer(Vector2 position)
        {
            var entity = Ecs.CreateEntity();

            entity.Attach<TransformComponent>()
                .Size = alienGreen_stand.Bounds.Size.ToVector2();

            entity.Attach<SpriteComponent>()
                .Sprite = new Sprite(alienGreen_stand);

           entity.Attach<PlayerComponent>();
           entity.Attach<RigidbodyComponent>();
           entity.Attach<PlayerController>();

            entity.Attach<SpriteBatchComponent>()
                .SpriteBatch = Ecs.Game.Services.GetService<SpriteBatch>();
                        
            return entity;
        }

        public Entity CreateLevel(TiledMap level)
        {
            var entity = Ecs.CreateEntity();

            entity.Attach<TiledMapComponent>()
                .Map = level;

            entity.Attach<TiledMapRendererComponent>()
                .Renderer = new TiledMapRenderer(Game.GraphicsDevice);

            return entity;
        }
        
        private EntityComponentSystem Ecs { get; }
        private ContentManager Content { get; }
        private Texture2D alienGreen_stand;
    }
}