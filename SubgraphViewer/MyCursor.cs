using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SubgraphViewer
{
    class MyCursor
    {
        public static Cursor EraserCursor
        {
            get { return GetEraserCursor(); }
        }
        private static Cursor GetEraserCursor()
        {
            Image fn = ImageResources.GetImageByName("eraser_cursor");
            Bitmap bitmap = new Bitmap(fn);
            IntPtr handle = bitmap.GetHicon();
            Cursor myCursor = new Cursor(handle);
            return myCursor;
        }
    }
}
