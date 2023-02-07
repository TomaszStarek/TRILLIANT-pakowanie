using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public static class BoxToPackaut
    {
        public static List<string> ListOfScannedBarcodesVerified {get; private set; } = new List<string>();

        public static List<string> ListOfScannedBarcodesPacked{ get; private set; } = new List<string>();

        public static void AddSnToPackoutListAndWriteToFile(string snToAdd)
        {
            ListOfScannedBarcodesPacked.Add(snToAdd);
            WriteActualSnToTxt(snToAdd, "packedbarcodes");
        }


        private static int WriteActualSnToTxt(string sn, string fileName)
        {


            // textBox1.Text = sn;

            ////////////////////////string sciezka = ("C:/logi/");      //definiowanieścieżki do której zapisywane logi
            ////////////////////////var date = DateTime.Now;
            ////////////////////////if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
            ////////////////////////{
            ////////////////////////    ;
            ////////////////////////}
            ////////////////////////else
            ////////////////////////    System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

            try
            {
                sn = System.Text.RegularExpressions.Regex.Replace(sn, @"\s+", string.Empty);

                using (StreamWriter sw = new StreamWriter(fileName + @".txt", true))
                {

                    sw.WriteLine(sn);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true}, ex.Message);              
                return 0;
            }

            return 1;
        }

        public static int CheckIsBarcodeDuplicatedAndAddToListIfNot(string barcodeToCheck)
        {
            if (ListOfScannedBarcodesVerified.Count >= 300)
                return 2;


            if (!ListOfScannedBarcodesVerified.Contains(barcodeToCheck) )
            {
                ListOfScannedBarcodesVerified.Add(barcodeToCheck);
                WriteActualSnToTxt(barcodeToCheck, "parametry");
                return 0;
            }
            return 1;
                
        }
        public static int CheckIsBarcodeDuplicated(string barcodeToCheck)
        {
            if (ListOfScannedBarcodesVerified.Count >= 300)
                return 2;

            if (!ListOfScannedBarcodesVerified.Contains(barcodeToCheck))
            {
                return 0;
            }
            return 1;

        }
        public static bool IsListCountIsLessEqThan300()
        {

            if (ListOfScannedBarcodesVerified.Count <= 300)
            {
                return true;
            }

            return false;
        }

        public static bool IsListCountIsEqual300()
        {

            if(ListOfScannedBarcodesVerified.Count == 300)
            {
                return true;
            }

            return false;
        }

        public static bool ClearEverything()
        {
            DialogResult d = MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Czy jesteś pewny/a. Utracisz wszystkie dane", "Usuwanie danych", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                ListOfScannedBarcodesVerified.Clear();
                ListOfScannedBarcodesPacked.Clear();
                ClearActualListTxtLog("parametry");
                ClearActualListTxtLog("packedbarcodes");
                return true;

            }
            return false;
            
        }

        public static Tuple<string, string> CreateStringFromList()
        {
            var _barcode1 = "";
            var _barcode2 = "";
            var i = 0;

            foreach (var item in ListOfScannedBarcodesVerified)
            {
                if(i < 150)
                    _barcode1 += $"{item};";
                else
                    _barcode2 += $"{item};";
                i++;
            }

            return Tuple.Create(_barcode1, _barcode2);
        }

        public static int WriteCompletedListToTxtLog(string sn)
        {
            sn = System.Text.RegularExpressions.Regex.Replace(sn, @"\s+", string.Empty);

            // textBox1.Text = sn;

            string sciezka = ("C:/logi/");      //definiowanieścieżki do której zapisywane logi
            var date = DateTime.Now;
            if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
            {
                ;
            }
            else
                System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

            try
            {
                using (StreamWriter sw = new StreamWriter("C:/logi/" + sn + "(" + date.ToString("yyyy-MM-dd HH-mm-ss") + ")" + ".txt"))
                {

                    foreach (var item in ListOfScannedBarcodesVerified)
                    {
                        sw.WriteLine(item);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.Message);
                return 0;
            }

            return 1;

        }

        public static bool ClearActualListTxtLog(string name)
        {
            try
            {
                using (FileStream fs = File.Open($@"{name}.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    lock (fs)
                    {
                        fs.SetLength(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.Message);
                return false;
            }

            return true;
        }

        public static int Read_param()
        {
            string sciezka = (@"parametry.txt");
            int i = 0;
            try
            {
                using (StreamReader sr = new StreamReader(sciezka))
                {
                    ListOfScannedBarcodesVerified.Clear();
                    
                    while (sr.Peek() >= 0)
                    {
                        ListOfScannedBarcodesVerified.Add(sr.ReadLine());
                        i++;
                    }
                    sr.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "blad odczytu parametrow:" + ex);
                ListOfScannedBarcodesVerified.Clear();
                return 0;
            }
            return i;
        }
        public static int ReadPackoutSn()
        {
            string sciezka = (@"packedbarcodes.txt");
            int i = 0;
            try
            {
                using (StreamReader sr = new StreamReader(sciezka))
                {
                    ListOfScannedBarcodesPacked.Clear();

                    while (sr.Peek() >= 0)
                    {
                        ListOfScannedBarcodesPacked.Add(sr.ReadLine());
                        i++;
                    }
                    sr.Close();
                }

                if (ListOfScannedBarcodesPacked.Count >= i && ListOfScannedBarcodesVerified.Count >= i && i > 0)
                    if (ListOfScannedBarcodesPacked[i - 1] == ListOfScannedBarcodesVerified[i - 1])
                    {
                        Form1.ScanOkScannerPackout_FisrtTime = true;
                    }
                return i;

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "blad odczytu parametrow:\n\n" + ex);
                ListOfScannedBarcodesVerified.Clear();
                return 0;
            }


        }

        public static bool CheckNumbers(string client)
        {
            List<string> listToCompare = new List<string>();
            var webservices = new WebServices();
            try
            {
                string first = webservices.GetBoxNumber("TRILLIANT", ListOfScannedBarcodesVerified[0]);
                string last = webservices.GetBoxNumber("TRILLIANT", ListOfScannedBarcodesVerified[ListOfScannedBarcodesVerified.Count - 1]);

                if(first.Equals("0") || last.Equals("0"))
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie wszystkie numery seryjne mają przypisany numer boxu!");
                    return false;
                }

                if (first == last)
                {
                    var result = webservices.GetSerialNoByBox("TRILLIANT", first);
                    listToCompare = result.Split(';').ToList();

                    var firstNotSecond = ListOfScannedBarcodesVerified.Except(listToCompare).ToList();

                    if (firstNotSecond.Count == 0)
                    {
                        //zrobic zapis do pliku i wyczyscic liste
                        if (WriteCompletedListToTxtLog(SQL.BoxIdFromDb) == 1)
                        {
                            ListOfScannedBarcodesVerified.Clear();
                            ListOfScannedBarcodesPacked.Clear();
                            ClearActualListTxtLog("parametry");
                            ClearActualListTxtLog("packedbarcodes");
                            
                        }
                        else
                            MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się stworzyć pliku z numerami boxu na C:/logi!");

                        var errorCount = ProblemsToReport.ListOfOccurredProblems.Count;
                        if (errorCount > 0)
                        {
                            SQL.SendDataOfAllErrorsFromBoxToDb();
                            try
                            {
                                SQL.UpdateCountOfErrorsInDbBox(errorCount.ToString(), SQL.BoxIdFromDb);
                            }
                            catch (Exception)
                            {
                                SQL.UpdateCountOfErrorsInDbBox("99999", SQL.BoxIdFromDb);
                            }
                            

                            if (ProblemsToReport.WriteCompletedErrorListToTxtLog(SQL.BoxIdFromDb) == 1)
                            {
                                ;
                            }
                            else
                                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się stworzyć pliku z błędami boxu na C:/errorlogi!");
                            ProblemsToReport.ClearAllErr();
                        }
                        if (ProblemsToReport.ListOfWarnings.Count > 0)
                        {
                            SQL.SendDataOfAllWarningsFromBoxToDb();

                            if (ProblemsToReport.WriteCompletedWarningsListToTxtLog(SQL.BoxIdFromDb) == 1)
                            {
                                ;
                            }
                            else
                                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Nie udało się stworzyć pliku z błędami boxu na C:/warningslogi!");
                            ProblemsToReport.ClearAllWarn();
                        }
                        return true;
                    }
                    
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd programu:" + ex);
                return false;
            }

        }


        //public static bool CheckIsBarcodePackedThenClearItFromList()
        //{
        //    //List<string> copyOfListOfScannedBarcodes = new List<string>();
        //    ListOfScannedBarcodes.Clear();
        //    for (int i = 0; i < 301; i++)
        //    {
        //        ListOfScannedBarcodes.Add("NEQA4909856");

        //    }

        //    List<string> copyOfListOfScannedBarcodes = new List<string>(ListOfScannedBarcodes);
        //      var lenghtOfList = ListOfScannedBarcodes.Count;
        //    string boxnumber = "", boxnumberToCompare = "";

        //    WebServices webServices = new WebServices();

        //    for (int i = 0; i < lenghtOfList; i++)
        //    {
        //        boxnumber = webServices.GetBoxNumber("TRILLIANT", copyOfListOfScannedBarcodes[0]);

        //        if (i == 0)
        //            boxnumberToCompare = boxnumber;


        //        if (boxnumber.Length > 2 && boxnumberToCompare.Equals(boxnumber))
        //            copyOfListOfScannedBarcodes.RemoveAt(0);
        //        else
        //        {
        //            if (boxnumber.Equals("0"))
        //                MessageBox.Show($"Numer {copyOfListOfScannedBarcodes[0]} nie ma przypisanego boxu!");
        //            else if (!boxnumberToCompare.Equals(boxnumber))
        //                MessageBox.Show("Nie wszystkie numery z listy mają ten sam numer boxu!");
        //            else
        //                MessageBox.Show("Brak połączenia z MES!");

        //            return false;
        //        }

        //    }
        //    //zrobic zapis do pliku i wyczyscic liste
        //    if (WriteCompletedListToTxtLog(boxnumber) == 1)
        //    {
        //        ListOfScannedBarcodes.Clear();
        //        ClearActualListTxtLog();
        //    }
        //    else
        //        MessageBox.Show("Nie udało się stworzyć pliku z numerami boxu na C:/logi!");



        //    if (ListOfScannedBarcodes.Count > 0)
        //        return false;
        //    else
        //        return true;

        //}

    }
}
