using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;


namespace Mechima
{
    public static class ContentLoader
    {
        public static AnimData LoadAnimation(string spriteName)
        {
            string path = Directory.GetCurrentDirectory() + "\\Content\\anims\\" +  spriteName + ".anim";

            System.Diagnostics.Debug.WriteLine(path);

            if (!File.Exists(path)) return null;

            string[] file = File.ReadAllLines(path);

            string[] cellData = file[0].Split(',');
            string[] animData = file[1].Split(',');


            AnimData anim = new AnimData()
            {
                TextureSize = new Vector2(DisplayManager.spriteMap[spriteName].Width, DisplayManager.spriteMap[spriteName].Height),
                CellSize = new Vector2(int.Parse(cellData[0]), int.Parse(cellData[1])),
                animSpeed = float.Parse(animData[0])
            };

            foreach(string line in file[2..])
            {
                string[] linedata = line.Split(':');
                AnimationState animType = (AnimationState)Enum.Parse(typeof(AnimationState), linedata[0]);
                int[] frames = Array.ConvertAll(linedata[1].Split(","), s => int.Parse(s));
                anim.AddAnimState(animType, frames.ToList<int>());
            }



            return anim;
        }
    }
}
