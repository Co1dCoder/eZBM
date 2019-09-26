using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MsdiTransportation.SubgradeComponent;
using MsdiTransportation.SubgradeTemplateLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
           // Application.Run(new EnumsAndComboBox());
            // Application.Run(new Form1());
            test();
        }

        public static void test()
        {
            // HighwayConfigurations.TestSetAlignmentStandards();
            var f = new FormSubgradeConstructor();
            f.ShowDialog();
        }
    }
}
