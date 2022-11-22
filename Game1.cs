using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            DisplayManager.graphicsDevice = _graphics;
           
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            

            foreach(string spritename in DisplayManager.spritePaths)
            {
                DisplayManager.spriteMap[spritename] = Content.Load<Texture2D>(spritename);
            }
            DisplayManager.defaultFont = Content.Load<SpriteFont>("font");

            GameManager.Initialize();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.P)) DisplayManager.SetScreenResolution(1920, 1080);
            if (Keyboard.GetState().IsKeyDown(Keys.R)) DisplayManager.SetScreenResolution(1280, 720);

            GameManager.Update(gameTime);
            DisplayManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            

            DisplayManager.Draw(_spriteBatch);
    


            base.Draw(gameTime);
        }
    }
}
