using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    /// <summary>
    /// Tracks meta-information connecting player to their character in-game. Players pair to a ControllableEntity, as well as a ControlScheme.
    /// </summary>
    public class Player
    {
        public ControllableEntity controlledEntity;

        private Texture2D cursor;

        public ControlScheme Controls { get; private set; } = new ControlScheme();

        public bool AllowControl = true;

        public Player()
        {
            cursor = DisplayManager.spriteMap["crosshair"];
            
            Mouse.SetCursor(MouseCursor.FromTexture2D(cursor, cursor.Width / 2, cursor.Height / 2));
            
        }


        //Grants the player control of a given entity
        public void TakeControl(Creature entity)
        {
            if (controlledEntity != null) controlledEntity.isControlled = false;

            entity.isControlled = true;
            entity.TargetPosition = (() => Mouse.GetState().Position.ToVector2());
            this.controlledEntity = entity;
        }


        public void Update()
        {
            List<ActionType> inputActions = controls.GetActions();

            //inputActions.Sort(); //unfortunately i have to sort this list because of stupid things like moving before an action that modifies movespeed is invoked

            if(AllowControl && controlledEntity != null)
            {
                
                foreach (ActionType action in inputActions)
                    controlledEntity.QueueAction(action);
            }
            
                
            
        }
    }
}
