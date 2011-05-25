using System;

namespace TrainEditor
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Editor form = new Editor();
            form.Show();
            Game1 game = new Game1(form.getDrawSurface(), form.pbEditor.Width, form.pbEditor.Height);
            form.game = game;
            game.Run();
        }
    }
#endif
}

