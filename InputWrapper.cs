using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maid
{
    class InputWrapper
    {
        private KeyboardState LastKeyState, CurrentKeyState;

        //GamePadState LastState, CurrentState;
        public InputWrapper()
        {
            LastKeyState = Keyboard.GetState();
            CurrentKeyState = LastKeyState;
        }

        public bool JustPressed(Keys Key)
        {
            if (CurrentKeyState.IsKeyDown(Key) && LastKeyState.IsKeyUp(Key))
            {
                return true;
            }
            return false;
        }

        public bool JustReleased(Keys Key)
        {
            if (CurrentKeyState.IsKeyUp(Key) && LastKeyState.IsKeyDown(Key))
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            LastKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();
        }

        public void KeyHeld(Keys Key, int Length)
        {
            throw new NotImplementedException();
        }

        public KeyboardState KeyboardSt()
        {
            return CurrentKeyState;
        }
    }
}
