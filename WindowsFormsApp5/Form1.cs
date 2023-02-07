using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using EasyModbus;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Text.RegularExpressions;

namespace WindowsFormsApp5
{
    

    public partial class Form1 : Form
    {
        #region fields
        private QrForm frm2, frm;
        private ConfirmProblemOccurrence frm3;
        private SerialPort port;
        private string _lineReadIn;
        private string _poprzedniBarcode;
        private int _counterPiecesInBox = 0;
        private bool _boxDone = false;

        private ScannerForCheckBoards scannerForCheckBoards;
        private ScannerForPackout scannerForPackout;

        // this will prevent cross-threading between the serial port
        // received data thread & the display of that data on the central thread
        private delegate void preventCrossThreading(string x);
        private preventCrossThreading accessControlFromCentralThread;
        private static System.Timers.Timer aTimer;
        #endregion

        
        public static bool ScanOkScannerPackout { get; set; } = false;

        public static bool ScanOkScannerPackout_FisrtTime { get; set; } = false;
        public static bool ScanerBlocked { get; set; } = false;
        public static string ManualBarcodeAfterBlock { get; set; } = "";
        #region timer 
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(500);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        // bool flaga; 

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (scannerForCheckBoards.Port.IsOpen)
            {
                try
                {
                    scannerForCheckBoards.Port.Write("LON\r");
                }
                catch (Exception)
                {
                    ;
                }           
            }
            if (scannerForPackout.Port.IsOpen)
            {
                try
                {
                    scannerForPackout.Port.Write("LON\r");
                }
                catch (Exception)
                {
                    ;
                }
            }
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            scannerForPackout = new ScannerForPackout("COM7", textBox3, labelScanOut2,labelCountPackedPieces,labelScanInfoPAK, labelStatusInfo, buttonWygenerujBarcode);

           scannerForCheckBoards = new ScannerForCheckBoards("COM4", textBox1, labelScanOut,
                        labelCountVerifiedPiecesToBox, labelStatusInfo, buttonWygenerujBarcode, BtnErrorOccursAccept);


            if (File.Exists(@"parametry.txt"))
            {
                _counterPiecesInBox = BoxToPackaut.Read_param();
                ChangeControl.UpdateControl(labelCountVerifiedPiecesToBox, _counterPiecesInBox.ToString(), true);
            }
            if (File.Exists(@"packedbarcodes.txt"))
            {
                _counterPiecesInBox = BoxToPackaut.ReadPackoutSn();
                ChangeControl.UpdateControl(labelCountPackedPieces, _counterPiecesInBox.ToString(), true);
            }
            if (File.Exists(@"errors.txt"))
            {
                ProblemsToReport.ReadErrors();

            }
            if (File.Exists(@"warnings.txt"))
            {
                ProblemsToReport.ReadWarnings();

            }
            if (_counterPiecesInBox == 0)
            {
                try
                {
                //PLC.RunBool("PROGRAM:MainProgram.Mes_App_scan");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(), "Błąd połączenia z PLC!");
                }
            }

            SetTimer();
            aTimer.Start();
          //  PLC.InitEvent();
        }

        // this is called when the serial port has receive-data for us.

        private void button1_Click_1(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if(clickedButton.Name.Equals("button1"))
            {
                if (scannerForCheckBoards.Port.IsOpen)
                {
                    aTimer.Enabled = true;
                    port.Write("LON\r");
                }
                else
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Port skanera zamknięty!");
            }
            else
            {
                if (scannerForPackout.Port.IsOpen)
                {
                    aTimer.Enabled = true;
                    scannerForPackout.Port.Write("LON\r");
                }
                else
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Port skanera zamknięty!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (clickedButton.Name.Equals("button2"))
            {
                if (scannerForCheckBoards.Port.IsOpen)
                {
                    aTimer.Enabled = false;
                    port.Write("LOFF\r");
                }
                else
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Port skanera zamknięty!");
            }
            else
            {
                if (scannerForPackout.Port.IsOpen)
                {
                    aTimer.Enabled = false;
                    scannerForPackout.Port.Write("LOFF\r");
                }
                else
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Port skanera zamknięty!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PLC.WriteBool("PROGRAM:MainProgram.Mes_App_scan");
            ProblemsToReport.WriteToListProblem("Wciśnięto przycisk SKAN_OK");
            //    PLC.ReadDint("PROGRAM:MainProgram.HMI_MainPlacedParts");           
            //    PLC.RunDint("PROGRAM:MainProgram.Mes_App_counter");            
        }

        private void ClearAllData()
        {
            var result = BoxToPackaut.ClearEverything();

            if (result)
            {
                _counterPiecesInBox = 0;
                scannerForPackout._counterPiecesPacked = 0;
                Form1.ScanOkScannerPackout = false;
                _boxDone = false;

                ChangeControl.UpdateControl(labelScanInfoPAK, Color.HotPink, "Wyzerowano wszystkie dane!", true);
                ChangeControl.UpdateControl(labelCountPackedPieces, Color.DeepPink, "0", true);

                ProblemsToReport.ClearAllErr();
                ProblemsToReport.ClearAllWarn();

                ChangeControl.UpdateControl(labelStatusInfo, Color.HotPink, "Wyzerowano wszystkie dane!", true);
                ChangeControl.UpdateControl(buttonWygenerujBarcode, Color.DeepPink, "", false);

                ChangeControl.UpdateControl(labelBarcode1Accepted, Color.LawnGreen, "", false);
                ChangeControl.UpdateControl(labelBarcode2Accepted, Color.LawnGreen, "", false);

                ChangeControl.UpdateControl(buttonIfMesDoneThenPlcDone, Color.Plum, "", false);
                ChangeControl.UpdateControl(BtnErrorOccursAccept, Color.Fuchsia, "", false);

                ChangeControl.UpdateControl(labelCountVerifiedPiecesToBox, SystemColors.Control, _counterPiecesInBox.ToString(), true);

                var res = PLC.DintToZero("PROGRAM:MainProgram.App_Mes_Error_Occurred_int");

                if (!res)
                {
                    res = PLC.DintToZero("PROGRAM:MainProgram.App_Mes_Error_Occurred_int");
                    if (!res)
                    {
                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd przy zerowaniu pamięci błędów maszyny"
                            , "Błąd komunikacji ze sterownikiem");
                        ProblemsToReport.WriteToListProblem("Błąd przy zerowaniu pamięci błędów maszyny");
                    }
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeControl.UpdateControl(labelStatusInfo, Color.HotPink, "Zeskanowano 300 sztuk wygeneruj Barkody! \n Pamiętaj żeby je potwierdzić po zeskanowaniu!", true);
            ChangeControl.UpdateControl(buttonWygenerujBarcode, Color.DeepPink, "Wygeneruj Barkody!", true);

            ChangeControl.UpdateControl(labelBarcode1Accepted, Color.Red, "Zaakceptuj 1. część barkodu!", true);
            ChangeControl.UpdateControl(labelBarcode2Accepted, Color.Red, "Zaakceptuj 2. część barkodu!", true);

            ChangeControl.UpdateControl(labelBarcode1Accepted, Color.LawnGreen, "Zaakceptowano 1. część barkodu!", true);
            ChangeControl.UpdateControl(labelBarcode2Accepted, Color.LawnGreen, "Zaakceptowano 2. część barkodu!", true);
            ChangeControl.UpdateControl(BtnErrorOccursAccept, Color.Fuchsia, "Potwierdź wystąpienie błędów!", true);

            ProblemsToReport.WriteToListProblem("Wciśnięto przycisk odkryj okienka");
            //    ChangeControl.UpdateControl(buttonIfMesDoneThenPlcDone, Color.Plum, "Sprawdź barkody zeskanowanych płytek w MES i wyslij sygnał końca boxu do PLC!", true);

        }
        private void BtnErrorOccursAccept_Click(object sender, EventArgs e)
        {
            frm3 = new ConfirmProblemOccurrence( labelStatusInfo, buttonWygenerujBarcode, BtnErrorOccursAccept, buttonDoneBoxNoBarcode, _counterPiecesInBox);
            frm3.Show();
        }

        private void BarcodeGenerateOnClick()
        {

                var xd = BoxToPackaut.CreateStringFromList();

                //MessageBox.Show(xd.Item1);
                //MessageBox.Show(xd.Item2);
                frm2 = new QrForm(xd.Item2, false, labelBarcode2Accepted, buttonIfMesDoneThenPlcDone);
                frm2.Show();
                frm = new QrForm(xd.Item1, true, labelBarcode1Accepted, buttonIfMesDoneThenPlcDone);
                frm.Show();

                ChangeControl.UpdateControl(labelBarcode1Accepted, Color.Red, "Zaakceptuj 1. część barkodu!", true);
                ChangeControl.UpdateControl(labelBarcode2Accepted, Color.Red, "Zaakceptuj 2. część barkodu!", true);

                ChangeControl.UpdateControl(labelStatusInfo, "Wysyłam do plc sygnał końca boxu! \nPo zeskanowaniu barkodów do MES, sprawdź je za pomocą przycisku: Sprawdź numery w MES! ", true);

                ChangeControl.UpdateControl(buttonIfMesDoneThenPlcDone, Color.Plum, "Sprawdź numery w MES", true);
                Thread.Sleep(250);
                ////////if (!_boxDone)
                ////////{
                ////////    var res = PLC.WriteBool("PROGRAM:MainProgram.Mes_App_Box_Done"); //if everything OK send to the PLC signal to done the box

                ////////    if (!res) //if not display info for technicians what they should check
                ////////    {
                ////////        res = PLC.WriteBool("PROGRAM:MainProgram.Mes_App_Box_Done");
                ////////        if (!res)
                ////////        {
                ////////            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Numery zostały poprawnie spakowane! \n Nastąpił jednak błąd komunikacji ze sterownikiem...", " Wezwij UTR");
                ////////            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Wpiszcie hasło i spróbujcie wyzwolić sygnał przyciskiem końca partii,\n\n jeśli nie pomoże to zróbcie restart aplikacji,\n\n" +
                ////////                "jeśli i to nie pomoże sprawdźcie połączenie pomiędzy maszyną a komputerem: \n przycisk windows -> odpalenie wiersza poleceń (cmd) ->\n" +
                ////////                "wpiszcie polecenie: ping 192.168.1.214");
                ////////        }

                ////////    }
                ////////    else
                ////////        _boxDone = true;
                ////////}

        }

        private void FinishBoxWithoutGenerateBarcodes()
        {
            if (!_boxDone)
            {
                var res = PLC.WriteBool("PROGRAM:MainProgram.Mes_App_Box_Done"); //if everything OK send to the PLC signal to done the box

                if (!res) //if not display info for technicians what they should check
                {
                    res = PLC.WriteBool("PROGRAM:MainProgram.Mes_App_Box_Done");
                    if (!res)
                    {
                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Numery zostały poprawnie spakowane! \n Nastąpił jednak błąd komunikacji ze sterownikiem...", " Wezwij UTR");
                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Wpiszcie hasło i spróbujcie wyzwolić sygnał przyciskiem końca partii,\n\n jeśli nie pomoże to zróbcie restart aplikacji,\n\n" +
                            "jeśli i to nie pomoże sprawdźcie połączenie pomiędzy maszyną a komputerem: \n przycisk windows -> odpalenie wiersza poleceń (cmd) ->\n" +
                            "wpiszcie polecenie: ping 192.168.1.214");
                    }
                    

                }
                if(res)
                {
                    if(SQL.ReadCountOfSerialNumbersOfGivenIdBox(SQL.BoxIdFromDb) == 300)
                    {
                        if (BoxToPackaut.WriteCompletedListToTxtLog(SQL.BoxIdFromDb) == 1)
                        {
                            BoxToPackaut.ListOfScannedBarcodesVerified.Clear();
                            BoxToPackaut.ListOfScannedBarcodesPacked.Clear();
                            BoxToPackaut.ClearActualListTxtLog("parametry");
                            BoxToPackaut.ClearActualListTxtLog("packedbarcodes");
                        }
                        else
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się stworzyć pliku z numerami boxu na C:/logi!");


                        if (ProblemsToReport.WriteCompletedErrorListToTxtLog(SQL.BoxIdFromDb) == 1)
                        {
                            ;
                        }
                        else
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się stworzyć pliku z błędami boxu na C:/errorlogi!");


                        if (ProblemsToReport.WriteCompletedWarningsListToTxtLog(SQL.BoxIdFromDb) == 1)
                        {
                            ;
                        }
                        else
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się stworzyć pliku z błędami boxu na C:/warningslogi!");

                    }
                    else
                    {
                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd przy zapisie / odczycie rekordów bazy danych"
                            , "Błąd komunikacji z bazą danych");
                    }                   

                    _counterPiecesInBox = 0;
                    _boxDone = false;

                    ProblemsToReport.ClearAllErr();
                    ProblemsToReport.ClearAllWarn();

                    var res2 = PLC.DintToZero("PROGRAM:MainProgram.App_Mes_Error_Occurred_int");

                    if (!res2)
                    {
                        res2 = PLC.DintToZero("PROGRAM:MainProgram.App_Mes_Error_Occurred_int");
                        if (!res2)
                        {
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd przy zerowaniu pamięci błędów maszyny"
                                , "Błąd komunikacji ze sterownikiem");
                            ProblemsToReport.WriteToListProblem("Błąd przy zerowaniu pamięci błędów maszyny");
                        }
                    }

                    //reset all labels for the new box
                    ChangeControl.UpdateControl(buttonDoneBoxNoBarcode, Color.DarkMagenta, "Zakończ pakowanie rolki", false);
                    ChangeControl.UpdateControl(labelStatusInfo, Color.Green, "Zweryfikuj rolkę za pomocą nowej aplikacji, żeby wygenerować barkody \n Dane boxu wyczyszczone, można zaczynać nowy box", true);
                    ChangeControl.UpdateControl(labelUTR, "", true);
                    ChangeControl.UpdateControl(buttonWygenerujBarcode, "", false);
                    ChangeControl.UpdateControl(labelBarcode1Accepted, "", false);
                    ChangeControl.UpdateControl(labelBarcode2Accepted, "", false);

                    _counterPiecesInBox = 0;  //numbers are OK, so reset the counter for the new box
                    ChangeControl.UpdateControl(labelCountVerifiedPiecesToBox, _counterPiecesInBox.ToString(), true);

                    ChangeControl.UpdateControl(buttonIfMesDoneThenPlcDone, Color.Plum, "Sprawdź numery w MES", false);
                    ChangeControl.UpdateControl(BtnErrorOccursAccept, Color.Fuchsia, "Potwierdż wystąpienie błędów!", false);

                }
                    
            }



        }

            private void button6_Click(object sender, EventArgs e)
        {
            if (frm3 is null)
            {
                //if (ProblemsToReport.ListOfOccurredProblems.Count == 0)
                //{
                    BarcodeGenerateOnClick();
                //}
                //else
                //{
                //    ChangeControl.UpdateControl(labelStatusInfo, "Wystąpiły błędy podczas pakowania!\nZaakceptuj wyskakujący komunikat!", true);
                //}
            }
            else
            {
                if (frm3.Confirmed) //|| ProblemsToReport.ListOfOccurredProblems.Count == 0)
                {
                    BarcodeGenerateOnClick();
                }
                //else
                //{
                //    ChangeControl.UpdateControl(labelStatusInfo, "Wystąpiły błędy podczas pakowania!\nZaakceptuj wyskakujący komunikat!", true);
                //}
            }

        }



        private void button6_Click_2(object sender, EventArgs e)
        {
            if (_counterPiecesInBox == 300 || _counterPiecesInBox == 0)
            {
                PLC.WriteBool("PROGRAM:MainProgram.Mes_App_Box_Done");
                ProblemsToReport.WriteToListProblem("Wciśnięto przycisk końca partii");
            }
            else
                MessageBox.Show("Brak 300 płytek, nie udało się wysłać sygnału końca partii");

        }

        private void buttonIfMesDoneThenPlcDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm2 != null && frm != null)
                    if (!frm2.Confirmed)
                    {
                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Zaakceptuj drugą część barkodu!");
                    }
                    else
                    {
                        if (!frm.Confirmed)
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Zaakceptuj pierwszą część barkodu!");
                        else
                        {
                            ChangeControl.UpdateControl(labelStatusInfo, Color.Plum, "Barkody poprawnie zaakceptowane, sprawdzam numery w MES!", true);
                            Thread.Sleep(100);
                            var res = CheckHistory.CheckAllNumbers(labelStatusInfo, labelUTR, buttonWygenerujBarcode, labelBarcode1Accepted,
                                        labelBarcode2Accepted, ref _counterPiecesInBox, labelCountVerifiedPiecesToBox, labelScanInfoPAK, labelCountPackedPieces, buttonIfMesDoneThenPlcDone, BtnErrorOccursAccept);
                            if (res)
                            {
                                var result = PLC.DintToZero("PROGRAM:MainProgram.App_Mes_Error_Occurred_int");

                                if (!result)
                                {
                                    result = PLC.DintToZero("PROGRAM:MainProgram.App_Mes_Error_Occurred_int");
                                    if (!result)
                                    {
                                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd przy zerowaniu pamięci błędów maszyny"
                                            , "Błąd komunikacji ze sterownikiem");
                                        ProblemsToReport.WriteToListProblem("Błąd przy zerowaniu pamięci błędów maszyny");
                                    }
                                }
                                _boxDone = false;

                                textBox2.Text = string.Empty;
                                textBox2.Visible = true;
                            }
                                
                        }

                    }
                else
                {
                    ChangeControl.UpdateControl(labelStatusInfo, Color.Red, "Wpierw wygeneruj i zaakceptuj barkody!", true);
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Wpierw wygeneruj i zaakceptuj barkody!");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd programu:" + ex);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm3 = new ListOfScannedSn();
            frm3.Show();

        }
        private void button8_Click(object sender, EventArgs e)
        {
            //var hui = new WebServices();

            //hui.GetNcNumber("TRILLIANT", "NEQA5353882");
          //  var kak = CheckHistory.checkSnHistory("NEQA5159002");



           // var version = $"{libplctag.LibPlcTag.VersionMajor}.{libplctag.LibPlcTag.VersionMinor}.{libplctag.LibPlcTag.VersionPatch}";
           ////    Form1.ScanOkScannerPackout = true;
           //  SQL.ReadCountOfSerialNumbersOfGivenIdBox("19");
           ////SQL.CreateBoxInDb();
           ////SQL.ReadCreatedBoxInDb();
           ////SQL.SendDataOfCompletedBoxToDb();
           ////SQL.SendDataOfAllErrorsFromBoxToDb();
           ////SQL.SendDataOfAllWarningsFromBoxToDb();
            ///
            //ChangeControl.UpdateControl(BtnErrorOccursAccept, Color.Red, "Wpierw wygeneruj i zaakceptuj barkody!", true);
            //          SQL.UpdateCountOfErrorsInDbBox("695684","19");
            //      ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Wystąpił błąd historii produktu! \n1.Sprawdź wyskakujące komunikaty!\n2.Zeskanuj skanerem ręcznym nową płytkę!\n3.Odłóż ją na miejsce odczytu skanera automatycznego!", true);
            //int uj = 0;
            //PLC.ReadDint("HMI_MainPlacedParts", out uj);
            //PROGRAM:MainProgram.App_Mes_Error_Occurred_int  TM50ppWG
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.ToUpper().Equals("UTR"))
            {
                ChangeControl.UpdateControl(labelUTR, "", false);
                textBox2.Visible = false;
            }        
            else
                ChangeControl.UpdateControl(labelUTR, "", true);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var fail = true;
                var barcodeManual = Regex.Replace(textBox1.Text, @"\s+", string.Empty);

                if(barcodeManual.Length == 11)
                {
                    if(CheckHistory.checkSnHistory(barcodeManual))
                    {
                        if(BoxToPackaut.CheckIsBarcodeDuplicated(barcodeManual) == 0)
                        {
                            ManualBarcodeAfterBlock = barcodeManual;
                            fail = false;
                        }
                    }

                }
                if(fail)
                {
                    textBox1.Text = String.Empty;
                    textBox1.Select();                   
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FinishBoxWithoutGenerateBarcodes();
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            aTimer.Stop();
            Thread.Sleep(100);

            if (scannerForCheckBoards.Port.IsOpen)
            {
                scannerForCheckBoards.Port.Write("LOFF\r");
            }
            if (scannerForPackout.Port.IsOpen)
            {
                scannerForPackout.Port.Write("LOFF\r");
            }

            if (_counterPiecesInBox > 0)
                ProblemsToReport.WriteToListProblem("Zamknięto aplikację podczas pakowania");
            //    Thread.Sleep(10000);
            //port.Close();
            System.Windows.Forms.Application.Exit();
            //Application.
        }

 
    }
}
