using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using PlatformerGame.Scenes;

namespace PlatformerGame
{
    public class GameEntry : Game
    {
        public GameEntry()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }
        
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(spriteBatch);
            
            Services.AddService(new BoxingViewportAdapter(Window, GraphicsDevice, 1024, 768));

            camera = new Camera2D(Services.GetService<BoxingViewportAdapter>());
            camera.ZoomOut(0.5f);
            Services.AddService(camera);

            Components.Add(new ScreenGameComponent(this, new[] { new Ingame(this) }));
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());
            base.Draw(gameTime);
            spriteBatch.End();
        }

        private GraphicsDeviceManager graphics;
        private Camera2D camera;
        private SpriteBatch spriteBatch;
    }
}