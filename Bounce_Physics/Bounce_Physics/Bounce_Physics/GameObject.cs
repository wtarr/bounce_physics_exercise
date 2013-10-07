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
        protected BoundingSphere BoundingSphere;
        protected Vector3 diffuseColor;

        //protected float Rotation;
        protected Vector3 Position;
        protected Vector3 Velocity;
        protected float Mass;
        protected Vector3 Up, Forward, Right;
        protected float RotationSpeed;

        public GameObject(Game game, Camera camera)
            : base(game)
        {
            _camera = camera;
            
            Position = Vector3.Zero;
            diffuseColor = new Vector3(0, 0, 255);
            Up = Vector3.Up;
            Forward = Vector3.Forward;
            Right = Vector3.Right;
            RotationSpeed = 0.1f;

            game.Components.Add(this);

        }

        public GameObject(Game game, Camera camera, Model model)
            : base(game)
        {
            _camera = camera;

            Position = Vector3.Zero;
            diffuseColor = new Vector3(255, 0, 255);
            Up = Vector3.Up;
            Forward = Vector3.Forward;
            Right = Vector3.Right;
            //RotationSpeed = 0.0001f;
            Model = model;
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
            RotateAboutRightAxis(gameTime);
            
            base.Update(gameTime);
        }

        public void RotateAboutUpAxis(GameTime gameTime)
        {
            // Create the rotation matrix
            Matrix m = Matrix.CreateFromAxisAngle(Up, gameTime.ElapsedGameTime.Milliseconds*RotationSpeed);
            // Apply it to the up and the forward
            Forward = Vector3.Transform(Forward, m);
            
            Right = Vector3.Transform(Right, m);
        }

        public void RotateAboutForwardAxis(GameTime gameTime)
        {
            // create the rotation matrix
            Matrix m = Matrix.CreateFromAxisAngle(Forward, gameTime.ElapsedGameTime.Milliseconds*RotationSpeed);

            Up = Vector3.Transform(Up, m);
            Right = Vector3.Transform(Right, m);

        }

        public void RotateAboutRightAxis(GameTime gameTime)
        {
            Matrix m = Matrix.CreateFromAxisAngle(Right, gameTime.ElapsedGameTime.Milliseconds*RotationSpeed);

            Up = Vector3.Transform(Up, m);
            Forward = Vector3.Transform(Forward, m);

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
                       

                        effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
                        effect.EmissiveColor = new Vector3(0, 0, 0);
                        

                        effect.World = transforms[mesh.ParentBone.Index]*
                                       Matrix.CreateWorld(Position, Forward, Up);
                        effect.View = _camera.CreateView();
                        effect.Projection = _camera.CreatePerspectiveFieldOfView();
                        effect.DiffuseColor = diffuseColor;
                    }

                    mesh.Draw();
                }
            }


            base.Draw(gameTime);
        }
    }


}
