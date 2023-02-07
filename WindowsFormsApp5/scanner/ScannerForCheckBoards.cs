using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public class ScannerForCheckBoards : Scanner
    {
        private Label _labelScanOut, _labelCountPiecesToBox, _labelStatusInfo;
        private Button _buttonWygenerujBarcode, _BtnErrorOccursAccept;
        private string _lineReadIn = "";
        private string _poprzedniBarcode;
        public int _counterPiecesInBox = 0;

        public bool ScanerBlocked { get; set; } = false;

        public ScannerForCheckBoards(string com, TextBox textBox, Label labelScanOut, Label labelCountPiecesToBox,
            Label labelStatusInfo, Button buttonWygenerujBarcode, Button BtnErrorOccursAccept) : base(com, textBox)
        {
            _BtnErrorOccursAccept = BtnErrorOccursAccept;
            _buttonWygenerujBarcode = buttonWygenerujBarcode;
            _labelStatusInfo = labelStatusInfo;
            _labelCountPiecesToBox = labelCountPiecesToBox;
            _labelScanOut = labelScanOut;

         //   _com = com;
        }
        public override void port_DataReceived(object sender, SerialDataReceivedEventArgs rcvdData)
        {
            

            while (Port.BytesToRead > 0)
            {
                _lineReadIn += Port.ReadExisting();
                Thread.Sleep(25);
            }


            var res = MethodForVerifySnFromScanner.DataReceivedOperations(ref _lineReadIn, ref _poprzedniBarcode, ref _counterPiecesInBox, _labelScanOut,
                        _labelCountPiecesToBox, _labelStatusInfo, _buttonWygenerujBarcode, _BtnErrorOccursAccept);

            if (res.Length > 1 && !ScanerBlocked)
                displayTextReadIn(_lineReadIn);

            //  _poprzedniBarcode = lineReadIn;
            _lineReadIn = string.Empty;


        }

    }
}

