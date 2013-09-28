using System;

namespace Bounce_Physics
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BounceyBounceGame game = new BounceyBounceGame())
            {
                game.Run();
            }
        }
    }
#endif
}

