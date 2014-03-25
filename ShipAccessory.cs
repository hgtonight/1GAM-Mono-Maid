using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maid
{
    class ShipAccessory : SpaceObject
    {
        Vector2 Offset, BasePosition;
        bool Animating;
        int AnimationIndex;
        List<Vector2> AnimationFrames;

        public ShipAccessory(Vector2 Offset1, Rectangle Sprite1)
        {
            this.Offset = Offset1 + new Vector2(Sprite1.Width / 2, Sprite1.Height / 2);
            this.BasePosition = new Vector2(0, 0);
            this.Sprite = Sprite1;
            Animating = false;
            AnimationIndex = 0;
            AnimationFrames = new List<Vector2>();
            // basic firing animation
            AnimationFrames.Add(new Vector2(0, 0));
            AnimationFrames.Add(new Vector2(0, -2));
            AnimationFrames.Add(new Vector2(0, -4));
            AnimationFrames.Add(new Vector2(0, -1));
            AnimationFrames.Add(new Vector2(0, 1));
            
        }

        public void UpdatePosition(Vector2 NewPosition, double Rot)
        {
            this.BasePosition = NewPosition;
            this.rotation = Rot;
        }

        public void UpdateDrawVector(Vector2 origin)
        {
            Matrix myRotationMatrix = Matrix.CreateRotationZ((float)this.rotation);
            Vector2 rotatedVector =  Vector2.Transform(Offset + AnimationFrames[AnimationIndex], myRotationMatrix);
            this.position = rotatedVector + origin;
        }

        public void StartAnimation()
        {
            Animating = true;
        }

        public override void Update(GameTime gameTime, InputWrapper Input)
        {
            if (Animating)
            {
                if (gameTime.ElapsedGameTime.Ticks % 4 == 0)
                {
                    AnimationIndex++;

                    if (AnimationIndex >= AnimationFrames.Count)
                    {
                        Animating = false;
                        AnimationIndex = 0;
                    }
                }
            }
            else
            {
                AnimationIndex = 0;
            }

            base.Update(gameTime, Input);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 origin)
        {
            this.UpdateDrawVector(origin);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
