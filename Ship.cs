using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maid
{
    class Ship : SpaceObject
    {
        public bool Accelerating;
        private List<ShipAccessory> Accessories;
        private int CurrentWeapon;

        public Ship()
        {
            InitShip();
        }

        public void InitShip() {
            position = new Vector2(200, 100);
            Accessories = new List<ShipAccessory>();
            CurrentWeapon = 0;
            Velocity = new Vector2(0, 0);
            rotation = 0;
            RotationOrigin = new Vector2(0, 0);
        }

        public void AddAccessory(Vector2 Offset, Rectangle Sprite)
        {
            Accessories.Add(new ShipAccessory(Offset, Sprite));
        }

        public void SetSprite(Rectangle sprite)
        {
            Sprite = sprite;
            RotationOrigin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Render the Accessories
            for (int i = 0; i < Accessories.Count; i++)
            {
                Accessories[i].Draw(gameTime, spriteBatch, this.position);
            }

            base.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime, InputWrapper Input)
        {
            int mills = gameTime.ElapsedGameTime.Milliseconds;
            if (Input.KeyboardSt().IsKeyDown(Keys.Down))
            {
                //Velocity += new Vector2(0, 1);
            }

            if (Input.KeyboardSt().IsKeyDown(Keys.Up))
            {
                // find the component magnitudes for a unit vector pointing in the direction of the rotation
                Velocity += new Vector2((float)Math.Cos(rotation - (Math.PI / 2)), (float)Math.Sin(rotation - (Math.PI / 2)));
                Accelerating = true;
            }
            else
            {
                Accelerating = false;
            }

            if (Input.KeyboardSt().IsKeyDown(Keys.Right))
            {
                rotation += 0.1f;
            }

            if (Input.KeyboardSt().IsKeyDown(Keys.Left))
            {
                rotation -= 0.1f;
            }

            // Keep angle within 0 - 2pi
            rotation = rotation % (Math.PI * 2);

            position += (Velocity * mills / 1000);

            for (int i = 0; i < Accessories.Count; i++)
            {
                Accessories[i].UpdatePosition(position, rotation);
            }

            this.WrapPosition();
        }

        public Vector2 ActiveWeaponPosition()
        {
            CurrentWeapon++;
            CurrentWeapon = CurrentWeapon % Accessories.Count;
            return Accessories[CurrentWeapon].Position();
        }
    }
}
