using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{

    /// <summary>
    /// Maps player input to commands understood by the game. 
    /// 
    /// System flow is this: 
    /// ControlScheme {input->ActionType}
    /// Player {ControlScheme -> ControllableEntity}
    /// ControllableEntity {ActionType->Action}
    /// </summary>
    public class ControlScheme
    {
        public Dictionary<Keys, ActionType> kbControlMap;
        public Dictionary<string, ActionType> mouseControlMap;

        private bool leftIsClicked;

        //This can give an idea of the intention behind the data structure.
        //Keyboard inputs map to ActionType enums. Actiontypes will correspond to programmed actions in-game
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
            {"leftClickUp", ActionType.Action04 },
            {"rightClick", ActionType.Secondary}
        };

        public ControlScheme()
        {
            this.kbControlMap = kbDefaultControls;
            this.mouseControlMap = mouseDefaultControls;
        }


        /// <summary>
        /// Checks the state of all inputs in the control map and creates a list of ActionType for game logic.
        /// </summary>
        /// <returns></returns>
        public List<ActionType> GetActions()
        {
            List<ActionType> actions = new List<ActionType>();

            foreach(Keys key in kbControlMap.Keys)
                if (Keyboard.GetState().IsKeyDown(key))
                    actions.Add(kbControlMap[key]);

            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                actions.Add(mouseControlMap["leftClick"]);
                leftIsClicked = true;
            }
            else
            {
                if (leftIsClicked)
                {
                    actions.Add(mouseControlMap["leftClickUp"]);
                    leftIsClicked = false;
                }
                    
                
            }
                
            if (mouse.RightButton == ButtonState.Pressed)
                actions.Add(mouseControlMap["rightClick"]);

            return actions;
        }
    }
}
