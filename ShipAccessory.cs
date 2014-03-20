using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class ShipAccessory
    {
        Vector2 Offset, Position, DrawVector, RotationOrigin;
        double Rotation;
        Rectangle Sprite;

        public ShipAccessory(Vector2 Offset1, Rectangle Sprite1)
        {
            this.Offset = Offset1;
            this.Position = new Vector2(0, 0) + Offset1;
            this.Sprite = Sprite1;
        }

        public void UpdatePosition(Vector2 NewPosition)
        {
            this.Position = NewPosition + this.Offset;
        }

        public void UpdateDrawVector(Vector2 origin, float radians)
        {
            this.Rotation = radians;
            this.RotationOrigin = origin;
            Matrix myRotationMatrix = Matrix.CreateRotationZ(radians);
            Vector2 rotatedVector =  Vector2.Transform(this.Position - origin, myRotationMatrix);
            this.DrawVector = rotatedVector + origin;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpaceSprites.Sheet, DrawVector, Sprite, Color.White, (float)Rotation, RotationOrigin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
