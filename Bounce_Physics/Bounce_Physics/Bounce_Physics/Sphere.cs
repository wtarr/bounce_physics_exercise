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
    public class Sphere : GameObject, IamASphere
    {
        private Game _game;

        public Sphere(Game game, Camera camera, Vector3 position, Vector3 velocity)
            : base(game, camera)
        {
            // TODO: Construct any child components here
            _game = game;
            Velocity = velocity;
            Position = position;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {

            Model = _game.Content.Load<Model>("Models\\sphere");
            BoundingRadius.Radius = Model.Meshes[0].BoundingSphere.Radius;
            Console.WriteLine(BoundingRadius.Radius);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            Position += Velocity*gameTime.ElapsedGameTime.Milliseconds/1000.0f;

            BoundingRadius.Center = Position;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public bool HasCollisionOccured(Sphere g1, Sphere g2)
        {
            throw new NotImplementedException();
        }
    }
}
