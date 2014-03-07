using System;

namespace Maid
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Maid game = new Maid())
            {
                game.Run();
            }
        }
    }
#endif
}

