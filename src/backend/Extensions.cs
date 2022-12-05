using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    /// <summary>
    /// Container for various extension methods
    /// </summary>
    public static class Extensions
    {
        //Gets the angle in radians with 0 radians being directly upwards on screen.
        public static float GetAngleFromMouse(this Vector2 point)
        {
            Vector2 displacement = Mouse.GetState().Position.ToVector2() - point;


            return MathF.Atan2(displacement.Y, displacement.X);
        }


        public static Vector2 NormalizeToMagnitude(this Vector2 vector, float magnitude)
        {
            return Vector2.Normalize(vector) * magnitude;
        }

        public static float ToAngle(this Vector2 vector)
        {
            return MathF.Atan2(vector.Y, vector.X) - MathF.PI/2;
        }

        public static void RotateFromParent(this Item item)
        {
            item.Rotation = GameManager.lerpRotation(item.Rotation, (item.ParentCreature.WorldPosition - item.ParentCreature.TargetPosition.Invoke()).ToAngle(), item.lookSpeed) ;
            
        }
        
        public static void Delete(this Entity e)
        {
            GameManager.PoppedEntities.Add(e);
        }

    }
}
