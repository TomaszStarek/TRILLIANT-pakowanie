using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public static class PlcPreparedMethods
    {
        public static void checkErrorOccurrenceInPLC()
        {
            int valueOfErrorOccurrence = 0;
            var result = PLC.ReadDint("PROGRAM:MainProgram.App_Mes_Error_Occurred_int", out valueOfErrorOccurrence);

            if (!result)
            {
                result = PLC.ReadDint("PROGRAM:MainProgram.App_Mes_Error_Occurred_int", out valueOfErrorOccurrence);
                if (!result)
                {
                    MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "Błąd przy sprawdzaniu błędów na maszynie przy pakowaniu rolki"
                        , "Błąd komunikacji ze sterownikiem");
                    ProblemsToReport.WriteToListProblem("Błąd przy sprawdzaniu błędów na maszynie przy pakowaniu rolki");
                }
            }

            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 0))
                ProblemsToReport.WriteToListProblem("PLC Alarm Carrier Motion Error (błąd ruchu taśmy)");
            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 1))
                ProblemsToReport.WriteToListProblem("PLC Alarm Door Open (otworzone drzwii)");
            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 2))
                ProblemsToReport.WriteToListProblem("PLC Alarm Empty Pieces Detected (Wykrycie pustych slotów/bez płytki)");
            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 3))
                ProblemsToReport.WriteToListProblem("PLC Alarm Home needed (Homowanie)");
            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 4))
                ProblemsToReport.WriteWarnigToList("PLC Alarm Tape Jammed (płytki krzywo położone lub nałożone na siebie)");
            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 5))
                ProblemsToReport.WriteToListProblem("PLC Alarm Key Error");
            if (CheckBitsOfErrorOccurrenceResponse(valueOfErrorOccurrence, 6))
                ProblemsToReport.WriteToListProblem("PLC Alarm ConsecAtt (nieprawidłowe zliczenie płytek)");
        }
        private static bool CheckBitsOfErrorOccurrenceResponse(int valueOfErrorOccurrence, int p)
        {
            int nRightp = valueOfErrorOccurrence >> p;
            int bit = nRightp & 1;

            return (bit == 1);//by default boolean expressions are set to true            
        }
    }
}
