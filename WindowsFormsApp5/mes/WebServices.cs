using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class WebServices
    {
        public string CheckSerialNumberByCheckpoint(string client, string checkPoint, string sn)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient("BoardsSoap"))
            {
                try
                {
                    var result = wsMES.CheckSerialNumberByCheckpointEPS("TRILLIANT", "PACKOUT", sn);
                    return result;
                }
                catch
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak połączenia z MES!");
                    return "Brak połączenia z MES!";
                }
            }
        }

        public string GetBoxNumber(string client, string sn)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient("BoardsSoap"))
            {
                try
                {
                    var result = wsMES.GetBoxNumber("TRILLIANT", sn);
                    return result;
                }
                catch
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak połączenia z MES!");
                    return "E";
                }

            }
        }

        public string GetSerialNoByBox(string client, string sn)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient("BoardsSoap"))
            {
                try
                {
                    var result = wsMES.GetSerialNoByBox("TRILLIANT", sn,"");
                    return result;
                }
                catch
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak połączenia z MES!");
                    return "E";
                }

            }
        }

        public string[] GetMeasuredData(string client, string sn)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient("BoardsSoap"))
            {
                try
                {
                    var result = wsMES.GetMeasuredData("TRILLIANT", sn,"","");

                    return result;
                }
                catch
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak połączenia z MES!");
                    return null;
                }

            }
        }

        public DataSet GetTestHistory(string client, string sn)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient("BoardsSoap"))
            {
                try
                {
                    var result = wsMES.GetTestHistory("TRILLIANT", sn, "");
                    return result;
                }
                catch
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak połączenia z MES!");
                    return null;
                }

            }
        }

        private string[] GetBoardData(string client, string sn)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient("BoardsSoap"))
            {
                try
                {
                    var result = wsMES.GetBoardData("TRILLIANT", sn);
                    return result;
                }
                catch
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak połączenia z MES!");
                    return null;               }

            }
        }

        public string GetNcNumber(string client, string sn)
        {
            try
            {
                var result = GetBoardData(client, sn);

                result[1] = result[1].Substring(7);

                return result[1];
            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, $"Błąd przy pobieraniu numery Nc płyty!\n{ex.Message}", sn);
                return "Error";
            }

        }


        public int CheckTimeEndStep(string stepToCheck, string client, string sn, string sign, TimeSpan timespan)
        {
            try
            {
                var result = GetTestHistory(client, sn);

                if (result is null)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak odpowiedzi od MES, wysyłam zapytanie jeszcze raz!", sn);
                    result = GetTestHistory(client, sn);
                    if (result is null)
                    {
                        MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Brak odpowiedzi od MES!", sn);
                        return 1;
                        
                    }
                    
                }

                var Test = result.Tables[0].TableName;
                if (Test != "TestHistory") return 1; //numer produktu nie widnieje w systemie MES


                var data = (from row in result.Tables["TestHistory"].AsEnumerable()
                            where (row.Field<string>("StepText").ToUpper() == stepToCheck.ToUpper() || row.Field<string>("StepText").ToUpper() == stepToCheck + "_IZOLACJA".ToUpper())
                                       && row.Field<string>("Status").ToUpper() == "Pass".ToUpper()
                            select new
                            {
                                TestProcess = row.Field<string>("StepText"),
                                //  TestType = row.Field<string>("TestType"),
                                TestStatus = row.Field<string>("Status"),
                                StartDateTime = row.Field<DateTime>("StartTime"),
                                StopDateTime = row.Field<DateTime>("EndTime"),
                            }).FirstOrDefault();

                if (data != null)
                {
                    if (sign.Equals(">"))
                    {
                        if (DateTime.Now - data.StopDateTime > timespan)
                            return 0;
                    }
                    else if (sign.Equals("<"))
                    {
                        if (DateTime.Now - data.StopDateTime < timespan)
                            return 0;
                    }

                    return 3;//zły bufor

                }
                else return 2; //brak kroku
            }
            catch
            {
                MessageBox.Show("Nie udało się uzyskać odpowiedzi od Mes (GetTestHistory)");

                return 1;
            }

        }

    }
}
