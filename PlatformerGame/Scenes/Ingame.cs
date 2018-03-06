using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Components;
using MonoGame.Additions.Entities.Systems;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using PlatformerGame.Entities.Components;
using PlatformerGame.Entities.Levels;
using PlatformerGame.Entities.Systems;
using System.Linq;
using System.Reflection;

namespace PlatformerGame.Scenes
{
    public sealed class Ingame : Screen
    {
        public Ingame(Game game)
        {
            _game = game;
            _mapRenderer = new TiledMapRenderer(_game.GraphicsDevice);
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            _ecs = new EntityComponentSystem(_game);
        }

        public override void Initialize()
        {
            base.Initialize();

            _ecs.RegisterSystem(new SpriteSystem(_game));
            _ecs.RegisterSystem(new PlayerControllerSystem(_game));

            _entityFactory = new EntityFactory(_ecs, _game.Content);
            _game.Services.GetService<LevelManager>().SetLevel("Levels\\testlevel");
            MyPlayer = _entityFactory.CreatePlayer(Vector2.Zero);


            MyPlayer.GetComponent<PlayerComponent>().MoveTo(new Vector2(5, -20));
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _ecs.Update(gameTime);

            _game.Services.GetService<Camera2D>().LookAt(MyPlayer.GetComponent<PlayerComponent>().Position);            

            var currentLevel = _game.Services.GetService<LevelManager>().CurrentLevel;
            var collisionLayer = currentLevel.Map.GetLayer<TiledMapObjectLayer>("CollisionIndicator");
            var polyLineObj = collisionLayer.Objects.FirstOrDefault(o => o is TiledMapPolylineObject) as TiledMapPolylineObject;
            var playerComponent = MyPlayer.GetComponent<PlayerComponent>();
            var spriteComponent = MyPlayer.GetComponent<SpriteComponent>();

            playerComponent.MoveBy(new Vector2(0, 10f));

            if (polyLineObj != null)
            {
                for (int i = 0; i < polyLineObj.Points.Length - 1; i++)
                {
                    var currentPoint = polyLineObj.Points[i] + polyLineObj.Position;
                    var nextPoint = polyLineObj.Points[i + 1] + polyLineObj.Position;


                    if(playerComponent.Position.X + (spriteComponent.Sprite.BoundingRectangle.Width / 2f) >= currentPoint.X && playerComponent.Position.X + (spriteComponent.Sprite.BoundingRectangle.Width / 2f) < nextPoint.X)
                    {
                        float y = SolveLinearEquation(currentPoint, nextPoint, playerComponent.Position.X + (spriteComponent.Sprite.BoundingRectangle.Width / 2f));
                        if (y <= playerComponent.Position.Y + (spriteComponent.Sprite.BoundingRectangle.Height))
                        {
                            playerComponent.MoveTo(new Vector2(playerComponent.Position.X, y - spriteComponent.Sprite.BoundingRectangle.Height));
                        }   
                    }
                }
            }

            base.Update(gameTime);
        }

        private float SolveLinearEquation(Point2 p1, Point2 p2, float x)
        {
            var m = (p1.Y - p2.Y) / (p1.X - p2.X);
            var b = p1.Y - (m * p1.X);
            return m * x + b;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _game.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            _game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            var currentLevel = _game.Services.GetService<LevelManager>().CurrentLevel;

            var viewMatrix = _game.Services.GetService<Camera2D>().GetViewMatrix();
            var projMatrix = Matrix.CreateOrthographicOffCenter(0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height, 0, 0, -1f);

            var terrainLayer = currentLevel.Map.GetLayer<TiledMapTileLayer>("Terrain");
            
            _mapRenderer.Draw(terrainLayer, ref viewMatrix, ref projMatrix);

            var cam = _game.Services.GetService<Camera2D>();
            var batch = _game.Services.GetService<SpriteBatch>();

            batch.Begin(transformMatrix: cam.GetViewMatrix());
            _ecs.Draw(gameTime);
            batch.End();
        }

        private Game _game;
        private TiledMapRenderer _mapRenderer;
        private SpriteBatch _spriteBatch;
        private EntityComponentSystem _ecs;
        private EntityFactory _entityFactory;

        private Entity MyPlayer;
    }
}