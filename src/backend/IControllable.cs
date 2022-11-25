using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public enum ActionType 
    {
        Primary,Secondary,Action01,Action02,Action03,Action04, MoveUp, MoveDown, MoveRight, MoveLeft
    }

    public interface IControllable
    {
        Dictionary<ActionType, Action> ActionMap {get;set;}

        List<ActionType> QueuedActions { get; set; }

        bool isControlled { get; set; }

        void SetControl(ActionType actionType, Action mappedAction) {
            ActionMap[actionType] = mappedAction;
        }

        void SetControl(List<KeyValuePair<ActionType,Action>> actions)
        {
            foreach(KeyValuePair<ActionType,Action> action in actions)
            {
                ActionMap[action.Key] = action.Value;
            }
        }

        void QueueAction(ActionType actionType)
        {
            if (this.ActionMap.ContainsKey(actionType))
                this.QueuedActions.Add(actionType);
        }

        void InvokeQueuedActions()
        {
            foreach(ActionType action in QueuedActions)
            {
                this.ActionMap[action].Invoke();
            }
            this.QueuedActions.Clear();
        }
    }
}
