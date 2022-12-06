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

        public bool leftIsClicked;

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


    }
}
