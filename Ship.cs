using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maid
{
    class Ship
    {
        public Vector2 Position, Velocity, RotationOrigin;
        public double Rotation;
        public bool Accelerating;
        private Rectangle Sprite;
        private Vector2[] AccessoryVectors, DrawAccessoryVectors;

        public Ship()
        {
            InitShip();
        }

        public void InitShip() {
            Position = new Vector2(400, 200);

            AccessoryVectors = new Vector2[3];
            DrawAccessoryVectors = new Vector2[3];

            Velocity = new Vector2(0, 0);
            Rotation = 0;
            RotationOrigin = new Vector2(0, 0);
        }

        public void SetSprite(Rectangle sprite)
        {
            Sprite = sprite;
            RotationOrigin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);

            UpdateAccessoryVectors();

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Render the Accessories
            for (int i = 0; i < DrawAccessoryVectors.Length; i++)
            {
                spriteBatch.Draw(SpaceSprites.Sheet, DrawAccessoryVectors[i], SpaceSprites.gun00(), Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
            
            // Render dat ship
            spriteBatch.Draw(SpaceSprites.Sheet, Position, Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
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
            DrawAccessoryVectors = (Vector2[])AccessoryVectors.Clone();
            RotatePoints(ref Position, (float)Rotation, ref DrawAccessoryVectors);
        }

        private void UpdateAccessoryVectors()
        {
            AccessoryVectors[0] = Position + new Vector2(12, 10);
            AccessoryVectors[1] = Position + new Vector2(42, -22);
            AccessoryVectors[2] = Position + new Vector2(72, 10);
        }

        private static void RotatePoints(ref Vector2 origin, float radians, ref Vector2[] Vectors)
        {
            Matrix myRotationMatrix = Matrix.CreateRotationZ(radians);

            for (int i = 0; i < Vectors.Length; i++)
            {
                // Rotate relative to origin.
                Vector2 rotatedVector =  Vector2.Transform(Vectors[i] - origin, myRotationMatrix);

                // Add origin to get final location.
                Vectors[i] = rotatedVector + origin;
            }
        }

        public void WrapPosition(int X, int Y)
        {
            if (Position.X > X)
            {
                Position.X -= X;
            }
            else if (Position.X < 0)
            {
                Position.X += X;
            }

            if (Position.Y > Y)
            {
                Position.Y -= Y;
            }
            else if (Position.Y < 0)
            {
                Position.Y += Y;
            }

            UpdateAccessoryVectors();
        }
    }
}
