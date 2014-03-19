using System;
using Microsoft.Xna.Framework;

namespace Maid
{
    class Boolet : SpaceObject
    {
        private int TicksUntilDeath;
        private Vector2 Velocity;
        public Boolet(Vector2 Pos, Double Rot)
        {
            Position = Pos;
            Rotation = Rot;
            Sprite = SpaceSprites.laserBlue02();
            RotationOrigin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            Velocity = new Vector2((float)Math.Sin(Rotation) * 10, (float)Math.Cos(Rotation) * 10 * -1);
            TicksUntilDeath = 120;
        }

        public bool Update()
        {
            TicksUntilDeath--;
            Position += Velocity;

            this.WrapPosition();
            if (TicksUntilDeath < 0)
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
