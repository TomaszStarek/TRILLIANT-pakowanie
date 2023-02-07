using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    internal class ScannerForPackout : Scanner
    {
        private Label _labelScanOut2;
        private Label _labelCountVerifiedPieces;
        private Label _labelScanInfoPAK;
        private Label _labelStatusInfo;
        private Button _buttonWygenerujBarcode;

        private string _lineReadIn = "";
        private string _poprzedniBarcode;
        public int _counterPiecesPacked = 0;
      //  public bool ScanOK { get; set; } = false;
        public ScannerForPackout(string com, TextBox textBox, Label labelScanOut2, Label labelCountVerifiedPieces, Label labelScanInfoPAK,
            Label labelStatusInfo, Button buttonWygenerujBarcode) : base(com, textBox)
        {
            _labelScanOut2 = labelScanOut2;
            _labelCountVerifiedPieces = labelCountVerifiedPieces;
            _labelScanInfoPAK = labelScanInfoPAK;
            _labelStatusInfo = labelStatusInfo;
            _buttonWygenerujBarcode = buttonWygenerujBarcode;

        }
        public override void port_DataReceived(object sender, SerialDataReceivedEventArgs rcvdData)
        {
            while (Port.BytesToRead > 0)
            {
                _lineReadIn += Port.ReadExisting();
                Thread.Sleep(25);
            }

            _lineReadIn = Regex.Replace(_lineReadIn, @"\s+", string.Empty);
            ChangeControl.UpdateControl(_labelScanOut2, _lineReadIn, true);

            if (_lineReadIn.Length > 11)
                _lineReadIn = _lineReadIn.Remove(11);

            _poprzedniBarcode = "dup";

            if(Form1.ScanOkScannerPackout_FisrtTime)
                if(BoxToPackaut.ListOfScannedBarcodesPacked.Last().Equals(_lineReadIn))
                {
                    Form1.ScanOkScannerPackout = true;
                    Form1.ScanOkScannerPackout_FisrtTime = false;
                    return;
                }


            if (_lineReadIn.Length == 11 && !_lineReadIn.Contains("ER") && !_lineReadIn.Equals(_poprzedniBarcode))
            {
                var checkSnCanBeAddedToPackedList = PackoutMethodsForCheckSn.CheckSnCanBeAddedToPackedList(_lineReadIn);
                if (checkSnCanBeAddedToPackedList == 0)
                {
                    var isItLastElement = false;
                    if (BoxToPackaut.ListOfScannedBarcodesPacked.Count > 0)
                        isItLastElement = BoxToPackaut.ListOfScannedBarcodesPacked.Last().Equals(_lineReadIn);

                    if (!_lineReadIn.Equals(_poprzedniBarcode) && !isItLastElement && Form1.ScanOkScannerPackout == false)
                    {
                        BoxToPackaut.AddSnToPackoutListAndWriteToFile(_lineReadIn);

                        if (BoxToPackaut.ListOfScannedBarcodesPacked.Count > 294 &&
                         BoxToPackaut.ListOfScannedBarcodesVerified.IndexOf(_lineReadIn) == BoxToPackaut.ListOfScannedBarcodesPacked.IndexOf(_lineReadIn))
                            Form1.ScanOkScannerPackout = true;

                        _poprzedniBarcode = _lineReadIn;

                    }
                    if (BoxToPackaut.ListOfScannedBarcodesVerified.IndexOf(_lineReadIn) == BoxToPackaut.ListOfScannedBarcodesPacked.IndexOf(_lineReadIn) && Form1.ScanOkScannerPackout == false)
                    {
                        if(BoxToPackaut.ListOfScannedBarcodesPacked.Count <= 294)
                            Form1.ScanOkScannerPackout = true;

                            _counterPiecesPacked++;
                            ChangeControl.UpdateControl(_labelCountVerifiedPieces, Color.LawnGreen, BoxToPackaut.ListOfScannedBarcodesPacked.Count.ToString(), true);
                            ChangeControl.UpdateControl(_labelScanInfoPAK, Color.LawnGreen, $"Zeskanowano poprawnie barkod: {_lineReadIn}", true);
                    }                           
                    else
                    {
                       // Form1.ScanOkScannerPackout = false;
                            ChangeControl.UpdateControl(_labelScanInfoPAK, Color.Red, $"{_lineReadIn} NOK / (nie zgadza się kolejność)", true);
                    } 
                            

                    if (Form1.ScanOkScannerPackout && BoxToPackaut.ListOfScannedBarcodesPacked.Count > 294 && BoxToPackaut.ListOfScannedBarcodesPacked.Count <= 300
                        && BoxToPackaut.ListOfScannedBarcodesVerified.Count ==300)
                    {
                        ChangeControl.UpdateControl(_labelCountVerifiedPieces, Color.LawnGreen, BoxToPackaut.ListOfScannedBarcodesPacked.Count.ToString(), true);
                        ChangeControl.UpdateControl(_labelScanInfoPAK, Color.LawnGreen, $"Zeskanowano poprawnie barkod: {_lineReadIn}", true);
                        try
                        {
                            var res = PLC.WriteBool("PROGRAM:MainProgram.Mes_App_scan");
                            if (!res)
                            {
                                res = PLC.WriteBool("PROGRAM:MainProgram.Mes_App_scan");

                                if (!res)
                                {
                                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nastąpił błąd komunikacji ze sterownikiem...", " Wezwij UTR");
                                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Wpiszcie hasło i spróbujcie wyzwolić sygnał przyciskiem SKAN_OK,\n\n jeśli nie pomoże to zróbcie restart aplikacji, spróbujcie wyzwolić sygnał przyciskiem SKAN_OK,\n\n" +
                                        "jeśli i to nie pomoże sprawdźcie połączenie pomiędzy maszyną a komputerem: \n przycisk windows -> odpalenie wiersza poleceń (cmd) ->\n" +
                                        "wpiszcie polecenie: ping 192.168.1.214");
                                }

                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Pomoże?");
                        }

                        Form1.ScanOkScannerPackout = false;
                    }
                    if (BoxToPackaut.ListOfScannedBarcodesPacked.Count == 300)
                    {
                        ChangeControl.UpdateControl(_labelStatusInfo, Color.HotPink, "Zweryfikowano i spakowano 300 sztuk, wygeneruj Barcody! \n Pamiętaj żeby je potwierdzić po zeskanowaniu!", true);
                        ChangeControl.UpdateControl(_buttonWygenerujBarcode, Color.DeepPink, "Wygeneruj Barkody!", true);
                    }
                    
                }
                else
                {
                    if(checkSnCanBeAddedToPackedList == 2)
                        ChangeControl.UpdateControl(_labelScanInfoPAK, Color.Red, $"{_lineReadIn} NOK / nie spełnia wymagań", true);
                    else if(_labelScanInfoPAK.BackColor != Color.Green)
                        ChangeControl.UpdateControl(_labelScanInfoPAK, Color.Green, $"{_lineReadIn} został już zweryfikowany", true);
                }

            }
            _lineReadIn = string.Empty;
        }
    }
}

