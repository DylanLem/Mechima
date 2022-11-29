﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    public static class GameManager
    {
        public static List<Camera> Cameras = new List<Camera>();

        public static Camera MainCam { get => Cameras[0]; }

        private static bool _initialized = false;

        public static Player _player;
        public static List<Entity> Entities = new List<Entity>();

        public static float lastTick = 0.01f;

        public static bool isPaused;
        public static float pauseTimer = 0;
        public static float pauseCool = 1f;


        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;


            _player = new Player();


            Cameras.Add(new Camera());

            MainCam.ScreenDimensions = new Rectangle(0, 0, (int)DisplayManager.Resolution.X / 2, (int)DisplayManager.Resolution.Y/2) ;
            MainCam.Viewport = new Rectangle(0, 0, 250, 150);


            DisplayManager.SetScreenResolution((int)DisplayManager.Resolution.X, (int)DisplayManager.Resolution.Y);


            Sword sword = (Sword)AddEntity(new Sword());
            sword.SetSprite("sword");

            Thruster thruster = (Thruster)AddEntity(new Thruster()); 

            Creature mech = (Creature)AddEntity(new Creature());
            mech.SetSprite("knight-sheet", true);

            mech.EquipItem(sword,"Primary");
            mech.EquipItem(thruster, "Propulsion");

            _player.controlledEntity = mech;
            _player.controlledEntity.WorldPosition = new Vector2(0, 0);
            

            TestDummy dummy = (TestDummy)AddEntity(new TestDummy());
            dummy.SetSprite("testdummy");
            dummy.WorldPosition = new Vector2(400,400);
        }


        public static void Update(GameTime gameTime)
        {
            lastTick = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(lastTick > 0)
                DisplayManager.RequestBlit(new BlitRequest("fps: " + ((float)(1/lastTick)).ToString(), Color.White, Vector2.Zero, AnchorPoint.TopLeft));

            
            pauseTimer += lastTick;


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                TogglePause(!isPaused);
            

            if (isPaused)
            {
                DisplayManager.RequestBlit(new BlitRequest("GAME PAUSED", Color.Black, DisplayManager.Resolution / 2, AnchorPoint.Center, MathF.PI, 4));
                return;
            }



            
            foreach (Entity e in Entities)
                e.Update(gameTime);

            _player.Update();

           
        }


        public static List<ICollidable> GetCollidables()
        {
            List<ICollidable> collidables = new List<ICollidable>();

            foreach (Entity e in Entities)
                if (e is ICollidable collidable)
                    collidables.Add(collidable);

            return collidables;
        }

        public static void Quit()
        {

        }

        public static void TogglePause(bool toggle)
        {
            if (pauseTimer < pauseCool) return;
                
            isPaused = toggle;
            pauseTimer = 0;
        }

        public static Entity AddEntity(Entity entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public static bool TryMove(Entity entity, Vector2 Position)
        {
            entity.WorldPosition = Position;
            return true;
        }

        //Gets the angle in radians with 0 radians being directly upwards on screen.
        public static float GetAngleFromMouse(this Vector2 point)
        {
            Vector2 displacement = Mouse.GetState().Position.ToVector2() - point;


            return MathF.Atan2(displacement.Y, displacement.X) ;
        }


        //Makes a vector where 0rad is directly upwards
        public static Vector2 MakeVector(float angle, float magnitude)
        {
            angle -= MathF.PI / 2;
            return new Vector2(magnitude * MathF.Cos(angle), magnitude * MathF.Sin(angle));
        }


        //idk apparently this lerps rotation i didnt make this
        public static float lerpRotation(float a, float b, float x)
        {

            float C = ((1 - x) * MathF.Cos(a)) + (x * MathF.Cos(b));
            float S = ((1 - x) * MathF.Sin(a)) + (x * MathF.Sin(b));

            return MathF.Atan2(S,C);
        }

        public static Vector2 NormalizeToMagnitude(this Vector2 vector, float magnitude)
        {
            return Vector2.Normalize(vector) * magnitude;
        }

    }
}
