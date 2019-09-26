/*
MDL LOAD "D:\GithubProjects\eZBM\bin\eZBMCE.dll"
*/

using System.Windows.Forms;
using eZBMCE.Cif;
using MSDIAddin;

namespace eZBMCE.AddinInitialize
{
    /// <summary>
    /// Class used for running key-ins. The key-ins
    /// XML file commands.xml provides the class name and the method names.
    /// </summary>
    internal sealed class Keyin
    {
        /// <summary> CommandKeyin </summary>
        /// <param name="unparsed"></param>    
        public static void CommandKeyin(string unparsed)
        {
            MessageBox.Show(@"KeyinTest");
            MSDIAddinClass.MstnControlDemo(null);
        }

        /// <summary> CommandKeyin </summary>
        /// <param name="unparsed"></param>    
        public static void Test_Cif(string unparsed)
        {
           CifTest.Test_Cif();
        }
    }
}
