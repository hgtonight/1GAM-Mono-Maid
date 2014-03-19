using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class SpaceObject
    {
        static public Vector2 WrapCoords;
        protected Vector2 Position, RotationOrigin, Velocity;
        protected double Rotation;
        protected Rectangle Sprite;

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int Overlap = 0;
            // Render the base object
            spriteBatch.Draw(SpaceSprites.Sheet, Position, Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            
            // Figure out if the object is crossing the left/right border
            if (Position.X - (Sprite.Width / 2) < 0)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, Position + new Vector2(WrapCoords.X, 0), Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
            else if (Position.X + (Sprite.Width / 2) > WrapCoords.X)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, Position - new Vector2(WrapCoords.X, 0), Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }

            // Figure out if the object is crossing the top/bottom border
            if (Position.Y - (Sprite.Height / 2) < 0)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, Position + new Vector2(0, WrapCoords.Y), Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
            else if (Position.Y + (Sprite.Height / 2) > WrapCoords.Y)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, Position - new Vector2(0, WrapCoords.Y), Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }

            // If both dimensions overlap, we have to draw the fourth object
            if (Overlap == 2)
            {
                spriteBatch.Draw(SpaceSprites.Sheet, Position + WrapCoords, Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        public virtual void WrapPosition()
        {
            if (Position.X > WrapCoords.X)
            {
                Position.X -= WrapCoords.X;
            }
            else if (Position.X < 0)
            {
                Position.X += WrapCoords.X;
            }

            if (Position.Y > WrapCoords.Y)
            {
                Position.Y -= WrapCoords.Y;
            }
            else if (Position.Y < 0)
            {
                Position.Y += WrapCoords.Y;
            }
        }

        public double CurrentRotation()
        {
            return Rotation;
        }
    }
}
