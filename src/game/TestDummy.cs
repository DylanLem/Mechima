using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public class TestDummy: ControllableEntity, ICollidable
    {
        public Circle Collider
        {
            get => new Circle()
            {
                Position = this.WorldPosition,
                Radius = (Scale.X * (float)this.SpriteCell.Width / 2)
            };
        }

        public TestDummy()
        {
            Scale = Vector2.One * 2;
        }

        public bool CheckCollision(ICollidable collidable)
        {
            return true;
        }
    }


}
