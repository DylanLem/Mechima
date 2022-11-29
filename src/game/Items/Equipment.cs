using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;


namespace Mechima
{
    
    public abstract class Equipment : Item
    {
        public abstract Dictionary<ActionType, Action> Abilities { get; }

       

        public void Enable()
        {
            if (Parent == null) return;

            Parent.SetControl(Abilities.ToList<KeyValuePair<ActionType, Action>>());
        }
    }
}
