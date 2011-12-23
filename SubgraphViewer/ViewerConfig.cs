using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Trinity.Configuration;
using Trinity.Networking;

namespace SubgraphViewer
{
    class ViewerConfig
    {
        public static int QueryPanelHeight = 550;
        public static int QueryPanleWidth = 600;
        public static float LinePenWidth = 2;
        public static double NodeRadius = 17;
        public static int ToolItemSize = 26;
        public static int LeftPanelWidth = 40;
        public static int BottomPanelHeight = 160;
        public static int FontSize = 15;
        public static string FontName = "黑体";
        public static Color BrushColor = Color.Black;

        public static int QueryResultWidth = 800;
        public static int QueryResultHeight = 500;
        public static int QueryResultMargin = 50;

        public static int CellPropertyWidth = 300;
        public static int CellPropertyHeight = 400;

        public static int SplashFormWidth = 400;
        public static int SplashFormHeight = 20;

        public static int MouseEraserWidth = 8;
        public static int MouseEraserHeight = 5;

        public static List<string> HostNameList;

        public static void ConfigureServer()
        {
            try
            {
                HostNameList = null;
                TrinityConfig.CurrentRunningMode = RunningMode.Client;
                HostNameList = BlackboardClient.HostNameList;
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }
    }

}
