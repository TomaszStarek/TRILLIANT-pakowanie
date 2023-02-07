using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class ChangeControl
    {

        public static bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }

        public static void UpdateControl(Control myControl, Color c, String s, bool widzialnosc)
        {
            //Check if invoke requied if so return - as i will be recalled in correct thread
            if (ControlInvokeRequired(myControl, () => UpdateControl(myControl, c, s, widzialnosc))) return;
            myControl.Text = s;
            myControl.BackColor = c;
            myControl.Visible = widzialnosc;
        }
        public static void UpdateControl(Control myControl, String s, bool widzialnosc)
        {
            //Check if invoke requied if so return - as i will be recalled in correct thread
            if (ControlInvokeRequired(myControl, () => UpdateControl(myControl, s, widzialnosc))) return;
            myControl.Text = s;
            myControl.Visible = widzialnosc;
        }

    }
}
