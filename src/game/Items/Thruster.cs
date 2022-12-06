using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    class Thruster: Ability
    {
        public float maxThrust = 150f;

        public override Dictionary<ActionType, Action> Abilities
        {
            get => new Dictionary<ActionType, Action>()
        {
            { ActionType.MoveDown, () => this.Activate(new Vector2(0, 1)) },
            { ActionType.MoveLeft, () => this.Activate(new Vector2(-1, 0)) },
            { ActionType.MoveRight, () => this.Activate(new Vector2(1, 0)) },
            { ActionType.MoveUp, () => this.Activate(new Vector2(0, -1)) }
        };
        }

        public override List<ItemTag> Tags { get => new List<ItemTag>() { ItemTag.Movement }; }

        public Thruster()
        {
            this["acceleration"] = 800.0f;
        }

        public void Activate(Vector2 direction)
        {
            
            Vector2 force = this["acceleration"] * direction * GameManager.lastTick;
            

            Vector2 expectedV = ParentCreature.Velocity + force;
            Vector2 deltaV = force;

            //float differential = maxThrust - expectedV.Length();

            if (maxThrust - expectedV.Length() < 0)
            {
                Vector2 normalV = expectedV.NormalizeToMagnitude(maxThrust);
                deltaV = normalV - expectedV;

               // float normalMag = force.Length() + differential;
                //force = force.NormalizeToMagnitude(normalMag);
                
            }

            ParentCreature.ForceVector += deltaV;
        }

        
    }
}
