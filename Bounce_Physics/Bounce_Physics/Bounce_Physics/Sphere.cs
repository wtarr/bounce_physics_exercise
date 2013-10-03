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
        private Vector3 previousPosition;

        public Sphere(Game game, Camera camera, Vector3 position, Vector3 velocity, float mass)
            : base(game, camera)
        {
            // TODO: Construct any child components here
            _game = game;
            Velocity = velocity;
            Position = position;
            BoundingSphere.Center = Position;
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
            
            
            HasCollisionOccured(gameTime);

            previousPosition = Position;

            Position += Velocity * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            
            BoundingSphere.Center = Position;

            //BoundingSphere.Radius = 5f;





            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public bool HasCollisionOccured(GameTime gt)
        {
           
                foreach (var sphere in BounceyBounceGame.CollidableList)
                {
                    Sphere s = sphere as Sphere;
                    if (BoundingSphere.Intersects(s.BoundingSphere) && !s.Equals(this))
                    {
                        s.defuseColor = new Vector3(255, 0, 0);
                        ReactToCollison(this, s, gt);
                    }
                }
            
            return false;
        }

        public Vector3 DeterminePointOfContact(Sphere me, Sphere you)
        {
            Vector3 direction = Vector3.Normalize(you.Position - me.Position);
            return me.Position + me.BoundingSphere.Radius*direction;
        }
        
        public void ReactToCollison( Sphere s1, Sphere s2, GameTime gt)
        {
            

            CounterOverLap(s1, s2, gt);

            //CalcFinalVelocity(s1, s2);
        }

        private void CalcFinalVelocity(Sphere s1, Sphere s2)
        {
            // http://studiofreya.com/blog/3d-math-and-physics/simple-sphere-sphere-collision-detection-and-collision-response/

            // V can be broken into (V.n)n + (V -(V.n)n) i.e Vperp + Vparallel

            var normal = Vector3.Normalize(s2.Position - s1.Position);

            var x1 = Vector3.Dot(s1.Velocity, normal); //V.N

            var v1Parallel = normal*x1; // PARALLEL

            var v1Perpendicular = s1.Velocity - v1Parallel; // PERP

            var m1 = s1.Mass;

            // The other sphere
            normal = normal*-1;

            var x2 = Vector3.Dot(normal, s2.Velocity);

            var v2Parallel = normal*x2;

            var v2Perpendicular = s2.Velocity - v2Parallel;

            var m2 = s2.Mass;

            s1.Velocity = (v1Parallel*((m1 - m2)/(m2 + m2)) + v2Parallel*((2*m2)/(m1 + m2))) + v1Perpendicular;
            //v perp + v para (doesnt change)

            s2.Velocity = (v1Parallel*((2*m1)/(m1 + m2)) + v2Parallel*((m2 - m1)/(m1 + m2))) + v2Perpendicular;
        }
        
        private void CounterOverLap(Sphere s1, Sphere s2, GameTime gt)
        {

            var overlap = (s1.BoundingSphere.Radius + s2.BoundingSphere.Radius) - Vector3.Distance(s1.Position, s2.Position);

            var relVelocity = s1.Velocity - s2.Velocity;

            var u = relVelocity.Length();

            var t = overlap/u;

            var timeOfCollision = gt.ElapsedGameTime.Milliseconds/1000.0f - t;

            s1.Position = s1.previousPosition + s1.Velocity*timeOfCollision;
            s2.Position = s2.previousPosition + s2.Velocity*timeOfCollision;
            var distTest = Vector3.Distance(s1.Position, s2.Position);

            CalcFinalVelocity(s1, s2);

            // Catch up to current time

            var diff = gt.ElapsedGameTime.Milliseconds/1000.0f - timeOfCollision;

            s1.Position += s1.Velocity*diff;
            s2.Position += s2.Velocity*diff;

            //// Determine over lap
            //var overlap = Math.Abs((s1.BoundingSphere.Radius + s2.BoundingSphere.Radius) - Vector3.Distance(s1.Position, s2.Position));

            //var gcd = GreatestCommonDenominator(s1.Velocity.Length(), s2.Velocity.Length());
            //var s1Portion = s1.Velocity.Length()/gcd;
            //var s2Portion = s2.Velocity.Length()/gcd;

            //var s1Adjusted = (overlap/(s1Portion + s2Portion))*s1Portion;
            //var s2Adjusted = (overlap/(s1Portion + s2Portion))*s2Portion;

        }

        private float GreatestCommonDenominator(float a, float b)
        {
            // http://en.wikipedia.org/wiki/Euclidean_algorithm
            if (b == 0)
                return a;
            return GreatestCommonDenominator(b, a%b);
        }
    }
}
