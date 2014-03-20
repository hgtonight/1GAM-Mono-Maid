using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class SpaceBoolet
    {
        public Vector2 Position, Velocity;
        private double Rotation;
        public int TicksTilDeath;
        public Rectangle Sprite;

        public SpaceBoolet(Vector2 Origin, double Rot)
        {
            Position = Origin;
            Rotation = Rot;
            Velocity = new Vector2((float)Math.Sin(Rotation) * 10, (float)Math.Cos(Rotation) * -1 * 10);
            Sprite = SpaceSprites.laserBlue01();
            TicksTilDeath = 180;
        }

        public bool Update(GameTime gameTime)
        {
            Position += Velocity;
            TicksTilDeath--;

            if (TicksTilDeath <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpaceSprites.Sheet, Position, Sprite, Color.White, (float)Rotation, new Vector2(Sprite.Width / 2, Sprite.Height / 2), 1.0f, SpriteEffects.None, 0.0f);
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
        }
    }
}
