using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{   
    public static class SQL
    {
        public static string BoxIdFromDb { get; private set; }

        private static DataTable dt;
        private static SqlDataAdapter da;
        private static DataSet ds;

        public static void ResetBoxIdFromDb()
        {
            BoxIdFromDb = "0";
        }


        public static void UpdateCountOfErrorsInDbBox(string countOfErrorsToWrite, string IdBox)
        {

            string cmdString = "UPDATE [dbo].[rolka] SET [blad] = (@val2) WHERE [id] = (@val3)";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");             //ID unnessesary
                    comm.Parameters.AddWithValue("@val2", countOfErrorsToWrite);
                    comm.Parameters.AddWithValue("@val3", IdBox);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("exception: " + ex);
                    }
                }
            }
        }




        public static void SendDataOfSingleBoardToDb(string sn, string boxId)
        {

            string cmdString = "INSERT INTO [dbo].[plytka] ([sn],[id_rolki]) VALUES ( @val2, @val3)";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");             //ID unnessesary
                    comm.Parameters.AddWithValue("@val2", sn);
                    comm.Parameters.AddWithValue("@val3", boxId);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("exception: " + ex);
                    }
                }
            }
        }

        public static void SendDataOfSingleErrorToDb(string errorToReport, string boxId)
        {

            string cmdString = "INSERT INTO [dbo].[bledy] ([blad],[data],[id_rolki]) VALUES ( @val2, @val3, @val4)";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");             //ID unnessesary
                    comm.Parameters.AddWithValue("@val2", errorToReport);
                    comm.Parameters.AddWithValue("@val3", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
                    comm.Parameters.AddWithValue("@val4", boxId);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("exception: " + ex);
                    }
                }
            }
        }

        public static void SendDataOfSingleWarningToDb(string warningToReport, string boxId)
        {

            string cmdString = "INSERT INTO [dbo].[komunikaty] ([komunikat],[data],[id_rolki]) VALUES ( @val2, @val3, @val4)";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");             //ID unnessesary
                    comm.Parameters.AddWithValue("@val2", warningToReport);
                    comm.Parameters.AddWithValue("@val3", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
                    comm.Parameters.AddWithValue("@val4", boxId);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("exception: " + ex);
                    }
                }
            }
        }

        public static void CreateBoxInDb()
        {

            string cmdString = "INSERT INTO [dbo].[rolka] ([data],[blad]) VALUES ( @val2, @val3)";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //  comm.Parameters.AddWithValue("@val1", "1");             //ID unnessesary
                    comm.Parameters.AddWithValue("@val2", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
                    comm.Parameters.AddWithValue("@val3", "0");

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("exception: " + ex);
                    }
                }
            }
        }

        public static void ReadCreatedBoxInDb()
        {
            // string cmdString = "SELECT * FROM [dbo].[rolka] ";    //OFFSET 3 ROWS FETCH NEXT 3 ROWS ONLY

            string cmdString = "SELECT TOP (1) [id] FROM [dbo].[rolka] ORDER BY data DESC";

            //   using (SqlConnection conn = new SqlConnection("Data Source=PLKWIM0T26B2PR1;Initial Catalog = trilliant; User ID = sa; Password = Poiuytrewq123456789!"))
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                ;
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;

                    try
                    {
                        da = new SqlDataAdapter(comm.CommandText, comm.Connection);
                        ds = new DataSet();
                        da.Fill(ds, "asd");

                        dt = ds.Tables["asd"];

                        if (dt.Rows[0].ItemArray[0] is null)
                            BoxIdFromDb = "9999999";
                        else
                            BoxIdFromDb = dt.Rows[0].ItemArray[0].ToString();


                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Exception: " + ex);
                    }
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Exception: " + ex);
                    }
                }
            }
        }

        public static int ReadCountOfSerialNumbersOfGivenIdBox(string boxId)
        {
            var countOfRecords = 0;
            // string cmdString = "SELECT * FROM [dbo].[rolka] ";    //OFFSET 3 ROWS FETCH NEXT 3 ROWS ONLY

            //    "UPDATE [dbo].[rolka] SET [blad] = (@val2) WHERE [id] = (@val3)";
            string cmdString = $"SELECT COUNT ([id]) FROM [dbo].[plytka] WHERE ([id_rolki]) = {boxId}";

            //   using (SqlConnection conn = new SqlConnection("Data Source=PLKWIM0T26B2PR1;Initial Catalog = trilliant; User ID = sa; Password = Poiuytrewq123456789!"))
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["windows"].ConnectionString))
            {
                ;
                using (SqlCommand comm = new SqlCommand())
                {


                    comm.Connection = conn;
                    comm.CommandText = cmdString;

                    try
                    {
                        da = new SqlDataAdapter(comm.CommandText, comm.Connection);
                        ds = new DataSet();
                        da.Fill(ds, "asd");

                        dt = ds.Tables["asd"];

                        if (dt.Rows[0].ItemArray[0] is null)
                            countOfRecords =  999;
                        else
                            countOfRecords = (int)dt.Rows[0].ItemArray[0];


                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Exception: " + ex);

                    }
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Exception: " + ex);
                    }
                    return countOfRecords;
                }
            }
        }

        public static void SendDataOfCompletedBoxToDb()
        {
            foreach (var item in BoxToPackaut.ListOfScannedBarcodesVerified)
            {
                SendDataOfSingleBoardToDb(item, BoxIdFromDb);
            }         
        }

        public static void SendDataOfAllErrorsFromBoxToDb()
        {
            foreach (var item in ProblemsToReport.ListOfOccurredProblems)
            {
                SendDataOfSingleErrorToDb(item, BoxIdFromDb);
            }
        }

        public static void SendDataOfAllWarningsFromBoxToDb()
        {
            foreach (var item in ProblemsToReport.ListOfWarnings)
            {
                SendDataOfSingleWarningToDb(item, BoxIdFromDb);
            }
        }




    }
}
