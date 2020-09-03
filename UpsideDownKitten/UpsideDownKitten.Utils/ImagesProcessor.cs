using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace UpsideDownKitten.Utils
{
    public static class ImagesProcessor
    {
        public static byte[] Rotate(byte[] data)
        {
            using (var image = Image.Load(data, out var imageFormat))
            {
                image.Mutate(x=>x.RotateFlip(RotateMode.Rotate180,FlipMode.None));
               // image.Mutate(x=>x.BlackWhite());
                image.Mutate(x=>x.BokehBlur());
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, imageFormat);
                    return ms.ToArray();
                }
            }
        }
    }
}
