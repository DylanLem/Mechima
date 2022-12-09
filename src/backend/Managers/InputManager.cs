using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    public static class InputManager
    {
        public static event InputNotifier MouseMoved, MouseLeftDown, MouseLeftUp, MouseRightDown, MouseRightUp;


        private static bool scrollwheel { get  {
                int state = Mouse.GetState().ScrollWheelValue;
                int val =  state - lastScrollWheel;
                lastScrollWheel = state;
                return val>0;
            } }

        private static int lastScrollWheel = 0;

        /// <summary>
        /// Checks the state of all inputs in the control map and creates a list of ActionType for game logic.
        /// </summary>
        /// <returns></returns>
        public static List<ActionType> GetActions(ControlScheme controls)
        {
            List<ActionType> actions = new List<ActionType>();

            foreach(Keys key in controls.kbControlMap.Keys)
                if (Keyboard.GetState().IsKeyDown(key))
                    actions.Add(controls.kbControlMap[key]);

            MouseState mouse = Mouse.GetState();


            if (mouse.LeftButton == ButtonState.Pressed)
            {
                actions.Add(controls.mouseControlMap["leftClick"]);
                controls.leftIsClicked = true;
            }

            else
                if (controls.leftIsClicked)
                {
                    actions.Add(controls.mouseControlMap["leftClickUp"]);
                    controls.leftIsClicked = false;
                }


            if (mouse.RightButton == ButtonState.Pressed)
                actions.Add(controls.mouseControlMap["rightClick"]);



            if (scrollwheel)
            {
                System.Diagnostics.Debug.WriteLine("scrollling up");
            }

            return actions;
        }
    }
}
