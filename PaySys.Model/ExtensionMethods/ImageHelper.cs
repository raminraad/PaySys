using System.Drawing;
using System.IO;

namespace PaySys.Model.ExtensionMethods
{
    public static class ImageHelper
    {
        //Convert Image to byte[] array:
        public static byte[] ToByteArray(this Image imageIn)
        {
            if (imageIn == null) return null;
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        //Convert byte[] array to Image:
        public static Image ToImage(this byte[] byteArrayIn)
        {
            return byteArrayIn != null ? Image.FromStream(new MemoryStream(byteArrayIn)) : null;
        }
    }
}
