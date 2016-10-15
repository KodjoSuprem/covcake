using System.Drawing;
using System.IO;

namespace covCake.Services
{
    public class BitmapServices
    {

        public static void ResizeAndSave(Stream file, int width, int height, bool keepRatio, string serverFilename)
        {
                Resize(file, width, height, keepRatio).Save(serverFilename);
        }

        public static void ResizeAndSave(string inputFilename, int percent, string serverFilename)
        {
                Resize(new FileStream(inputFilename, FileMode.Open, FileAccess.Read, FileShare.None), percent).Save(serverFilename);
        
        }

        public static void ResizeAndSave(Stream file, int percent, string serverFilename)
        {
                Resize(file, percent).Save(serverFilename);
        }

        public static Bitmap Resize(Stream file, int w, int h, bool keepratio)
        {
            Bitmap img = new Bitmap(file);

            if (keepratio)
            {
                float ratio = ((float)img.Width) / img.Height;
                if (img.Width > w)
                    h = (int)(h / ratio);
                else if (img.Height > h)
                    w = (int)(w * ratio);
            }

            return new Bitmap(img, w, h);
        }

        public static Bitmap Resize(Stream file, int percent)
        {
            Bitmap img = new Bitmap(file);

            int tmpWidth = (int)(img.Width * (percent / 100));
            int tmpHeight = (int)(img.Height * (percent / 100));
            return new Bitmap(img, tmpWidth, tmpHeight);
        }

    }
}
