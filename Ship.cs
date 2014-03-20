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
        private Vector2[] AccessoryVectors, DrawAccessoryVectors;
        private List<ShipAccessory> Weapons;
        private Rectangle Sprite;
        private List<ShipAccessory> Accessories;

        public Ship()
        {
            InitShip();
        }

        public void InitShip() {
            Position = new Vector2(200, 100);

            AccessoryVectors = new Vector2[3];
            DrawAccessoryVectors = new Vector2[3];

            Position = new Vector2(400, 200);
            Accessories = new List<ShipAccessory>();
            
            Velocity = new Vector2(0, 0);
            Rotation = 0;
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
            UpdateAccessoryVectors();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Render the Accessories
            for (int i = 0; i < Accessories.Count; i++)
            {
                Accessories[i].Draw(gameTime, spriteBatch);
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
                Velocity += new Vector2((float)Math.Cos(Rotation - (Math.PI / 2)), (float)Math.Sin(Rotation - (Math.PI / 2)));
                Accelerating = true;
            }
            else
            {
                Accelerating = false;
            }

            if (Input.KeyboardSt().IsKeyDown(Keys.Right))
            {
                Rotation += 0.1f;
            }

            if (Input.KeyboardSt().IsKeyDown(Keys.Left))
            {
                Rotation -= 0.1f;
            }

            // Keep angle within 0 - 2pi
            Rotation = Rotation % (Math.PI * 2);

            Position += (Velocity * mills / 1000);

            UpdateAccessoryVectors();
        }

        private void UpdateAccessoryVectors()
        {
            for (int i = 0; i < Accessories.Count; i++)
            {
                Accessories[i].UpdatePosition(Position);
                Accessories[i].UpdateDrawVector(RotationOrigin, (float)Rotation);
            }
        }

        public override void WrapPosition()
        {
            base.WrapPosition();
            UpdateAccessoryVectors();
        }

        public Vector2 ActiveWeaponPosition()
        {
            return Position + new Vector2(42, -22);
        }
    }
}
