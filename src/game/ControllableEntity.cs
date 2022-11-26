using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public enum ActionType
    {
        Primary, Secondary, Action01, Action02, Action03, Action04, MoveUp, MoveDown, MoveRight, MoveLeft
    }
    public abstract class ControllableEntity: Entity, IHasStats, ICollidable
    {

        public Dictionary<string, float> Stats { get; set; } = new Dictionary<string, float>();
        public Dictionary<string, float> Modifiers { get; set; } = new Dictionary<string, float>();




        public virtual Dictionary<ActionType, Action> ActionMap { get; set; }

        public List<ActionType> QueuedActions { get; set; } = new List<ActionType>();

        public bool isControlled { get; set; }

        public void SetControl(ActionType actionType, Action mappedAction)
        {
            ActionMap[actionType] = mappedAction;
        }

        public void SetControl(List<KeyValuePair<ActionType, Action>> actions)
        {
            foreach (KeyValuePair<ActionType, Action> action in actions)
            {
                ActionMap[action.Key] = action.Value;
            }
        }

        public void QueueAction(ActionType actionType)
        {
            if (this.ActionMap.ContainsKey(actionType))
                this.QueuedActions.Add(actionType);
        }

        public void InvokeQueuedActions()
        {
            foreach (ActionType action in QueuedActions)
            {
                this.ActionMap[action].Invoke();
            }
            this.QueuedActions.Clear();
        }



        public override void Update(GameTime gameTime)
        {
            this.Modifiers.Clear();
            InvokeQueuedActions(); //sussy way of referencing default method in IControllable
            base.Update(gameTime);

        }

    }
}
