using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public class SpriteException: Exception
    {
        
        public SpriteException(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
