using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public enum ItemTag
    {
        Damage, Movement, Protection, Stun, Buff, Debuff, AoE, Heal
    }

    public enum DamageType
    {
        Swing, Thrust, Projectile, Electric, Fire, Ice,
    }

    public enum NPCAction
    {
        Move, Attack
    }

    public enum AnimationState
    {
        Default, Idle, Attack, MoveUp, MoveDown, MoveLeft, MoveRight, Dying, Hurt, Charging, Discharging
    }
}
