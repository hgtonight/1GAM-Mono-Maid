using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class ShipAccessory
    {
        private Vector2 Offset;
        private Rectangle Sprite;

        public ShipAccessory(Vector2 offset)
        {
            Offset = offset;
        }

        public void SetSprite(Rectangle sprite)
        {
            Sprite = sprite;
        }
    }
}
