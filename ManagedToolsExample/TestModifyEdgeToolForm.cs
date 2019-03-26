/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/Elements/ManagedToolsExample/TestModifyEdgeToolForm.cs $
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagedToolsExample
{
public partial class TestModifyEdgeToolForm : Form
    {
    private static  bool allowTangentEdges;
    private static  double blendRadius;
    public TestModifyEdgeToolForm ()
    {
    InitializeComponent();
    }

    private void chkBoxAllowTangentEdge_CheckedChanged (object sender, EventArgs eventArgs)
    {
    allowTangentEdges = chkBoxAllowTangentEdge.Checked;
    }

    private void txtBlendRadius_KeyPress (object sender, KeyPressEventArgs eventArgs)
    {
           
    if ( !char.IsControl(eventArgs.KeyChar) && !char.IsDigit(eventArgs.KeyChar) &&  (eventArgs.KeyChar != '.') )
        {
        eventArgs.Handled = true;
        }

        // only allow one decimal point
    if ( (eventArgs.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1) )
        {
        eventArgs.Handled = true;
        }
            
    }

    public static double GetBlendRadius ()
        {
        return blendRadius;
        }
    public static bool GetTangencyOption ()
        {
        return allowTangentEdges;
        }

    private void txtBlendRadius_TextChanged (object sender, EventArgs e)
        {
        blendRadius = Convert.ToDouble(txtBlendRadius.Text);
        }
    }
}
