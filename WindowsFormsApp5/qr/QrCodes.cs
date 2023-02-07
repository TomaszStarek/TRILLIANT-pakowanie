using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.QrCode;

namespace WindowsFormsApp5
{
    class QrCodes
    {
        
        // Event to generate the QC Code
        public Bitmap Generate(string str)
        {
            var qrImage = GenerateMyQCCode(str);
            return qrImage;
        }
        public void Read()
        {
            ReadQRCode();
        }
        private Bitmap GenerateMyQCCode(string QCText)
        {
            try
            {

                var QCwriter = new BarcodeWriter();
                QCwriter.Format = BarcodeFormat.QR_CODE;
                QCwriter.Options = new ZXing.Common.EncodingOptions
                {   
                
                    Width = 600,
                    Height = 600
                
                };


                BarcodeWriter writer = new BarcodeWriter();
                QrCodeEncodingOptions qr = new QrCodeEncodingOptions()
                {
                    QrCompact = true,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M,
                    Height = 500,
                    Width = 500
                
                };
                writer.Options = qr;
                writer.Format = BarcodeFormat.QR_CODE;
                var res = writer.Write(QCText);

                //var hints = new System.Collections.Hashtable();

                //// Map<EncodeHintType, Object> hints = new Hastable<EncodeHintType, Object>();
                //hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);

                //new MultiFormatWriter().encode(QCText, BarcodeFormat.QR_CODE, 1024, 1024, ErrorCorrectionLevel.L); //mapWith(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L));

               var barcodeBitmap = new Bitmap(res);

               return barcodeBitmap;

            }
            catch (Exception ex)
            {

                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, ex.ToString(),"Nie udało się stworzyć barkodu");
                var barcodeBitmap = new Bitmap("benek.jpg");
                return barcodeBitmap;
            }

            #region saveQrToFile
            /*
            jakbym chciał zapisywać barkody

            //var folder = "C:/images/" + DateTime.Now.ToString("yyyy-MM-dd");
            //var path = Path.Combine(folder, "MyQRImage.jpg");

            //using (MemoryStream memory = new MemoryStream())
            //{
            //    using (FileStream fs = new FileStream(path,
            //       FileMode.Create, FileAccess.ReadWrite))
            //    {
            //        barcodeBitmap.Save(memory, ImageFormat.Jpeg);
            //        byte[] bytes = memory.ToArray();
            //        fs.Write(bytes, 0, bytes.Length);
            //    }
            //}

            jakbym chciał zapisywać barkody */
            #endregion

        }

        private void ReadQRCode()
        {
            var QCreader = new BarcodeReader();
            string QCfilename = "C:/images/MyQRImage.jpg";
            var QCresult = QCreader.Decode(new Bitmap(QCfilename));
            if (QCresult != null)
            {
                MessageBox.Show(new Form { TopLevel = true, TopMost = true }, "My QR Code: " + QCresult.Text);
            }
        }
    }







}

