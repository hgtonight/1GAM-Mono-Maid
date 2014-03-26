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

namespace Maid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Maid : Microsoft.Xna.Framework.Game
    {
        const int MAX_BOOLETS = 20;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ship PlayerShip;
        InputWrapper Input;
        SoundEffect FlameFuel, LaserPewPew;
        SoundEffectInstance Accel;
        List<Boolet> Boolets;

        public Maid()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            PlayerShip = new Ship();
            PlayerShip.AddAccessory(new Vector2(-45, -45), SpaceSprites.gun00());
            PlayerShip.AddAccessory(new Vector2(-1 * SpaceSprites.gun00().Width + 1, -79), SpaceSprites.gun00());
            PlayerShip.AddAccessory(new Vector2(15, -45), SpaceSprites.gun00());
            Input = new InputWrapper();
            Boolets = new List<Boolet>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpaceSprites.Sheet = Content.Load<Texture2D>("sheet");
            FlameFuel = Content.Load<SoundEffect>("accellerate");
            LaserPewPew = Content.Load<SoundEffect>("laser9");
            Accel = FlameFuel.CreateInstance();
            PlayerShip.SetSprite(SpaceSprites.playerShip1_red());

            SpaceObject.WrapCoords = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            Input.Update(gameTime);
            PlayerShip.Update(gameTime, Input);

            if(Input.JustPressed(Keys.Space) && Boolets.Count < MAX_BOOLETS) {
                LaserPewPew.Play();
                Boolets.Add(new Boolet(PlayerShip.ActiveWeaponPosition(), PlayerShip.Rotation()));
            }

            for (int i = 0; i < Boolets.Count; i++)
            {
                if (!Boolets[i].Update())
                {
                    Boolets.RemoveAt(i);
				}
			}

            if (PlayerShip.Accelerating)
            {
                if (Accel.State != SoundState.Playing)
                {
                    Accel.Play();
                }
            }
            else
            {
                Accel.Stop();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            PlayerShip.Draw(gameTime, spriteBatch);

            for (int i = 0; i < Boolets.Count; i++)
            {
                Boolets[i].Draw(gameTime, spriteBatch);
            }
            
           
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
