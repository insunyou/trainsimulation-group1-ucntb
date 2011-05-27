using System;

namespace TrainSimXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            MainForm form = new MainForm();
	        form.Show();
	        Game1 game = new Game1(form.getDrawSurface(), form.pctSurface.Width, form.pctSurface.Height);
            game.onUpdate(new UpdatePanels(form.updatePanels));
            game.onInit(new InitPanels(form.initPanels));
            form.game = game;
	        game.Run();
        }
    }
#endif
}

