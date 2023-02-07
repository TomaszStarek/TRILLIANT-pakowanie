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
    public partial class ConfirmProblemOccurrence : Form
    {
        public bool Confirmed { get; private set; } = false;

        private Label _labelStatusInfo;
        private Button _buttonWygenerujBarcode, _buttonDoneBox;
        private int _counterPiecesInBox;
        private Button _btnErrorOccursAccept;
        public ConfirmProblemOccurrence(Label labelStatusInfo, Button buttonWygenerujBarcode, Button btnErrorOccursAccept, Button buttonDoneBox, int CounterPiecesInBox)
        {
            InitializeComponent();
            _labelStatusInfo = labelStatusInfo;
            _buttonWygenerujBarcode = buttonWygenerujBarcode;
            _counterPiecesInBox = CounterPiecesInBox;
            _btnErrorOccursAccept = btnErrorOccursAccept;
            _buttonDoneBox = buttonDoneBox;
            listBox1.DataSource = ProblemsToReport.ListOfOccurredProblems;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Confirmed = true;
            if(_counterPiecesInBox == 300)
            {
                ChangeControl.UpdateControl(_labelStatusInfo, Color.HotPink, "Zeskanowano 300 sztuk i potwierdzono błędy,\nWygeneruj Barcody! \nPamiętaj żeby je potwierdzić po zeskanowaniu!", true);
                ChangeControl.UpdateControl(_buttonWygenerujBarcode, Color.DeepPink, "Wygeneruj Barkody!", true);
                ChangeControl.UpdateControl(_btnErrorOccursAccept, Color.Fuchsia, "Potwierdż wystąpienie błędów!", false);
                ChangeControl.UpdateControl(_buttonDoneBox, Color.DarkMagenta, "Zakończ pakowanie rolki!", true);
            }   
            else
                ChangeControl.UpdateControl(_labelStatusInfo, Color.Red, "Potwierdzono błędy, ale brak 300 sztuk w partii!", true);

            

            this.Hide();
        }
    }
}
