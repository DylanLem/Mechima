using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public class Scene
    {
        

        public List<NPCBrain> NPCs = new List<NPCBrain>();
        public List<Entity> Entities = new List<Entity>();

        public List<Entity> QueuedEntities = new List<Entity>();
        public List<Entity> PoppedEntities = new List<Entity>();

        public void Load()
        {
            // The rest of this region is just me initializing game stuff. Eventually this will be possible through dev console / menus.

            Bow bow = (Bow)AddEntity(new Bow());
            bow.SetSprite("bow-sheet", true);

            //Sword sword = (Sword)AddEntity(new Sword());
            //sword.SetSprite("sword");

            Thruster thruster = (Thruster)AddEntity(new Thruster());

            Creature mech = (Creature)AddEntity(new Creature());
            mech.SetSprite("knight-sheet", true);

            mech.EquipItem(bow);
            mech.EquipItem(thruster);
            //mech.EquipItem(sword);

            GameManager._player.TakeControl(mech);
            GameManager._player.controlledEntity.WorldPosition = new Vector2(100, 100);



            Random rand = new Random();
            for (int i = 0; i < 15; i++)
            {
                //Thruster t = (Thruster)AddEntity(new Thruster());
                //t["acceleration"] = 50f;
                Sword s = (Sword)AddEntity(new Sword());
                s.SetSprite("sword");
                List<Item> items = new List<Item>()
                {
                    s
                };



                NPCBrain brain = Generator.CreateNPC("headsheet", items, "kill", true);
                brain.Vessel.WorldPosition = new Vector2(rand.Next(200, 1000), rand.Next(200, 600));

                //Init over
            }
        }

        public void Update()
        {
            foreach (Entity e in PoppedEntities)
            {
                Entities.Remove(e);

            }


            foreach (Entity e in Entities)
                e.Update();

            foreach (NPCBrain npc in NPCs)
                npc.Update();


            foreach (Entity e in QueuedEntities)
                Entities.Add(e);


            PoppedEntities.Clear();
            QueuedEntities.Clear();

        }



        public List<ICollidable> GetCollidables()
        {
            List<ICollidable> collidables = new List<ICollidable>();

            foreach (Entity e in Entities)
                if (e is ICollidable collidable)
                    collidables.Add(collidable);

            return collidables;
        }

        public Entity AddEntity(Entity entity)
        {
            QueuedEntities.Add(entity);
            return entity;
        }

    }
}
