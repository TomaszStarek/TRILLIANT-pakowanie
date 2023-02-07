using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    internal static class PackoutMethodsForCheckSn
    {

        private static bool CheckIsSnExistInListofStrings(string snToVerify, List<string> list)
        {          
            if (list.Contains(snToVerify))
            {
                return true;
            }

            return false;
        }




        public static int CheckSnCanBeAddedToPackedList(string snToVerify)
        {
            if (CheckIsSnExistInListofStrings(snToVerify, BoxToPackaut.ListOfScannedBarcodesVerified))
                if (!CheckIsSnExistInListofStrings(snToVerify, BoxToPackaut.ListOfScannedBarcodesPacked))
                {
                    return 0;
                }
                else if (BoxToPackaut.ListOfScannedBarcodesPacked.Last().Equals(snToVerify))
                {
                    return 1;
                }
                    
            return 2;
        }



    }
}
