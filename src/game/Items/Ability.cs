using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;


namespace Mechima
{
    
    public abstract class Ability : Item
    {
        public abstract Dictionary<ActionType, Action> Abilities { get; }

        protected enum State {Ready, Active, CoolDown}
        protected State CurrentState { get; set; }

        public void Enable()
        {
            if (ParentCreature == null) return;
            
            ParentCreature.SetControl(Abilities.ToList());
        }
    }
}
