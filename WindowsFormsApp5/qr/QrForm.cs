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
    public partial class QrForm : Form
    {
        private Label _label;
        private Button _button;
        private bool _isItFirstPart;
        public QrForm(string barcode, bool IsItFirstPart, Label label, Button button)
        {
            InitializeComponent();
            this.BringToFront();
            _label = label;
            _button = button;
            _isItFirstPart = IsItFirstPart;
            SetQrCodeToImageBox(barcode);
            SetLabelQrPartNumber(IsItFirstPart);

        }
        public bool Confirmed { get; private set; } = false;
        public void SetQrCodeToImageBox(string barcode)
        {
            var qr = new QrCodes();
            pictureBox1.Image = qr.Generate(barcode);
            //  qr.Readchachacha();
        }
        public void SetLabelQrPartNumber(bool isItFirstPart)
        {
            if (isItFirstPart)
                labelQrPartNumber.Text = "Część 1/2";
            else
                labelQrPartNumber.Text = "Część 2/2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Confirmed = true;

            if (!_isItFirstPart)
            {
                ChangeControl.UpdateControl(_label, Color.LawnGreen, "Zaakceptowano 2. część barkodu!", true);
                ChangeControl.UpdateControl(_button, Color.Green, "Sprawdź numery w MES", true);
            }
            else
                ChangeControl.UpdateControl(_label, Color.LawnGreen, "Zaakceptowano 1. część barkodu!", true);

            this.Hide();
        }


    }
}
