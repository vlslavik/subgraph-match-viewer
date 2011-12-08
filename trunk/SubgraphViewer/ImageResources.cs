using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SubgraphViewer
{
    class ImageResources
    {
        public static Image GetImageByName(string name)
        {
            string path = System.Environment.CurrentDirectory + "\\images\\" + name + ".jpg";
            return Image.FromFile(path);
        }
    }
}
