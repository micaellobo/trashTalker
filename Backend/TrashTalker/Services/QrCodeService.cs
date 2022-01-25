using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace TrashTalker.Services
{
    public class QrCodeService : IQrCodeService
    {
        public void generateQRCode(Guid idContainer)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(idContainer.ToString(), QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
        
            using var bitMap = qrCode.GetGraphic(20);
            using var ms = new MemoryStream();
            
            bitMap.Save(ms, ImageFormat.Png);
            var img = Image.FromStream(ms);
            if (!Directory.Exists("images"))
                Directory.CreateDirectory("images");
            
            Image.FromStream(ms).Save($"images/{idContainer}.jpeg", ImageFormat.Jpeg);
        }
    }

    public interface IQrCodeService
    {
        void generateQRCode(Guid idContainer);
    }
}