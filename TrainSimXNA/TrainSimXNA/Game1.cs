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
using TrainSimulator.Model;
using TrainSimulator.Ctr;

namespace TrainSimXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private UpdatePanels updatePanels;
        private int milisecondsPanels;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private IntPtr drawSurface;

        private RailRoad railroad;
        private LocoDriver locoDriver;
        private bool runSim = false;

        public Game1(IntPtr drawSurface, int width, int height)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.drawSurface = drawSurface;
	        graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
	        System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged += new EventHandler(Game1_VisibleChanged);
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            RailRoadCtr rCtr = new RailRoadCtr();
            railroad = rCtr.loadRailRoad("RailRoad2.xml", Content);

            TrainSet train1;
            train1 = new TrainSet();

            train1.name = "Train #1";
            TrainCart cart1 = new TrainCart(Content);
            cart1.currentTrack = railroad.tracks[0];
            cart1.previousTrack = railroad.tracks[1];
            cart1.position = 80;
            cart1.maxSpeed = 60;
            cart1.setCart();
            train1.cartList.Add(cart1);

            //TrainCart cart2 = new TrainCart(Content);
            //cart2.currentTrack = railroad.tracks[1];
            //cart2.previousTrack = railroad.tracks[0];
            //cart2.position = 5;
            //cart2.maxSpeed = 60;
            //cart2.setCart();
            //train1.cartList.Add(cart2);

            //TrainCart cart3 = new TrainCart(Content);
            //cart3.currentTrack = railroad.tracks[0];
            //cart3.previousTrack = railroad.tracks[7];
            //cart3.position = 30;
            //cart3.maxSpeed = 60;
            //cart3.setCart();
            //train1.cartList.Add(cart3);

            train1.engine = new Engine(train1, 20);
            train1.engine.Start();

            railroad.trains.Add(train1);

            locoDriver = new LocoDriver("Per", train1, railroad);


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (runSim)
            {
                locoDriver.freeToRun();
                foreach (TrainSet train in railroad.trains)
                {
                //    //while (train.engine.currentSpeed < 10)
                //    //    train.engine.accelerate(gameTime);
                    train.engine.updatePostion(gameTime);
                //    //foreach (TrainCart cart in train.cartList)
                //    //{
                //    //    cart.moveCart(2);
                //    //}
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            spriteBatch.Begin();
            foreach(Track t in railroad.tracks)
            {
                spriteBatch.Draw(t.gfx, t.position, null, new Color(255, 255, 255, 255), t.rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
                foreach(Signal s in t.signals)
                    spriteBatch.Draw(s.getTexture(), s.position, null, new Color(255, 255, 255, 255), 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }

            foreach (TrainSet train in railroad.trains)
                foreach(TrainCart cart in train.cartList)
                {
                    spriteBatch.Draw(cart.gfx, cart.currentPos, null, new Color(255, 255, 255, 255), cart.rotation, cart.origin, 1.0f, SpriteEffects.None, 0.0f);
                }
            spriteBatch.End();
            milisecondsPanels += gameTime.ElapsedGameTime.Milliseconds;
            if (milisecondsPanels > 1000)
            {
                updatePanels(railroad);
                milisecondsPanels = 0;
            }
            base.Draw(gameTime);
        }

        public void startSim()
        {
            runSim = true;
        }

        public void stopSim()
        {
            runSim = false;
        }

        public void onUpdate(UpdatePanels updatePanels)
        {
            this.updatePanels += updatePanels;
        }

        private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
	    {
	        e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle =
	        drawSurface;
	    }

        private void Game1_VisibleChanged(object sender, EventArgs e)
	    {
	        if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
	            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
	    }
    }
}
