using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    class Thruster: Equipment
    {
        public float maxThrust = 550f;

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

        public Thruster()
        {
            this["acceleration"] = 250.0f;
        }

        public void Activate(Vector2 direction)
        {
            Vector2 force = this["acceleration"] * direction * GameManager.lastTick;
            

            Vector2 expectedV = Parent.Velocity + force;

            float differential = maxThrust - expectedV.Length();

            if(differential < 0)
            {
                float normalMag = force.Length() + differential;
                force = force.NormalizeToMagnitude(normalMag);
            }

            Parent.ForceVector += force;
        }
    }
}
