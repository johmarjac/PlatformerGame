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
            Ecs = ecs;

            Content = new ContentManager(Game.Content.ServiceProvider, game.Content.RootDirectory);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            alienGreen_stand = Content.Load<Texture2D>("Assets/alienGreen_stand");
        }

        public Entity CreatePlayer(Vector2 position)
        {
            var entity = Ecs.CreateEntity();

            entity.Attach<SpriteComponent>()
                .Sprite = new Sprite(alienGreen_stand);

            entity.Attach<PlayerComponent>();

            entity.Attach<TransformComponent>()
                .Position = new Vector2(-20f, 5);

            entity.Attach<CollisionBodyComponent>();
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

            // add the poly line layer as body collsion

            entity.Attach<TiledMapRendererComponent>()
                .Renderer = new TiledMapRenderer(Game.GraphicsDevice);

            return entity;
        }
        
        private EntityComponentSystem Ecs { get; }
        private ContentManager Content { get; }
        private Texture2D alienGreen_stand;
    }
}