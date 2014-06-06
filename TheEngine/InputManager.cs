using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheEngine
{
    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }
    public static class InputManager
    {
        // Input states
        private static KeyboardState currentState;
        private static KeyboardState previousState;
        private static MouseState mouseState;
        private static MouseState previousMouseState;
        
        /// <summary>
        /// Update the states of the keyboard and mouse.
        /// </summary>
        public static void Update()
        {
            if (currentState != null)
            {
                previousState = currentState;
            }
            if (mouseState != null)
            {
                previousMouseState = mouseState;
            }
            
            
            currentState = Keyboard.GetState();
            mouseState = Mouse.GetState();

        }

        /// <summary>
        /// Returns true if the given key is currently up.
        /// </summary>
        /// <param name="key">The key you want to check.</param>
        /// <returns></returns>
        public static bool IsKeyUp(Keys key)
        {
            if (currentState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the given key is currently down.
        /// </summary>
        /// <param name="key">The key you want to check.</param>
        /// <returns></returns>
        public static bool IsKeyDown(Keys key)
        {
            if (currentState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the given key was previously down, but is now up.
        /// </summary>
        /// <param name="key">The key you want to check.</param>
        /// <returns></returns>
        public static bool IsKeyReleased(Keys key)
        {
            if (currentState.IsKeyUp(key) && previousState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returs true if the given key was previously up and is now down.
        /// </summary>
        /// <param name="key">The key you want to check.</param>
        /// <returns></returns>
        public static bool IsKeyPressed(Keys key)
        {
            if (currentState.IsKeyDown(key) && previousState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the current location of the mouse in the screen as a XNA Vector2.
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMouseLocation()
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static Vector2 GetTransformedMouseLocation()
        {
            return Vector2.Transform(GetMouseLocation(), CameraManager.Instance.CurrentCamera.BackwardsTransform);
        }

        /// <summary>
        /// Get the current location of the mouse in the screen as a .NET point.
        /// </summary>
        /// <returns></returns>
        public static Point GetMousePoint()
        {
 
            return new Point(mouseState.X, mouseState.Y);
        }

        public static Vector2 GetDeltaMouse()
        {


            Vector2 curr = new Vector2(mouseState.X, mouseState.Y);
            Vector2 prev = new Vector2(previousMouseState.X, previousMouseState.Y);
            //Console.WriteLine(curr + " , " + prev);
            return (prev - curr);
        }

        /// <summary>
        /// Returns true if the right mouse button is currently pressed.
        /// </summary>
        /// <returns></returns>
        public static bool IsMouseButtonDown(MouseButton button)
        {

            switch (button)
            {
                case (MouseButton.Left):
                    return mouseState.LeftButton == ButtonState.Pressed;
                case (MouseButton.Right):
                    return mouseState.RightButton == ButtonState.Pressed;
                case (MouseButton.Middle):
                    return mouseState.MiddleButton == ButtonState.Pressed;
                default:
#if DEBUG
                    throw new Exception("Somehow managed to have an invalid mouse button");
#else
                    return false;
#endif
            }


        }

        public static bool IsMouseButtonUp(MouseButton button)
        {
            switch (button)
            {
                case (MouseButton.Left):
                    return mouseState.LeftButton == ButtonState.Released;
                case (MouseButton.Right):
                    return mouseState.RightButton == ButtonState.Released;
                case (MouseButton.Middle):
                    return mouseState.MiddleButton == ButtonState.Released;
                default:
#if DEBUG
                    throw new Exception("Somehow managed to have an invalid mouse button");
#else
                    return false;
#endif
            }
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {

            switch (button)
            {
                case (MouseButton.Left):
                    return (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released);
                case (MouseButton.Right):
                    return (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released);
                case (MouseButton.Middle):
                    return (mouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released);
                default:
#if DEBUG
                    throw new Exception("Somehow managed to have an invalid mouse button");
#else
                    return false;
#endif
            }
        }

        public static bool IsMouseButtonReleased(MouseButton button)
        {

            switch (button)
            {
                case (MouseButton.Left):
                    return (mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed);
                case (MouseButton.Right):
                    return (mouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed);
                case (MouseButton.Middle):
                    return (mouseState.MiddleButton == ButtonState.Released && previousMouseState.MiddleButton == ButtonState.Pressed);
                default:
#if DEBUG
                    throw new Exception("Somehow managed to have an invalid mouse button");
#else
                    return false;
#endif
            }
        }

        public static int GetMouseWheelValue()
        {
            int val = mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;

            return val;
        }
    }
}
