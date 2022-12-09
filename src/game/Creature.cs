using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    /// <summary>
    /// Main class for any controllable entitiy that can be equipped with abilities
    /// In-game creatures/monsters/playable characters will all be instances of Creature
    /// 
    /// A creature with no abilities will be capable of nothing
    /// 
    /// 
    /// at the moment there are no non-Creature ControllableEntity classes, but the possibility exists.
    /// </summary>
    public sealed class Creature : ControllableEntity, ICollidable
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

            this["drag"] = 0.999f;

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

            Graphics.DrawCirclePrimitive(Color.Red, this.Collider.Position, this.Collider.Radius);

            foreach (ICollidable collidable in GameManager.GetCollidables())
            {
                if (collidable == this || (collidable is Item item && Items.Contains(item)))
                    continue;

                if(CheckCollision(collidable))
                    if(collidable is Creature c)
                    {
                        float overlap = Vector2.Distance(this.Collider.Position, collidable.Collider.Position) - (this.Collider.Radius + collidable.Collider.Radius);
                        Vector2 v = GameManager.MakeVector((this.WorldPosition - c.WorldPosition).ToAngle(), overlap);
                        
                        this.ForceVector += v;
                    }
            }

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
            if (Vector2.Distance(this.Collider.Position, collidable.Collider.Position) < this.Collider.Radius + collidable.Collider.Radius)
                return true;
            return false;
        }

        public float GetAttribute(string attribute)
        {
            return 1f;
        }
    }
}
