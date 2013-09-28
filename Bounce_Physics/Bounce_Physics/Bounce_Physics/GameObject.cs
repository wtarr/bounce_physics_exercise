using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Bounce_Physics
{
    
    public class GameObject : DrawableGameComponent
    {

        private Camera _camera;
        protected Model Model;
        protected BoundingSphere BoundingRadius;
        

        public float Rotation { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }

        public GameObject(Game game, Camera camera)
            : base(game)
        {
            _camera = camera;
            Rotation = 0.0f;
            Position = Vector3.Zero;

            game.Components.Add(this);

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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Model != null)
            {

                Matrix[] transforms = new Matrix[Model.Bones.Count];
                Model.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (var mesh in Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = transforms[mesh.ParentBone.Index] *
                                       Matrix.CreateRotationY(Rotation) *
                                       Matrix.CreateTranslation(Position);
                        effect.View = _camera.CreateView();
                        effect.Projection = _camera.CreatePerspectiveFieldOfView();
                        effect.DiffuseColor = new Vector3(0, 0, 255);
                    }

                    mesh.Draw();
                }
            }

            base.Draw(gameTime);
        }
    }


}
