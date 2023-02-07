using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using libplctag;
using libplctag.DataTypes;

namespace WindowsFormsApp5
{
    class PLC
    {
        public static bool ReadBool(string nameOfPlcVariable, out bool varValue)
        {

            var myTagBR = new Tag<BoolPlcMapper, bool>()
            {
                //    Name = "PROGRAM:MainProgram.Mes_App_scan",
                Name = nameOfPlcVariable,

                //PLC IP Address
                Gateway = "192.168.1.214",

                //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                Path = "1,0",

                //Type of PLC
                PlcType = PlcType.ControlLogix,

                //Protocol
                Protocol = Protocol.ab_eip,

                //A global timeout value that is used for Initialize/Read/Write methods
                Timeout = TimeSpan.FromSeconds(5),

            };
            
                try
                {
                    myTagBR.Initialize();

                //Read tag value - This pulls the value from the PLC into the local Tag value
                //bez sensu              myTag.Read();

                //Read back value from local memory
                //bez sensu             bool myVar = myTag.Value;

                //Set Tag Value
                //bez sensu myVar = !myVar;
                //    myDint++;
                //    myTag.Value = true;

                //   MessageBox.Show($"Starting tag write ({myDint})");
                //    myTag.Write();

                //Read tag value - This pulls the value from the PLC into the local Tag value
                //   MessageBox.Show($"Starting synchronous tag read");
                while(myTagBR.GetStatus() == Status.Pending)


                    myTagBR.Read();



                    //Read back value from local memory
                    //bez sensu var myVarReadBack = myTag.Value;
                    //       MessageBox.Show($"Final Value: {myDintReadBack}");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(), "Błąd komunikacji ze sterownikiem");
                //    myTag.Dispose();
                    return false;
                }
                finally
                {
                    varValue = myTagBR.Value;
                 //   myTag.Dispose();
                }
                //  myTag.Dispose();
                return true;
            
            

        }

        public static void InitEvent()
        {
               LibPlcTag.LogEvent += LibPlcTag_LogEvent;
                LibPlcTag.DebugLevel = DebugLevel.Warn;

        }


        private static void LibPlcTag_LogEvent(object sender, LogEventArgs e)
        {
            MessageBox.Show($"{e.DebugLevel}\t\t{e.Message}");
        }

        public static bool WriteBool(string nameOfPlcVariable)
        {
            //try
            //{
            //    LibPlcTag.LogEvent += LibPlcTag_LogEvent;
            //    LibPlcTag.DebugLevel = DebugLevel.Error;
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("PLC Write Bool", ex.Message);
            //}


            var myTagBW = new Tag<BoolPlcMapper, bool>()
            {
                //    Name = "PROGRAM:MainProgram.Mes_App_scan",
                Name = nameOfPlcVariable,

                //PLC IP Address
                Gateway = "192.168.1.214",

                //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                Path = "1,0",

                //Type of PLC
                PlcType = PlcType.ControlLogix,

                //Protocol
                Protocol = Protocol.ab_eip,
             //   UseConnectedMessaging = true,
                //A global timeout value that is used for Initialize/Read/Write methods
                Timeout = TimeSpan.FromSeconds(5),
            };

            try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (myTagBW != null)
                    {
                        if (myTagBW.GetStatus() == Status.Ok)
                        {
                            break;
                        }
                        else
                        {
                            Thread.Sleep(25);
                        }
                    }
                }
            }
            catch (Exception)
            {
                ;
            }

            try
            {

                myTagBW.Initialize();

                //Read tag value - This pulls the value from the PLC into the local Tag value
                //bez sensu              myTag.Read();

                //Read back value from local memory
                //bez sensu             bool myVar = myTag.Value;

                //Set Tag Value
                //bez sensu myVar = !myVar;
                //    myDint++;

                //var a = myTagBW.GetStatus();

                //while (myTagBW.GetStatus() == libplctag.Status.Pending)
                //{
                //    Thread.Sleep(100);
                //}

                //if (myTagBW.GetStatus() != libplctag.Status.ErrorNotFound) { MessageBox.Show(new Form { TopLevel = true, TopMost = true }, $"Wystąpił błąd podczas zmiany wewnętrznego stanu. Status: {myTagBW.GetStatus()}"); }




                myTagBW.Value = true;
                    myTagBW.Write();
                    //   MessageBox.Show($"Starting tag write ({myDint})");
                  //  myTag.Write();

                    //Read tag value - This pulls the value from the PLC into the local Tag value
                    //   MessageBox.Show($"Starting synchronous tag read");
                    //bez sensu   myTag.Read();

                    //Read back value from local memory
                    //bez sensu var myVarReadBack = myTag.Value;
                    //       MessageBox.Show($"Final Value: {myDintReadBack}");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(), "Błąd komunikacji ze sterownikiem");
                //    myTag.Dispose();
                    return false;
                }

              //  myTag.Dispose();
                return true;
            
        }

        #region IntMethods
        public static void RunDint(string nameOfPlcVariable)
        {
            //  MessageBox.Show($"\r\n*** ExampleRW ***");

            // var boolTag = new Tag<>

            var myTag = new Tag<DintPlcMapper, int>()
            {
                //Head_JogStep_steps
                //Name of tag on the PLC, Controller-scoped would be just "SomeDINT"
                //   Name = "PROGRAM:SomeProgram.SomeDINT",
                //  Name = "PROGRAM:MainProgram.HeadPickPosInStepsLSW", działa HMI_MainPlacedParts
                //  Name = "PROGRAM:MainProgram.Mes_App_counter",
                //   Name = "PROGRAM:MainProgram.Mes_app_sub",
                Name = nameOfPlcVariable,

                //PLC IP Address
                Gateway = "192.168.1.214",

                //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                Path = "1,0",

                //Type of PLC
                PlcType = PlcType.ControlLogix,

                //Protocol
                Protocol = Protocol.ab_eip,

                //A global timeout value that is used for Initialize/Read/Write methods
                Timeout = TimeSpan.FromSeconds(5),
            };
            

                try
                {
                    myTag.Initialize();

                    //Read tag value - This pulls the value from the PLC into the local Tag value
                    //     MessageBox.Show("Starting tag read");
                    myTag.Read();

                    //Read back value from local memory
                    int myDint = myTag.Value;
                    //     MessageBox.Show($"Initial Value: {myDint}");

                    //Set Tag Value
                    myDint++;
                    //    myDint++;
                    myTag.Value = myDint;

                    //MessageBox.Show($"Starting tag write ({myDint})");
                    myTag.Write();

                    //Read tag value - This pulls the value from the PLC into the local Tag value
                    //   MessageBox.Show($"Starting synchronous tag read");
                    myTag.Read();

                    //Read back value from local memory
                    var myDintReadBack = myTag.Value;
                    //       MessageBox.Show($"Final Value: {myDintReadBack}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(), "Błąd komunikacji ze sterownikiem");
                }

            
            
        }

        public static bool DintToZero(string nameOfPlcVariable)
        {
            //  MessageBox.Show($"\r\n*** ExampleRW ***");

            // var boolTag = new Tag<>

            var myTag = new Tag<DintPlcMapper, int>()
            {
                //Head_JogStep_steps
                //Name of tag on the PLC, Controller-scoped would be just "SomeDINT"
                //   Name = "PROGRAM:SomeProgram.SomeDINT",
                //  Name = "PROGRAM:MainProgram.HeadPickPosInStepsLSW", działa HMI_MainPlacedParts
                //  Name = "PROGRAM:MainProgram.Mes_App_counter",
                //   Name = "PROGRAM:MainProgram.Mes_app_sub",
                Name = nameOfPlcVariable,

                //PLC IP Address
                Gateway = "192.168.1.214",

                //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                Path = "1,0",

                //Type of PLC
                PlcType = PlcType.ControlLogix,

                //Protocol
                Protocol = Protocol.ab_eip,

                //A global timeout value that is used for Initialize/Read/Write methods
                Timeout = TimeSpan.FromSeconds(5),
            };
            

                try
                {
                    myTag.Initialize();

                    //Read tag value - This pulls the value from the PLC into the local Tag value
                    //     MessageBox.Show("Starting tag read");
                    // myTag.Read();

                    //Read back value from local memory
                    //     int myDint = myTag.Value;
                    //     MessageBox.Show($"Initial Value: {myDint}");

                    //Set Tag Value
                    //    myDint++;
                    //    myDint++;
                    myTag.Value = 0;

                    //            MessageBox.Show($"Starting tag write ({myDint})");
                    myTag.Write();

                    //Read tag value - This pulls the value from the PLC into the local Tag value
                    //   MessageBox.Show($"Starting synchronous tag read");
                    myTag.Read();

                    //Read back value from local memory
                    if (myTag.Value == 0)
                    {
                    //   myTag.Dispose();
                        return true;
                    }
                    else
                    {
                    //    myTag.Dispose();
                        return false;
                    }

                    //       MessageBox.Show($"Final Value: {myDintReadBack}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(), "Błąd komunikacji ze sterownikiem");
                   // myTag.Dispose();
                    return false;
                }
            
        }

        public static bool ReadDint(string nameOfPlcVariable, out int ReadedValue)
        {
            //  MessageBox.Show($"\r\n*** ExampleRW ***");
            // var boolTag = new Tag<>

            var myTag = new Tag<DintPlcMapper, int>()
            {
                //Head_JogStep_steps
                //Name of tag on the PLC, Controller-scoped would be just "SomeDINT"
                //   Name = "PROGRAM:SomeProgram.SomeDINT",
                //  Name = "PROGRAM:MainProgram.HeadPickPosInStepsLSW", działa HMI_MainPlacedParts
                //  Name = "PROGRAM:MainProgram.Mes_App_counter",
                //   Name = "PROGRAM:MainProgram.Mes_app_sub",
                Name = nameOfPlcVariable,

                //PLC IP Address
                Gateway = "192.168.1.214",

                //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                Path = "1,0",

                //Type of PLC
                PlcType = PlcType.ControlLogix,

                //Protocol
                Protocol = Protocol.ab_eip,

                //A global timeout value that is used for Initialize/Read/Write methods
                Timeout = TimeSpan.FromSeconds(5),
            };
            
                //DINT Test Read/Write
                //var myTag = new Tag<DintPlcMapper, int>()
                //{
                //    //Head_JogStep_steps
                //    //Name of tag on the PLC, Controller-scoped would be just "SomeDINT"
                //    //   Name = "PROGRAM:SomeProgram.SomeDINT",
                //    Name = "PROGRAM:MainProgram.HeadPickPosInStepsLSW",

                //    //PLC IP Address
                //    Gateway = "192.168.1.214",

                //    //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                //    Path = "1,0",

                //    //Type of PLC
                //    PlcType = PlcType.ControlLogix,

                //    //Protocol
                //    Protocol = Protocol.ab_eip,

                //    //A global timeout value that is used for Initialize/Read/Write methods
                //    Timeout = TimeSpan.FromMilliseconds(TIMEOUT),
                //};
                try
                {
                    myTag.Initialize();

                    //Read tag value - This pulls the value from the PLC into the local Tag value
                    //     MessageBox.Show("Starting tag read");
                    myTag.Read();

                    //Read back value from local memory
                    //  int myDint = myTag.Value;
                    //  MessageBox.Show(new Form { TopLevel = true, TopMost = true }, $"Initial Value: {myDint}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(), "Błąd komunikacji ze sterownikiem");
                //   myTag.Dispose();
                    return false;
                }
                finally
                {
                    ReadedValue = myTag.Value;
                }
                //  myTag.Dispose();
                return true;
            
        }
        #endregion

    }
}
