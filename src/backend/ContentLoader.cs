using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Mechima
{
    public static class ContentLoader
    {
        public static AnimData LoadAnimation(string animName)
        {
            string path = Directory.GetCurrentDirectory() + "\\" +  animName + ".anim";

            if (!File.Exists(path)) return null;
            
            System.Diagnostics.Debug.WriteLine(path);

            return null;
        }
    }
}
