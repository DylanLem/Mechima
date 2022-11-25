﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    public class Player
    {
        public ControllableEntity controlledEntity;

        private Texture2D cursor;

        private ControlScheme controls = new ControlScheme();

        public Player()
        {
            cursor = DisplayManager.spriteMap["crosshair"];
            
            Mouse.SetCursor(MouseCursor.FromTexture2D(cursor, cursor.Width / 2, cursor.Height / 2));
            
        }


        //Grants the player control of a given entity
        public void TakeControl(ControllableEntity entity)
        {
            if (controlledEntity != null) controlledEntity.isControlled = false;

            entity.isControlled = true;
            this.controlledEntity = entity;
        }


        public void Update()
        {
            List<ActionType> inputActions = controls.GetActions();

            inputActions.Sort();

            foreach (ActionType action in inputActions)
            {
                
                controlledEntity.QueueAction(action);
            }
            
        }
    }
}
