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

        public Sphere(Game game, Camera camera, Vector3 position, Vector3 velocity, float mass)
            : base(game, camera)
        {
            // TODO: Construct any child components here
            _game = game;
            Velocity = velocity;
            Position = position;
            Mass = mass;
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
            BoundingSphere.Radius = Model.Meshes[0].BoundingSphere.Radius;
            

            Console.WriteLine(BoundingSphere.Radius);

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

            BoundingSphere.Center = Position;

            HasCollisionOccured();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public bool HasCollisionOccured()
        {
            foreach (var sphere in BounceyBounceGame.CollidableList)
            {
                Sphere s = sphere as Sphere;
                if (this.BoundingSphere.Intersects(s.BoundingSphere) && s!= this)
                {
                    s.defuseColor = new Vector3(255, 0, 0);
                    CalculateFinalVelocity(this, s);
                }
            }

            return false;
        }

        public Vector3 DeterminePointOfContact(Sphere me, Sphere you)
        {
            Vector3 direction = Vector3.Normalize(you.Position - me.Position);
            return me.Position + me.BoundingSphere.Radius*direction;
        }
        
        public void CalculateFinalVelocity( Sphere s1, Sphere s2)
        {
            // http://studiofreya.com/blog/3d-math-and-physics/simple-sphere-sphere-collision-detection-and-collision-response/

            var normal = Vector3.Normalize(s2.Position - s1.Position);
            
            var x1 = Vector3.Dot(normal, s1.Velocity);

            var v1x = normal * x1;

            var v1y = s1.Velocity - v1x;

            var m1 = s1.Mass;

            // The other sphere
            normal = normal*-1;
            
            var x2 = Vector3.Dot(normal, s2.Velocity);

            var v2x = normal*x2;

            var v2y = s2.Velocity - v2x;

            var m2 = s2.Mass;

            s1.Velocity = v1x*((m1 - m2)/(m2 + m2)) + v2x*((2*m2)/(m1 + m2)) + v1y;

            s2.Velocity = v1x*((2*m1)/(m1 + m2)) + v2x*((m2 - m1)/(m1 + m2)) + v2y;



        }


        
    }
}
