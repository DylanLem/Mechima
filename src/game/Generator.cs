using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public static class Generator
    {

        public static NPCBrain CreateNPC(string sprite, List<Item> items, string goal, bool isEnemy)
        {
            NPCBrain brain = new NPCBrain();

            Creature vessel = (Creature)GameManager.AddEntity(new Creature());
            
            brain.Vessel = vessel;

            vessel.SetSprite(sprite, true);

            foreach (Item item in items)
                vessel.EquipItem(item);

            if (isEnemy)
                brain.Target = GameManager._player.controlledEntity;

            vessel.TargetPosition = (() => brain.Target.GetPosition());

            brain.GenerateGoal(goal);

            GameManager.NPCs.Add(brain);

            return brain;

        }
    }
}
