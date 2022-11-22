using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    public class ControlScheme
    {
        public Dictionary<Keys, ActionType> kbControlMap;
        public Dictionary<string, ActionType> mouseControlMap;

        


        private readonly Dictionary<Keys, ActionType> kbDefaultControls = new Dictionary<Keys, ActionType>()
        {
            {Keys.S, ActionType.MoveDown },
            {Keys.A, ActionType.MoveLeft },
            {Keys.D, ActionType.MoveRight },
            {Keys.W, ActionType.MoveUp},
            {Keys.J, ActionType.Primary},
            {Keys.K, ActionType.Secondary },
            
        };

        private readonly Dictionary<string, ActionType> mouseDefaultControls = new Dictionary<string, ActionType>()
        {
            {"leftClick", ActionType.Primary},
            {"rightClick", ActionType.Secondary}
        };

        public ControlScheme()
        {
            this.kbControlMap = kbDefaultControls;
            this.mouseControlMap = mouseDefaultControls;
        }

        private Keys[] ConvertToKeys()
        {
            Keys[] f = new Keys[kbControlMap.Count];
            kbControlMap.Keys.CopyTo(f, 0);

            return f;
        }

        public List<ActionType> GetActions()
        {
            List<ActionType> actions = new List<ActionType>();

            foreach(Keys key in kbControlMap.Keys)
                if (Keyboard.GetState().IsKeyDown(key))
                    actions.Add(kbControlMap[key]);

            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
                actions.Add(mouseControlMap["leftClick"]);
            if (mouse.RightButton == ButtonState.Pressed)
                actions.Add(mouseControlMap["rightClick"]);

            return actions;
        }
    }
}
