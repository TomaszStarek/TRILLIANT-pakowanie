using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public static class MethodForVerifySnFromScanner
    {
        private static bool _blockComunicates = false;
        private static string _previousOkBarcode = "";
        private static int _counterDoubledScan = 0;
        private static bool PiecesMismatchIsConfirmed = false;
        private static bool PiecedBlockedBecauseOfMisMatch = false;
        public static string DataReceivedOperations(ref string _lineReadIn, ref string _poprzedniBarcode, ref int _counterPiecesInBox, Label labelScanOut,
            Label labelCountPiecesToBox, Label labelStatusInfo, Button buttonWygenerujBarcode, Button btnErrorOccursAccept)
        {           
            _lineReadIn = Regex.Replace(_lineReadIn, @"\s+", string.Empty);
            ChangeControl.UpdateControl(labelScanOut, _lineReadIn, true);

            if (_lineReadIn.Length > 11)
                _lineReadIn = _lineReadIn.Remove(11);

            
            if (_lineReadIn.Equals(_poprzedniBarcode))
            {
                
                if(_lineReadIn.Equals(_previousOkBarcode))
                {
                    if (_counterDoubledScan > 200)
                    {
                        ChangeControl.UpdateControl(labelStatusInfo, Color.Yellow, $"200 prób przeczytania tego samego barkodu: {_lineReadIn},\njeśli maszyna się zatrzymała, a skaner wyświetla cyfrę, to" +
                            $" sprawdź czy\npłytka poprzedzająca nie ma takiego samego barkodu, może to być przyczyną zatrzymania (aplikacja nie zeskanuje dwa razy tego samego numeru).", true);
                        _counterDoubledScan = 0;
                        ProblemsToReport.WriteWarnigToList($"200 prób przeczytania tego samego barkodu: {_lineReadIn}");
                    }

                }
                // displayTextReadIn(lineReadIn);
                _counterDoubledScan++;
            }
            else if (_lineReadIn.Length == 11 && !_lineReadIn.Contains("ER"))
            {
                if(!PiecesMismatchIsConfirmed)
                {
                    int counterOfPiecesFromMachine = 0;
                    // var res = PLC.ReadDint();
                    var res = PLC.ReadDint("HMI_MainPlacedParts",out counterOfPiecesFromMachine);

                    if (!res)
                    {
                        res = PLC.ReadDint("HMI_MainPlacedParts", out counterOfPiecesFromMachine);
                        if (!res)
                        {
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się poprawnie odczytać z maszyny ilości położonych przez nią płytek. " +
                            "Sprawdzanie czy liczba zeskanowanych płytek jest zgodna z liczbą na maszynie zostanie wyłączona dla tej rolki",
                            "Błąd komunikacji ze sterownikiem");
                            PiecesMismatchIsConfirmed = true;
                            ProblemsToReport.WriteToListProblem("Błąd komunikacji ze sterownikiem przy odczycie liczby płytek");
                        }
                    }


                    if (counterOfPiecesFromMachine - _counterPiecesInBox != 2 && counterOfPiecesFromMachine != 300)
                    {
                        //DialogResult d = MessageBox.Show(new Form { TopLevel = true, TopMost = true },
                        //         $" Zeskanowano płytkę o numerze: {_lineReadIn}\n\n" +
                        //         $"Liczba zeskanowanych płytek jest niezgodna z tym co jest na maszynie, wyzeruj aplikację i maszynę, zacznij pakowanie tej rolki jeszcze raz :(",
                        //         "Nieprawidłowa liczba zeskanowanych płytek", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //ProblemsToReport.WriteWarnigToList($"Liczba zeskanowanych płytek jest niezgodna z tym co jest na maszynie sn:{_lineReadIn}");
                        //ProblemsToReport.WriteToListProblem($"Liczba zeskanowanych płytek jest niezgodna z tym co jest na maszynie sn:{_lineReadIn}");
                        //PiecedBlockedBecauseOfMisMatch = true;
                        PiecedBlockedBecauseOfMisMatch = false;
                    }
                    else
                        PiecedBlockedBecauseOfMisMatch = false;
                }

                if(!PiecedBlockedBecauseOfMisMatch)
                {
                    if (!Form1.ScanerBlocked)
                    {
                        if ((BoxToPackaut.ListOfScannedBarcodesPacked.Count == 0 && BoxToPackaut.ListOfScannedBarcodesVerified.Count < 5) ||
                            (Form1.ScanOkScannerPackout && BoxToPackaut.ListOfScannedBarcodesVerified.Except(BoxToPackaut.ListOfScannedBarcodesPacked).Count() < 6) 
                            && BoxToPackaut.ListOfScannedBarcodesVerified.Count <= 300)
                        {

                            if (CheckHistory.checkSnHistory(_lineReadIn))
                            {
                                
                                var resultOfBarcode = BoxToPackaut.CheckIsBarcodeDuplicatedAndAddToListIfNot(_lineReadIn);
                                if (resultOfBarcode == 0) // OK
                                {
                                    _counterPiecesInBox++;
                                    ChangeControl.UpdateControl(labelCountPiecesToBox, BoxToPackaut.ListOfScannedBarcodesVerified.Count.ToString(), true);
                                    ChangeControl.UpdateControl(labelStatusInfo, Color.Green, $"Barkod {_lineReadIn}, Ok!", true);
                                    _blockComunicates = false;


                                    Form1.ScanOkScannerPackout = false;
                                    _previousOkBarcode = _lineReadIn;
                                    _counterDoubledScan = 0;
                                    var attemptsRunBool = 0;
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
                                    _poprzedniBarcode = _lineReadIn;

                                }
                                else if (resultOfBarcode == 1)
                                {
                                    ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Barkod {_lineReadIn}, już jest zeskanowany!\n", true);
                                    ProblemsToReport.WriteToListProblem($"Barkod {_lineReadIn}, już jest zeskanowany!");
                                }
                                else if (resultOfBarcode == 2)
                                {
                                    ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Partia już zawiera 300 sztuk!\n Zeskanowany numer{_lineReadIn} jest ignorowany!", true);
                                    ProblemsToReport.WriteToListProblem($"Partia już zawiera 300 sztuk! Zeskanowany numer{_lineReadIn} jest ignorowany!");
                                }


                                if (BoxToPackaut.IsListCountIsEqual300() && resultOfBarcode != 2)
                                {
                                    PlcPreparedMethods.checkErrorOccurrenceInPLC();

                                    SQL.CreateBoxInDb();
                                    SQL.ReadCreatedBoxInDb();
                                    SQL.SendDataOfCompletedBoxToDb();

                                    if (ProblemsToReport.ListOfOccurredProblems.Count == 0)
                                    {
                                        //ChangeControl.UpdateControl(labelStatusInfo, Color.HotPink, "Zeskanowano 300 sztuk, wygeneruj Barcody! \n Pamiętaj żeby je potwierdzić po zeskanowaniu!", true);
                                        //ChangeControl.UpdateControl(buttonWygenerujBarcode, Color.DeepPink, "Wygeneruj Barkody!", true);


                                    }
                                    else
                                    {
                                        //ChangeControl.UpdateControl(labelStatusInfo, Color.IndianRed, "Zeskanowano 300 sztuk, wystąpiły jednak błędy! \n Potwierdź błędy i przekaż box do sprawdzenia po zeskanowaniu!", true);
                                        //ChangeControl.UpdateControl(btnErrorOccursAccept, Color.Fuchsia, "Potwierdż wystąpienie błędów!", true);

                                        ////   btnErrorOccursAccept.PerformClick();

                                        //MethodInvoker methodInvokerDelegate = delegate ()
                                        //{ btnErrorOccursAccept.PerformClick(); };

                                        ////This will be true if Current thread is not UI thread.
                                        //if (btnErrorOccursAccept.InvokeRequired)
                                        //    btnErrorOccursAccept.Invoke(methodInvokerDelegate);
                                        //else
                                        //    methodInvokerDelegate();
                                    }
                                    _poprzedniBarcode = _lineReadIn;

                                }
                                //else
                                //    ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Brak potwierdzenia zeskanowania płyty przez drugi skaner", true);

                            }
                            else
                            {
                                ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Historia barkodu: {_lineReadIn}, NOK! \n Sprawdź wyskakujący komunikat!", true);
                                ProblemsToReport.WriteWarnigToList($"Historia barkodu: {_lineReadIn}, NOK! Sprawdź wyskakujący komunikat!");
                                //ProblemsToReport.WriteToListProblem($"Historia barkodu: {_lineReadIn}, NOK! Sprawdź wyskakujący komunikat!");
                                Form1.ScanerBlocked = true;
                                Form1.ManualBarcodeAfterBlock = "";
                            }
                        }
                        else
                        {
                            ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Brak potwierdzenia zeskanowania płyty przez drugi skaner", true);
                        }
                    }
                    else
                    {
                        if (Form1.ManualBarcodeAfterBlock.Length == 11)
                        {
                            ChangeControl.UpdateControl(labelStatusInfo, Color.Orange, $"Wystąpił błąd historii produktu!\n2.Zeskanuj skanerem ręcznym nową płytkę-> OK barkod:{Form1.ManualBarcodeAfterBlock}!\n3.Odłóż ją na miejsce odczytu skanera automatycznego!", true);
                        }
                        else
                            ChangeControl.UpdateControl(labelStatusInfo, Color.Red, $"Wystąpił błąd historii produktu! \n1.Sprawdź wyskakujące komunikaty!\n2.Zeskanuj skanerem ręcznym nową płytkę!\n3.Odłóż ją na miejsce odczytu skanera automatycznego!", true);
                        if (Form1.ManualBarcodeAfterBlock.Equals(_lineReadIn))
                        {
                            Form1.ScanerBlocked = false;
                        }
                    }
                    
                    
                }

                    
                return _lineReadIn;
            }
            else
            {
                return _lineReadIn;
            }
            return "";
        }
        public static void Test(Control labelStatusInfo, Button BtnErrorOccursAccept)
        {
            ChangeControl.UpdateControl(labelStatusInfo, Color.IndianRed, "Zeskanowano 300 sztuk, wystąpiły jednak błędy! \n Potwierdź błędy i przekaż box do sprawdzenia po zeskanowaniu!", true);
            ChangeControl.UpdateControl(BtnErrorOccursAccept, Color.Fuchsia, "Potwierdż wystąpienie błędów!", true);

            MethodInvoker methodInvokerDelegate = delegate ()
            { BtnErrorOccursAccept.PerformClick(); };

            //This will be true if Current thread is not UI thread.
            if (BtnErrorOccursAccept.InvokeRequired)
                BtnErrorOccursAccept.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }
        

    }
}
