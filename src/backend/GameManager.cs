using System;
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

        public static float effectTime { get => lastTick * 1.1f; }
        public static float effectTimer { get; set; } = 0f;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;


            _player = new Player();


            Cameras.Add(new Camera());

            MainCam.ScreenDimensions = new Rectangle(0, 0, (int)DisplayManager.Resolution.X, (int)DisplayManager.Resolution.Y);
            MainCam.Viewport = new Rectangle(0, 0, 250, 150);


            DisplayManager.SetScreenResolution((int)DisplayManager.Resolution.X, (int)DisplayManager.Resolution.Y);


            Sword sword = (Sword)AddEntity(new Sword(DisplayManager.spriteMap["sword"]));

            Mech mech = (Mech)AddEntity(new Mech(DisplayManager.spriteMap["headsheet"]));
            mech.EquipItem(sword,"Primary");


            _player.controlledEntity = mech;
            _player.controlledEntity.WorldPosition = new Vector2(0, 0);
            _player.controlledEntity.CellSize = new Vector2(8,8);
            _player.controlledEntity.Animated = true;

        }


        public static void Update(GameTime gameTime)
        {
            lastTick = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DisplayManager.RequestBlit(new BlitRequest(lastTick.ToString(), Color.White, Vector2.Zero));

            effectTimer += lastTick;
            pauseTimer += lastTick;


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                TogglePause(!isPaused);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ContentLoader.LoadAnimation("hihi");

            if (isPaused)
            {
                DisplayManager.RequestBlit(new BlitRequest("GAME PAUSED", Color.Black, DisplayManager.Resolution / 2));
                return;
            }



            _player.Update();
            foreach (Entity e in Entities)
                e.Update(gameTime);

            if (effectTimer >= effectTime)
                effectTimer = 0;
           
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
        public static float GetAngleFromMouse(Vector2 point)
        {
            Vector2 displacement = Mouse.GetState().Position.ToVector2() - point;


            return MathF.Atan2(displacement.X, -displacement.Y) ;
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
    }
}
