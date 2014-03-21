using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class ShipAccessory : SpaceObject
    {
        Vector2 Offset, InternalPosition;

        public ShipAccessory(Vector2 Offset1, Rectangle Sprite1)
        {
            this.Offset = Offset1;
            this.InternalPosition = new Vector2(0, 0) + Offset1;
            this.Sprite = Sprite1;
        }

        public void UpdatePosition(Vector2 NewPosition, double Rot)
        {
            this.InternalPosition = NewPosition + this.Offset;
            this.rotation = Rot;
        }

        public void UpdateDrawVector(Vector2 origin)
        {
            this.RotationOrigin = origin;
            Matrix myRotationMatrix = Matrix.CreateRotationZ((float)this.rotation);
            Vector2 rotatedVector =  Vector2.Transform(this.InternalPosition - origin, myRotationMatrix);
            this.position = rotatedVector + origin;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 origin)
        {
            this.UpdateDrawVector(origin);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
