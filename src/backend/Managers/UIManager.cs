using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public static class UIManager
    {
        public static List<UINode> RootNodes = new List<UINode>();
        public static Dictionary<string, UINode> nodeMap = new Dictionary<string, UINode>();

        public static UINode LoadUINode(string menu)
        {
            UINode ui = new UINode();
            return ui;

        }



    }
}
