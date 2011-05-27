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
        private InitPanels initPanels;
        private UpdatePanels updatePanels;
        private int milisecondsPanels;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private IntPtr drawSurface;

        public RailRoad railroad { get; set; }
        private bool runSim = false;

        private SoundEffect whistle;

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
            railroad = rCtr.loadRailRoad("Railroad2.xml", Content);

            TrainSet train1;
            train1 = new TrainSet("Train #1", new List<TrainCart>(), new Engine(20));

            TrainCart cart1 = new TrainCart(Content);
            cart1.currentTrack = railroad.tracks[1];
            cart1.previousTrack = railroad.tracks[0];
            cart1.position = 50;
            cart1.maxSpeed = 60;
            cart1.setCart();
            train1.cartList.Add(cart1);

            railroad.trains.Add(train1);

            train1.locoDriver =  new LocoDriver("Per", train1, railroad);
           
            TrainSet train2;
            train2 = new TrainSet("Train #2", new List<TrainCart>(), new Engine(20));

            TrainCart cart2 = new TrainCart(Content);
            cart2.currentTrack = railroad.tracks[7];
            cart2.previousTrack = railroad.tracks[6];
            cart2.position = 10;
            cart2.maxSpeed = 60;
            cart2.rotation = MathHelper.ToRadians(-90);
            cart2.setCart();
            train2.cartList.Add(cart2);

            railroad.trains.Add(train2);

            train2.locoDriver = new LocoDriver("Ole", train2, railroad);
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

            whistle = Content.Load<SoundEffect>("train");
            initPanels(railroad);
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
                railroad.updateSensors();
                foreach (TrainSet train in railroad.trains)
                    train.locoDriver.update(gameTime);
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
                if(t.signal != null)
                    spriteBatch.Draw(t.signal.getTexture(), t.signal.position, null, new Color(255, 255, 255, 255), 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }

            foreach (TrainSet train in railroad.trains)
                foreach(TrainCart cart in train.cartList)
                {
                    spriteBatch.Draw(cart.gfx, cart.currentPos, null, new Color(255, 255, 255, 255), cart.rotation, cart.origin, 1.0f, SpriteEffects.None, 0.0f);
                }
            spriteBatch.End();
            milisecondsPanels += gameTime.ElapsedGameTime.Milliseconds;
            if (milisecondsPanels > 200)
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

        public void playSound()
        {
            whistle.Play();
        }

        public void onUpdate(UpdatePanels updatePanels)
        {
            this.updatePanels += updatePanels;
        }

        public void onInit(InitPanels initPanels)
        {
            this.initPanels += initPanels;
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
