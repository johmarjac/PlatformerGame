using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Components;
using MonoGame.Additions.Graphics;

namespace PlatformerGame.Entities.Components
{
    internal sealed class EntityFactory
    {
        public EntityFactory(EntityComponentSystem ecs, ContentManager content)
        {
            Ecs = ecs;

            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            PixelArt = content.Load<Texture2D>("Assets/PixelArt");
        }

        public Entity CreatePlayer(Vector2 position)
        {
            var entity = Ecs.CreateEntity();

            entity.Attach<PlayerComponent>();
            entity.Attach<PlayerController>();

            var spriteBatchComponent = entity.Attach<SpriteBatchComponent>();
            spriteBatchComponent.SpriteBatch = Ecs.Game.Services.GetService<SpriteBatch>();

            var spriteComponent = entity.Attach<SpriteComponent>();
            spriteComponent.Sprite = new Sprite(PixelArt);
            spriteComponent.Sprite.Scale = 0.5f;

            return entity;
        }
        
        private EntityComponentSystem Ecs { get; }
        private Texture2D PixelArt;
    }
}