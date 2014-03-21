using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class SpaceObject
    {
        static public Vector2 WrapCoords;
        protected Vector2 position, RotationOrigin, Velocity;
        protected double rotation;
        protected Rectangle Sprite;

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int Overlap = 0;
            // Render the base object
            spriteBatch.Draw(SpaceSprites.Sheet, position, Sprite, Color.White, (float)rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            
            // Figure out if the object is crossing the left/right border
            if (position.X - (Sprite.Width / 2) < 0)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, position + new Vector2(WrapCoords.X, 0), Sprite, Color.White, (float)rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
            else if (position.X + (Sprite.Width / 2) > WrapCoords.X)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, position - new Vector2(WrapCoords.X, 0), Sprite, Color.White, (float)rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }

            // Figure out if the object is crossing the top/bottom border
            if (position.Y - (Sprite.Height / 2) < 0)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, position + new Vector2(0, WrapCoords.Y), Sprite, Color.White, (float)rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
            else if (position.Y + (Sprite.Height / 2) > WrapCoords.Y)
            {
                Overlap++;
                spriteBatch.Draw(SpaceSprites.Sheet, position - new Vector2(0, WrapCoords.Y), Sprite, Color.White, (float)rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }

            // If both dimensions overlap, we have to draw the fourth object
            if (Overlap == 2)
            {
                spriteBatch.Draw(SpaceSprites.Sheet, position + WrapCoords, Sprite, Color.White, (float)rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        public virtual void WrapPosition()
        {
            if (position.X > WrapCoords.X)
            {
                position.X -= WrapCoords.X;
            }
            else if (position.X < 0)
            {
                position.X += WrapCoords.X;
            }

            if (position.Y > WrapCoords.Y)
            {
                position.Y -= WrapCoords.Y;
            }
            else if (position.Y < 0)
            {
                position.Y += WrapCoords.Y;
            }
        }

        public double Rotation()
        {
            return rotation;
        }

        public Vector2 Position()
        {
            return position;
        }
    }
}
