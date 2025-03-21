using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace BudgetManagementSystem.Infrastructure.Concrete;

[SupportedOSPlatform("windows")]
public static class QrCodeGenerator
{
    public static string ImageContentType { get; set; } = "image/jpeg";
    public static byte[] DisplayQrCode(string text)
    {
        var qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new QRCode(qrCodeData);

        //Set logo in center of QR-code
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\logo.png");
        Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Image.FromFile(path));

        return BitmapToBytes(qrCodeImage);
    }

    private static byte[] BitmapToBytes(Bitmap img)
    {
        using MemoryStream stream = new();
        img.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    //private static string ToImage(byte[] byteValue)
    //{
    //    var base64Image = Convert.ToBase64String(byteValue);
    //    return $"data:{ImageContentType};base64,{base64Image}";
    //}
}





