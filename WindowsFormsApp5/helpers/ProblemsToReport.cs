using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public static class ProblemsToReport
    {
        public static bool ProblemOccurred { get; set; }
        public static List<string> ListOfOccurredProblems { get; private set; } = new List<string>();
        public static List<string> ListOfWarnings { get; private set; } = new List<string>();

        public static void WriteToListProblem(string barcodeWithProblemOccurred)
        {
            var now = DateTime.Now;
            ListOfOccurredProblems.Add(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));
            WriteProblemToTxt(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));

        }
        public static void WriteWarnigToList(string barcodeWithProblemOccurred)
        {
            var now = DateTime.Now;
            ListOfWarnings.Add(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));
            WriteWarningToTxt(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));

        }
        //public static void WriteToListProblem(string barcodeWithProblemOccurred, bool IsItJustWarning)
        //{
        //    var now = DateTime.Now;
        //    if(IsItJustWarning)
        //    {
        //        ListOfWarnings.Add(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));
        //        WriteProblemToTxt("y" + barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));
        //    }
        //    else
        //    {
        //        ListOfOccurredProblems.Add(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));
        //        WriteProblemToTxt(barcodeWithProblemOccurred + now.ToString(" yyyy-MM-dd HH-mm-ss"));
        //    }
        //}

        public static int ReadErrors()
        {
            string sciezka = (@"errors.txt");
            int i = 0;
            try
            {
                using (StreamReader sr = new StreamReader(sciezka))
                {
                    ListOfOccurredProblems.Clear();

                    while (sr.Peek() >= 0)
                    {
                        ListOfOccurredProblems.Add(sr.ReadLine());
                        i++;
                    }
                    sr.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "blad odczytu zeskanowanych płytek z pliku:" + ex);
                WriteToListProblem("blad odczytu błędów z pamięci programu");
              //  ListOfScannedBarcodes.Clear();
                return 0;
            }
            return i;
        }
        public static int ReadWarnings()
        {
            string sciezka = (@"warnings.txt");
            int i = 0;
            try
            {
                using (StreamReader sr = new StreamReader(sciezka))
                {
                    ListOfWarnings.Clear();

                    while (sr.Peek() >= 0)
                    {
                        ListOfWarnings.Add(sr.ReadLine());
                        i++;
                    }
                    sr.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "blad odczytu zeskanowanych płytek z pliku:" + ex);
                WriteToListProblem("blad odczytu komunikatów z pamięci programu");
                //  ListOfScannedBarcodes.Clear();
                return 0;
            }
            return i;
        }
        private static int WriteProblemToTxt(string sn)
        {
          //  sn = System.Text.RegularExpressions.Regex.Replace(sn, @"\s+", string.Empty);

            // textBox1.Text = sn;

            //////////string sciezka = ("C:/Errorlogi/");      //definiowanieścieżki do której zapisywane logi
            //////////var date = DateTime.Now;
            //////////if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
            //////////{
            //////////    ;
            //////////}
            //////////else
            //////////    System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

            try
            {
                using (StreamWriter sw = new StreamWriter(@"errors.txt", true))
                {

                    sw.WriteLine(sn);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.Message);

                var now = DateTime.Now;
                ListOfOccurredProblems.Add("Błąd zapisu błędu do pliku" + now.ToString(" yyyy-MM-dd HH-mm-ss"));

                return 0;
            }

            return 1;
        }

        private static int WriteWarningToTxt(string sn)
        {
            //  sn = System.Text.RegularExpressions.Regex.Replace(sn, @"\s+", string.Empty);

            // textBox1.Text = sn;

            //////////string sciezka = ("C:/Errorlogi/");      //definiowanieścieżki do której zapisywane logi
            //////////var date = DateTime.Now;
            //////////if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
            //////////{
            //////////    ;
            //////////}
            //////////else
            //////////    System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

            try
            {
                using (StreamWriter sw = new StreamWriter(@"warnings.txt", true))
                {

                    sw.WriteLine(sn);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.Message);

                var now = DateTime.Now;
                ListOfWarnings.Add("Błąd zapisu komunikatu do pliku" + now.ToString(" yyyy-MM-dd HH-mm-ss"));

                return 0;
            }

            return 1;
        }
        public static bool ClearActualWarningListTxtLog()
        {
            try
            {
                using (FileStream fs = File.Open(@"warnings.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
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
                var now = DateTime.Now;
                ListOfWarnings.Add("Błąd przy zerowaniu pamięci zapisanych błędów" + now.ToString(" yyyy-MM-dd HH-mm-ss"));
                return false;
            }

            return true;
        }

        public static bool ClearActualListTxtLog()
        {
            try
            {
                using (FileStream fs = File.Open(@"errors.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
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
                var now = DateTime.Now;
                ListOfOccurredProblems.Add("Błąd przy zerowaniu pamięci zapisanych błędów" + now.ToString(" yyyy-MM-dd HH-mm-ss"));
                return false;
            }

            return true;
        }

        public static bool ClearAllErr()
        {
            
            ListOfOccurredProblems.Clear();
            ClearActualListTxtLog();


            return true;
        }
        public static bool ClearAllWarn()
        {

            ListOfWarnings.Clear();
            ClearActualWarningListTxtLog();

            return true;
        }

        public static int WriteCompletedErrorListToTxtLog(string sn)
        {
            sn = System.Text.RegularExpressions.Regex.Replace(sn, @"\s+", string.Empty);

            // textBox1.Text = sn;

            string sciezka = (@"C:/errorlogi/");      //definiowanieścieżki do której zapisywane logi
            var date = DateTime.Now;
            if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
            {
                ;
            }
            else
                System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:/errorlogi/" + sn + "(" + date.ToString("yyyy-MM-dd HH-mm-ss") + ")" + ".txt"))
                {

                    foreach (var item in ListOfOccurredProblems)
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
        public static int WriteCompletedWarningsListToTxtLog(string sn)
        {
            sn = System.Text.RegularExpressions.Regex.Replace(sn, @"\s+", string.Empty);

            // textBox1.Text = sn;

            string sciezka = (@"C:/warningslogi/");      //definiowanieścieżki do której zapisywane logi
            var date = DateTime.Now;
            if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
            {
                ;
            }
            else
                System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:/warningslogi/" + sn + "(" + date.ToString("yyyy-MM-dd HH-mm-ss") + ")" + ".txt"))
                {

                    foreach (var item in ListOfWarnings)
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

    }
}
