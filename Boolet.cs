using System;
using Microsoft.Xna.Framework;

namespace Maid
{
    class Boolet : SpaceObject
    {
        private int TicksUntilDeath;
        private new Vector2 Velocity;
        public Boolet(Vector2 Pos, Double Rot)
        {
            position = Pos;
            rotation = Rot;
            Sprite = SpaceSprites.laserBlue02();
            RotationOrigin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            Velocity = new Vector2((float)Math.Sin(rotation) * 2, (float)Math.Cos(rotation) * 2 * -1);
            TicksUntilDeath = 120;
        }

        public bool Update()
        {
            TicksUntilDeath--;
            position += Velocity;

            this.WrapPosition();
            if (TicksUntilDeath <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
