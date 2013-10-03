using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bounce_Physics
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BounceyBounceGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static List<IDrawable> CollidableList; 
        
        private Camera _camera;
        
        public BounceyBounceGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            CollidableList = new List<IDrawable>();
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _camera = new Camera(this, graphics);

            Sphere s1 = new Sphere(this, _camera, new Vector3(-20, 0, 0), new Vector3(5, 0, 0), 10);
            Sphere s2 = new Sphere(this, _camera, new Vector3(20, 0, 0), new Vector3(-5, 0, 0), 10);
            Sphere s3 = new Sphere(this, _camera, new Vector3(0, 1, -20), new Vector3(0, 0, 5), 100);

            CollidableList.Add(s1);
            CollidableList.Add(s2);
            CollidableList.Add(s3);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
