using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Creature : ControllableEntity, ICollidable
    {
        public List<Item> Items = new List<Item>();

        public Func<Vector2> TargetPosition;
        
        public Circle Collider
        {
            get => new Circle()
            {
                Position = this.WorldPosition,
                Radius = (Scale.X * (float)this.SpriteCell.Width / 2)
            };
        }




    public Creature() 
        {
            Scale = new Vector2(2f, 2f);

            this.ActionMap = new Dictionary<ActionType, Action>()
            {
                {ActionType.MoveDown, null},
                {ActionType.MoveLeft, null},
                {ActionType.MoveRight, null},
                {ActionType.MoveUp, null},
                {ActionType.Primary, null},
                {ActionType.Secondary, null}      
            };
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);



           


            this.Color = Color.White;
        }



        public void EquipItem(Item item)
        {
            Items.Add(item);
            item.SetParent(this);
            if (item is Ability ability) {
                
                ability.Enable();
            }
            
        }
        
        public bool CheckCollision(ICollidable collidable)
        {
            return false;
        }

        public float GetAttribute(string attribute)
        {
            return 1f;
        }
    }
}
