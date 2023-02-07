using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class ListOfScannedSn : Form
    {

        public ListOfScannedSn()
        {
            InitializeComponent();

            List<string> list = new List<string>() ;
            List<string> list2 = new List<string>() ;

            list.AddRange(BoxToPackaut.ListOfScannedBarcodesVerified);
            list2.AddRange(BoxToPackaut.ListOfScannedBarcodesPacked);
            list.Reverse();
            list2.Reverse();

            listBox1.DataSource = list;
            listBox2.DataSource = list2;
            textBox1.Select();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var snToFind = textBox1.Text.ToUpper();


                int curIndex = listBox1.Items.IndexOf(snToFind);
                if (curIndex >= 0)
                {
                    listBox1.SetSelected(curIndex, true);
                }               
                else
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, $"Numer nie znaleziony: {snToFind} ");

                int curIndex2 = listBox2.Items.IndexOf(snToFind);
                if (curIndex2 >= 0)
                {
                    listBox2.SetSelected(curIndex2, true);
                }

            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                var snToFind = textBox2.Text.ToUpper();


                int curIndex = listBox1.Items.IndexOf(snToFind);
                if (curIndex >= 0)
                {
                    listBox1.SetSelected(curIndex, true);
                }


                int curIndex2 = listBox2.Items.IndexOf(snToFind);
                if (curIndex2 >= 0)
                {
                    listBox2.SetSelected(curIndex2, true);
                }
                else
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, $"Numer nie znaleziony: {snToFind} ");

            }
        }
    }
}
