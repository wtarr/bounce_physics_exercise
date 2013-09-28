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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Camera : GameComponent
    {

        private Game _game;
        private GraphicsDeviceManager _graphics;
        

        public Vector3 Target { get; set; }
        public Vector3 CameraPosition { get; set; }
        public Vector3 Orientation { get; set; }
        public float AspectRatio { get; set; }
        public float FieldOfView { get; set; }
        public float NearPlane { get; set; }
        public float FarPlane { get; set; }
        
        public Camera(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            _game = game;
            _graphics = graphics;

            Target = Vector3.Zero;
            CameraPosition = new Vector3(0.0f, 50.0f, 20.0f);
            AspectRatio = _graphics.GraphicsDevice.Viewport.AspectRatio;
            Orientation = Vector3.Up;
            FieldOfView = MathHelper.ToRadians(45.0f);
            NearPlane = 1.0f;
            FarPlane = 10000.0f;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            
            base.Update(gameTime);
        }

        public Matrix CreateView()
        {
            return Matrix.CreateLookAt(CameraPosition,
                        Target, Orientation);
        }

        public Matrix CreatePerspectiveFieldOfView()
        {
            return Matrix.CreatePerspectiveFieldOfView(
                        FieldOfView, AspectRatio,
                        NearPlane, FarPlane);
        }

        
    }
}
